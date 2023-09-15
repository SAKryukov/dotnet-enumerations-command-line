namespace SA.Test.CommandLine.View {
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using StringList = System.Collections.Generic.List<string>;

    public partial class WindowDocumentation : Window {

        public WindowDocumentation() {
            InitializeComponent();
            Universal.Enumerations.Enumeration<Main.SwitchOption> switches = new();
            Universal.Enumerations.Enumeration<Main.StringOption> values = new();
            StringList stringSwitches = new();
            foreach (var option in switches)
                ShowOption(stringSwitches, option, isSwitch: true);
            treeViewItemSwitches.ItemsSource = stringSwitches;
            StringList stringValues = new();
            foreach (var option in values)
                ShowOption(stringValues, option, isSwitch: false);
            treeViewItemValues.ItemsSource = stringValues;
        } //WindowDocumentation

        void ShowOption<ENUM>(StringList list, Universal.Enumerations.EnumerationItem<ENUM> option, bool isSwitch = false) {
            list.Add(Main.DefinitionSet.Documentation.FormatName(option.Name, option.AbbreviatedName == option.Name, isSwitch: isSwitch));
            if (option.AbbreviatedName != option.Name)
                list.Add(Main.DefinitionSet.Documentation.FormatName(option.AbbreviatedName, true, isSwitch: isSwitch));
            if (!string.IsNullOrEmpty(option.Description))
                list.Add(Main.DefinitionSet.Documentation.FormatDisplayName(option.DisplayName, option.Description));
            else
                list.Add(Main.DefinitionSet.Documentation.FormatDescripton(option.DisplayName));
        } //ShowOption

        protected override void OnClosing(CancelEventArgs e) {
            e.Cancel = true;
            Hide();
        } //OnClosing

        internal void ShowDocumentation(Window owner) {
            Owner = owner;
            ShowDialog();
        } //ShowDocumentation

    } //class WindowDocumentation

}
