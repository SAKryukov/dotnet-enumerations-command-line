namespace SA.Demo.Editing.View {
    using System;
    using System.Windows;

    public partial class WindowMain : Window {

        public WindowMain() {
            InitializeComponent();
            enumerationBitsetBox.Target = bitsetOption;
        } //WindowMain

        protected override void OnContentRendered(EventArgs eventArgs) {
            base.OnContentRendered(eventArgs);
        } //OnContentRendered

        Main.BitsetOption bitsetOption = default;

    } //class WindowMain

}
