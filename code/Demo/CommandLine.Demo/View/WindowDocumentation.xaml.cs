namespace SA.Test.CommandLine.View {
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using CultureInfo = System.Globalization.CultureInfo;
    using CultureList = System.Collections.Generic.List<System.Globalization.CultureInfo>;
    using Keyboard = System.Windows.Input.Keyboard;
    using StringList = System.Collections.Generic.List<string>;
    using AdvancedApplicationBase = Agnostic.UI.AdvancedApplicationBase;
    using ApplicationSatelliteAssemblyIndex = Agnostic.UI.ApplicationSatelliteAssemblyIndex;
    using Key = System.Windows.Input.Key;

    public partial class WindowDocumentation : Window {

        public WindowDocumentation() {
            InitializeComponent();
            cultures = PopulateCultures();
            BuildDocumentation();
            buttonCopy.Click += (_, _) => ImplementCopy();
            PreviewKeyDown += (_, eventArgs) => {
                if ((eventArgs.Key == Key.C) &&
                (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
                    ImplementCopy();
            }; //PreviewKeyDown
            buttonCopy.IsEnabled = false;
            treeView.SelectedItemChanged += (sender, _) =>
                buttonCopy.IsEnabled = (sender as TreeView).SelectedItem != null;
        } //WindowDocumentation

        CultureInfo[] PopulateCultures() {
            var implementedcultures = ApplicationSatelliteAssemblyIndex.ImplementedCultures;
            CultureList list = new(implementedcultures);
            list.Insert(0, System.Threading.Thread.CurrentThread.CurrentUICulture);
            void AddCulture(CultureInfo culture) {
                comboBoxCulture.Items.Add(culture.Name);
            } //AddCulture
            foreach (var culture in list)
                AddCulture(culture);
            if (comboBoxCulture.Items.Count > 1)
                comboBoxCulture.SelectedIndex = 0;
            comboBoxCulture.SelectionChanged += (sender, _) => {
                if (sender is not ComboBox comboBox) return;
                AdvancedApplicationBase.Current.Localize(cultures[comboBox.SelectedIndex]);
                BuildDocumentation();
            }; //comboBoxCulture
            return list.ToArray();
        } //PopulateCultures

        void ImplementCopy() {
            string value = treeView.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(value))
                Clipboard.SetText(value);
        } //ImplementCopy

        void BuildDocumentation() {
            Agnostic.Enumerations.Enumeration<Main.SwitchOption> switches = new(dynamic: true);
            Agnostic.Enumerations.Enumeration<Main.StringOption> values = new(dynamic: true);
            StringList stringSwitches = new();
            foreach (var option in switches)
                ShowOption(stringSwitches, option, isSwitch: true);
            treeViewItemSwitches.ItemsSource = stringSwitches;
            StringList stringValues = new();
            foreach (var option in values)
                ShowOption(stringValues, option, isSwitch: false);
            treeViewItemValues.ItemsSource = stringValues;
        } //BuildDocumentation

        static void ShowOption<ENUM>(StringList list, Agnostic.Enumerations.EnumerationItem<ENUM> option, bool isSwitch = false) {
            list.Add(Main.DefinitionSet.Documentation.FormatName(option.Name, option.AbbreviatedName == option.Name, isSwitch: isSwitch));
            if (option.AbbreviatedName != option.Name)
                list.Add(Main.DefinitionSet.Documentation.FormatName(option.AbbreviatedName, true, isSwitch: isSwitch));
            if (!string.IsNullOrEmpty(option.Description))
                list.Add(Main.DefinitionSet.Documentation.FormatDisplayName(option.DisplayName, option.Description));
            else
                list.Add(Main.DefinitionSet.Documentation.FormatDescripton(option.DisplayName));
        } //ShowOption

        protected override void OnContentRendered(EventArgs eventArgs) {
            base.OnContentRendered(eventArgs);
            if (comboBoxCulture.Items.Count > 1)
                Keyboard.Focus(comboBoxCulture);
        } //OnContentRendered

        protected override void OnClosing(CancelEventArgs e) {
            e.Cancel = true;
            Hide();
        } //OnClosing

        internal void ShowDocumentation(Window owner) {
            Owner = owner;
            ShowDialog();
        } //ShowDocumentation

        readonly CultureInfo[] cultures;

    } //class WindowDocumentation

}
