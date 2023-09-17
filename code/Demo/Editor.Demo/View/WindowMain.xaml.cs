namespace SA.Demo.Editing.View {
    using System;
    using System.Windows;

    public partial class WindowMain : Window {

        public WindowMain() {
            InitializeComponent();
            //Agnostic.UI.AdvancedApplicationBase.Current.Localize(new System.Globalization.CultureInfo("ru-RU"));
            enumerationBox.TargetObjectName = "Fruit Choice";
            enumerationBox.Target = valueOption;
            enumerationComboBox.Target = valueOptionCombo;
            enumerationComboBox.TargetObjectName = enumerationBox.TargetObjectName;
            enumerationBitsetBox.TargetObjectName = "bitset Options";
            enumerationBitsetBox.Target = bitsetOption;
        } //WindowMain

        protected override void OnContentRendered(EventArgs eventArgs) {
            base.OnContentRendered(eventArgs);
        } //OnContentRendered

        readonly Main.BitsetOption bitsetOption = default;
        readonly Main.ChoiceOption valueOption = default;
        readonly Main.ChoiceOption valueOptionCombo = default;

    } //class WindowMain

}
