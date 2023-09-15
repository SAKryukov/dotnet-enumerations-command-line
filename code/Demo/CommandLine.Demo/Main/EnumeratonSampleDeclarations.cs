namespace SA.Test.CommandLine.Main {
    using SA.Universal.Enumerations;

    enum StringOption {

        [Abbreviation(1)]
        [Description("Directory where the input files are found")]
        [DisplayName("InputDirectory")]
        InputDirectory,

        [Description("File name pattern")]
        [DisplayName("Input File Mask")]
        InputFileMask,

        [Description("Directory where the output files should be created")]
        [DisplayName("Output Directory")]
        [Abbreviation(1)]
        OutputDirectory,

        [Description("Output file format to be used regardless of the input file format")]
        [DisplayName("Force Output Format")]
        [Abbreviation(1)]
        ForceOutputFormat,

        [Abbreviation(1)]
        ConfigurationFile,

        [Abbreviation(3)]
        LogFile,

    } //enum StringOption

    enum SwitchOption {

        Default,

        [Abbreviation(1)]
        [Description("Use recursive pattern")]
        Recursive,

        [Abbreviation(1)]
        CreateOutputDirectory,
        [Abbreviation(1)]

        Quite,

    } //SwitchOption

}
