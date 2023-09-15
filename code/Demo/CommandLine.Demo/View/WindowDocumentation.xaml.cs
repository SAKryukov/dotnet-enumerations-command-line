namespace SA.Test.CommandLine.View {
    using System;
    using System.ComponentModel;
    using System.Windows;
    using CultureInfo = System.Globalization.CultureInfo;
    using CultureList = System.Collections.Generic.List<System.Globalization.CultureInfo>;
    using Keyboard = System.Windows.Input.Keyboard;
    using StringList = System.Collections.Generic.List<string>;
    using AdvancedApplicationBase = Agnostic.UI.AdvancedApplicationBase;
    using ApplicationSatelliteAssemblyIndex = Agnostic.UI.ApplicationSatelliteAssemblyIndex;
    using ComboBox = System.Windows.Controls.ComboBox;

    public partial class WindowDocumentation : Window {

        public WindowDocumentation() {
            InitializeComponent();
            cultures = PopulateCultures();
            BuildDocumentation();
            buttonCopy.Click += (_, _) => comboBoxCulture.SelectedItem = 0;
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

        void BuildDocumentation() {
            Universal.Enumerations.Enumeration<Main.SwitchOption> switches = new(refresh: true);
            Universal.Enumerations.Enumeration<Main.StringOption> values = new(refresh: true);
            StringList stringSwitches = new();
            foreach (var option in switches)
                ShowOption(stringSwitches, option, isSwitch: true);
            treeViewItemSwitches.ItemsSource = stringSwitches;
            StringList stringValues = new();
            foreach (var option in values)
                ShowOption(stringValues, option, isSwitch: false);
            treeViewItemValues.ItemsSource = stringValues;
        } //BuildDocumentation

        void ShowOption<ENUM>(StringList list, Universal.Enumerations.EnumerationItem<ENUM> option, bool isSwitch = false) {
            list.Add(Main.DefinitionSet.Documentation.FormatName(option.Name, option.AbbreviatedName == option.Name, isSwitch: isSwitch));
            if (option.AbbreviatedName != option.Name)
                list.Add(Main.DefinitionSet.Documentation.FormatName(option.AbbreviatedName, true, isSwitch: isSwitch));
            if (!string.IsNullOrEmpty(option.Description))
                list.Add(Main.DefinitionSet.Documentation.FormatDisplayName(option.DisplayName, option.Description));
            else
                list.Add(Main.DefinitionSet.Documentation.FormatDescripton(option.DisplayName));
        } //ShowOption

        protected override void OnContentRendered(EventArgs e) {
            base.OnContentRendered(e);
            if (comboBoxCulture.Items.Count > 1)
                Keyboard.Focus(comboBoxCulture);
        } //OnContentRendered

        protected override void OnClosing(CancelEventArgs e) {
            e.Cancel = true;
            Hide();
        } //OnClosing

        internal void ShowDocumentation(Window owner) {
            Owner = owner;
            BuildDocumentation();
            ShowDialog();
        } //ShowDocumentation

        CultureInfo[] cultures;

    } //class WindowDocumentation

}
