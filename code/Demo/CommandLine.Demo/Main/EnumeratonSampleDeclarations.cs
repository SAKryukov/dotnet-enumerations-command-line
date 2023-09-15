namespace SA.Test.CommandLine.Main {
    using SA.Universal.Enumerations;

    enum StringOption {
        [Abbreviation(1)]
        InputDirectory,
        InputFileMask,
        [Abbreviation(1)]
        OutputDirectory,
        [Abbreviation(1)]
        ForceOutputFormat,
        [Abbreviation(1)]
        ConfigurationFile,
        [Abbreviation(3)]
        LogFile,
    } //enum StringOption

    enum BitsetOption {
        Default,
        [Abbreviation(1)]
        Recursive,
        [Abbreviation(1)]
        CreateOutputDirectory,
        [Abbreviation(1)]
        Quite,
    } //BitsetOption

}
