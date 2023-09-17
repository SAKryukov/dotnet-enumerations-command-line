namespace SA.Demo.Editing.View {
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using CultureInfo = System.Globalization.CultureInfo;
    using CultureList = System.Collections.Generic.List<System.Globalization.CultureInfo>;
    using ApplicationSatelliteAssemblyIndex = Agnostic.UI.ApplicationSatelliteAssemblyIndex;
    using AdvancedApplicationBase = Agnostic.UI.AdvancedApplicationBase;
    using Keyboard = System.Windows.Input.Keyboard;
    using EnumerationEditorBase = Agnostic.UI.Controls.EnumerationEditorBase;

    public partial class WindowMain : Window {

        public WindowMain() {
            InitializeComponent();
            cultures = PopulateCultures();
            PopulateEnumerations();
        } //WindowMain

        protected override void OnContentRendered(EventArgs eventArgs) {
            base.OnContentRendered(eventArgs);
            if (comboBoxCulture.Items.Count > 1)
                Keyboard.Focus(comboBoxCulture);
        } //OnContentRendered

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
                PopulateEnumerations();
            }; //comboBoxCulture
            return list.ToArray();
        } //PopulateCultures

        void PopulateEnumerations() {
            enumerationBitsetBox.Target = bitsetOption;
            enumerationBox.Target = valueOption;
            enumerationComboBox.Target = valueOptionCombo;
            void SetVisibility(bool value) {
                foreach (EnumerationEditorBase box in new EnumerationEditorBase[] { enumerationBitsetBox, enumerationBox, enumerationComboBox })
                    box.IsLabelVisible = value;
            } //SetVisibility
            CheckBoxLabelVisibility.Checked += (_, _) => SetVisibility(true); 
            CheckBoxLabelVisibility.Unchecked += (_, _) => SetVisibility(false);
        } //PopulateEnumerations

        readonly Main.BitsetOption bitsetOption = default;
        readonly Main.ChoiceOption valueOption = default;
        readonly Main.ChoiceOption valueOptionCombo = default;

        readonly CultureInfo[] cultures;

    } //class WindowMain

}
