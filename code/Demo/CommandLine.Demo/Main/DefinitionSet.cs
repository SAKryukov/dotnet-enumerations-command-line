namespace SA.Test.CommandLine.Main {

    static class DefinitionSet {

        internal const string extensionExecutable = "exe";
        internal static string MaskSampler(string ownName, string ownExtension) =>
            $"{ownName}*{ownExtension}";

        internal static string ResourceKey(string optionName) =>
            $"{optionName}D";

        internal static class Sampler {
            internal const string localServer = ".";
            internal const int timeout = 1000; // 1s //SA???
        } //class Sampler

        internal static class Documentation {
            const string indent = "          ";
            const string delimiter = ",";
            const string suffix = ":";
            const string switchBnf = "{+|-}";
            internal static string FormatName(string name, bool last, bool isSwitch = false) =>
                $"-{name}{(isSwitch ? switchBnf : string.Empty)}{(last ? suffix : delimiter)}";
            internal static string FormatDescripton(string name) =>
                $"{indent}{name}";
            internal static string FormatDisplayName(string name, string description) =>
                $"{indent}{name}: {description}";
        } //class Documentation


    } //class DefinitionSet

}
