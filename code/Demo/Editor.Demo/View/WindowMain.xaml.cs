namespace SA.Demo.Editing.View {
    using System;
    using System.Windows;

    public partial class WindowMain : Window {

        public WindowMain() {
            InitializeComponent();
            enumerationBitsetBox.TargetObjectName = "bitset Options";
            enumerationBitsetBox.Target = bitsetOption;
            enumerationBox.TargetObjectName = "value Option";
            enumerationBox.Target = valueOption;
        } //WindowMain

        protected override void OnContentRendered(EventArgs eventArgs) {
            base.OnContentRendered(eventArgs);
        } //OnContentRendered

        Main.BitsetOption bitsetOption = default;
        Main.ValueOption valueOption = default;

    } //class WindowMain

}
