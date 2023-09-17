namespace SA.Test.CommandLine.View {
    using System;
    using System.Windows;
    using System.IO.Pipes;
    using CheckBox = System.Windows.Controls.CheckBox;
    using Process = System.Diagnostics.Process;
    using StringList = System.Collections.Generic.List<string>;
    using Keyboard = System.Windows.Input.Keyboard;
    using StreamReader = System.IO.StreamReader;
    using Path = System.IO.Path;
    using Directory = System.IO.Directory;
    using File = System.IO.File;
    using CommandLine = Agnostic.Utilities.CommandLine<Main.SwitchOption, Main.StringOption>;
    using CommandLineSwitchStatus = Agnostic.Utilities.CommandLineSwitchStatus;
    using CommandLineParsingOptions = Agnostic.Utilities.CommandLineParsingOptions;
    using Key = System.Windows.Input.Key;

    public partial class WindowMain : Window {

        public WindowMain() {
            InitializeComponent();
            documentation = new();
            textBoxCommandLine.TextChanged += (_, _) => Parse();
            buttonDocumentation.Click += (_, _) => documentation.ShowDocumentation(this);
            foreach(CheckBox checkbox in new CheckBox [] { checkBoxCaseSensitiveKeys, checkBoxCaseSensitiveAbbreviations, checkBoxCaseSensitiveFiles }) {
                checkbox.Checked += (_, _) => Parse();
                checkbox.Unchecked += (_, _) => Parse();
            }; //loop
            PreviewKeyDown += (_, eventArgs) => {
                if (eventArgs.Key == Key.F1)
                    documentation.ShowDocumentation(this);
            }; //PreviewKeyDown
            string name = System.Reflection.Assembly.GetEntryAssembly().Location;
            string[] files = Directory.GetFiles(Path.GetDirectoryName(name), Main.DefinitionSet.MaskSampler(Path.GetFileNameWithoutExtension(name), Path.GetExtension(name)));
            foreach (string file in files)
                if (file != name) {
                    pipeName = file;
                    break;
                } //if
            samplerName = Path.ChangeExtension(pipeName, Main.DefinitionSet.extensionExecutable);
        } //WindowMain

        protected override void OnContentRendered(EventArgs eventArgs) {
            base.OnContentRendered(eventArgs);
            Parse();
            Keyboard.Focus(textBoxCommandLine);
        } //OnContentRendered

        StringList GetPreparsedLines(string input) {
            if (!(File.Exists(pipeName) && File.Exists(samplerName)))
                return null;
            NamedPipeServerStream stream = new(pipeName, PipeDirection.In, 1);
            using StreamReader reader = new(stream);
            Process process = new();
            process.StartInfo.FileName = samplerName;
            process.StartInfo.Arguments = input;
            process.StartInfo.UseShellExecute = true;
            process.Start();
            stream.WaitForConnection();
            StringList list = new();
            while (true) {
                string line = reader.ReadLine();
                if (line == null) break;
                list.Add(line);
            } //loop
            return list;
        } //GetPreparsedLines

        void Parse() {
            StringList list = GetPreparsedLines(textBoxCommandLine.Text);
            CommandLineParsingOptions parsingOptions;
            parsingOptions = CommandLineParsingOptions.CaseInsensitive;
            if (checkBoxCaseSensitiveKeys.IsChecked == true) parsingOptions |= CommandLineParsingOptions.CaseSensitiveKeys;
            if (checkBoxCaseSensitiveAbbreviations.IsChecked == true) parsingOptions |= CommandLineParsingOptions.CaseSensitiveAbbreviations;
            if (checkBoxCaseSensitiveFiles.IsChecked == true) parsingOptions |= CommandLineParsingOptions.CaseSensitiveFiles;
            CommandLine commandLine = new(list.ToArray(), parsingOptions);
            treeViewItemFiles.ItemsSource = commandLine.Files;
            StringList values = new();
            foreach (var option in commandLine.ValueEnumeration)
                values.Add($"{option.Name}: {commandLine[option.EnumValue]}");
            treeViewItemOptionsValue.ItemsSource = values;
            StringList switchOptions = new();
            foreach (var switchOption in commandLine.SwitchEnumeration) {
                CommandLineSwitchStatus switchStatus = commandLine[switchOption.EnumValue];
                switchOptions.Add($"{switchOption.Name}: {switchStatus}");
            } //loop
            treeViewItemOptionsSwitch.ItemsSource = switchOptions;
            StringList unrecognized = new();
            foreach (string option in commandLine.UnrecognizedOptions)
                unrecognized.Add(option);
            treeViewItemUnrecognized.ItemsSource = unrecognized;
            StringList repeatedSwitches = new();
            foreach (string option in commandLine.RepeatedSwitches)
                repeatedSwitches.Add(option);
            treeViewItemRepeatedSwitches.ItemsSource = repeatedSwitches;
            StringList repeatedValues = new();
            foreach (string option in commandLine.RepeatedValues)
                repeatedValues.Add(option);
            treeViewItemRepeatedValues.ItemsSource = repeatedValues;
            StringList repeatedFiles = new();
            foreach (string option in commandLine.RepeatedFiles)
                repeatedFiles.Add(option);
            treeViewItemRepeatedFiles.ItemsSource = repeatedFiles;
        } //Parse

        readonly string pipeName, samplerName;
        readonly WindowDocumentation documentation;

    } //class WindowMain

}
