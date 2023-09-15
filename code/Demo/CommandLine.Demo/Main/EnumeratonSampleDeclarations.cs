namespace SA.Test.CommandLine.Main {
    using SA.Universal.Enumerations;
    using Application = System.Windows.Application;

    class Nameset : IStringAttribute {
        public Nameset() {
            dictionary = Application.Current?.MainWindow?.Resources;
        } //Nameset
        (string name, string description) IStringAttribute.this[string option] =>
            ((string)dictionary?[option], (string)dictionary?[$"{option}D"]);
        readonly System.Windows.ResourceDictionary dictionary;
    } //class Nameset

    [DisplayName(typeof(Nameset))]
    [Description(typeof(Nameset))]
    enum StringOption {
        [Abbreviation(1)]
        InputDirectory,
        InputFileMask,
        [Abbreviation(1)]
        OutputDirectory,
        [Abbreviation(1)]
        ForceOutputFormat,
        ConfigurationFile,
        [Abbreviation(3)]
        LogFile,
    } //enum StringOption

    [DisplayName(typeof(Nameset))]
    [Description(typeof(Nameset))]
    enum SwitchOption {
        Default,
        [Abbreviation(1)]
        Recursive,
        [Abbreviation(1)]
        CreateOutputDirectory,
        [Abbreviation(1)]
        Quiet,
    } //SwitchOption

}
