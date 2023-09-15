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
                ShowOption(stringSwitches, option);
            treeViewItemSwitches.ItemsSource = stringSwitches;
            StringList stringValues = new();
            foreach (var option in values)
                ShowOption(stringValues, option);
            treeViewItemValues.ItemsSource = stringValues;
        } //WindowDocumentation

        void ShowOption<ENUM>(StringList list, Universal.Enumerations.EnumerationItem<ENUM> option) {
            list.Add($"-{option.Name}");
            if (option.AbbreviatedName != option.Name)
                list.Add($"{option.AbbreviatedName}");
            if (!string.IsNullOrEmpty(option.Description))
                list.Add($"            {option.Description}");
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
