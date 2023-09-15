namespace SA.Test.Plugin {
    using System.IO.Pipes;
    using StreamWriter = System.IO.StreamWriter;

    static class Sampler {
        static void Main(string[] commandLine) {
            string name = System.Reflection.Assembly.GetEntryAssembly().Location;
            NamedPipeClientStream stream = new (CommandLine.Main.DefinitionSet.Sampler.localServer, name,
                        PipeDirection.Out, PipeOptions.None,
                        System.Security.Principal.TokenImpersonationLevel.Impersonation);
            stream.Connect(timeout: CommandLine.Main.DefinitionSet.Sampler.timeout); 
            if (!stream.IsConnected) return;
            using StreamWriter writer = new(stream);
            for (int index = 0; index < commandLine.Length; ++index)
                if (index == commandLine.Length - 1)
                    writer.Write(commandLine[index]);
                else
                    writer.WriteLine(commandLine[index]);
        } //Main
    } //class Sampler

}

