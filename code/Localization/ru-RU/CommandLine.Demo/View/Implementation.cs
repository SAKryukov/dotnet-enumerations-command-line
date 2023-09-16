[assembly: SA.Agnostic.PluginManifest(
    typeof(SA.Agnostic.UI.IApplicationSatelliteAssembly),
    typeof(SA.Plugin.Localization.Implementation))]

namespace SA.Plugin.Localization {
    using System.Windows;
    using IApplicationSatelliteAssembly = Agnostic.UI.IApplicationSatelliteAssembly;

    class Implementation : IApplicationSatelliteAssembly {

        internal Implementation() {
            Main documentation = new();
            resourceDictionary = documentation.Resources;
        } //Implementation

        ResourceDictionary IApplicationSatelliteAssembly.this[string fullTypeName] => resourceDictionary;
        ResourceDictionary IApplicationSatelliteAssembly.ApplicationResources => null;

        readonly ResourceDictionary resourceDictionary;

    } //class Implementation

}
