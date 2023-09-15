namespace SA.Test.CommandLine.Main {
    using System;
    using System.Windows;

    static class TestStart {
        [STAThread]
        static void Main() {
            Application app = new Agnostic.UI.AdvancedApplication<View.WindowMain>() {};
            app.Run();
        } //MainClass
    } //class TestStart

}
