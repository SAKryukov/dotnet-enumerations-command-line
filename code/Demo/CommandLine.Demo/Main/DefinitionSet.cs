namespace SA.Test.CommandLine.Main {

    static class DefinitionSet {

        internal const string extensionExecutable = "exe";
        internal static string MaskSampler(string ownName, string ownExtension) =>
            $"{ownName}*{ownExtension}";

        internal static class Sampler {
            internal const string localServer = ".";
            internal const int timeout = 1000; // 1s //SA???
        } //class Sampler

    } //class DefinitionSet

}
