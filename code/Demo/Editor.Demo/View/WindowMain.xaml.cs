namespace SA.Demo.Editing.View {
    using System;
    using System.Windows;

    public partial class WindowMain : Window {

        public WindowMain() {
            InitializeComponent();
            enumerationBox.TargetObjectName = "value Option";
            enumerationBox.Target = valueOption;
            enumerationComboBox.Target = valueOptionCombo;
            enumerationComboBox.TargetObjectName = enumerationBox.TargetObjectName;
            enumerationBitsetBox.TargetObjectName = "bitset Options";
            enumerationBitsetBox.Target = bitsetOption;
        } //WindowMain

        protected override void OnContentRendered(EventArgs eventArgs) {
            base.OnContentRendered(eventArgs);
        } //OnContentRendered

        Main.BitsetOption bitsetOption = default;
        Main.ValueOption valueOption = default;
        Main.ValueOption valueOptionCombo = default;

    } //class WindowMain

}
