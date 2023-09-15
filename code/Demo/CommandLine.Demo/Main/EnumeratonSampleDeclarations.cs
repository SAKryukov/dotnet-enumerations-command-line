namespace SA.Test.CommandLine.Main {
    using SA.Agnostic.Enumerations;
    using Application = System.Windows.Application;

    class Nameset : IStringAttribute {
        (string name, string description) IStringAttribute.this[string option] {
            get {
                System.Windows.ResourceDictionary dictionary = Application.Current?.MainWindow.Resources;
                return ((string)dictionary?[option], (string)dictionary?[DefinitionSet.ResourceKey(option)]);
            } //get this
        } //this
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
