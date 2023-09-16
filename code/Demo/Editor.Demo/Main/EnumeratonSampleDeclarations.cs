namespace SA.Demo.Editing.Main {
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

    [System.Flags]
    [DisplayTextProvider(typeof(Nameset))]
    enum BitsetOption : int {
        [NonEnumerable]
        None = 0,
        Recursive = 1 << 1,
        CaseSensitive = 1 << 2,
        TimeCritical = 1 << 3,
        PasswordProtected = 1 << 4,
        [NonEnumerable]
        All = Recursive | CaseSensitive | TimeCritical | PasswordProtected,
        [NonEnumerable]
        Default = Recursive | CaseSensitive,
    } //enum StringOption

    [DisplayTextProvider(typeof(Nameset))]
    enum ChoiceOption {
        Apple,
        MandarinOrange,
        Avocado,
        BlackRaspberry,
        RedRaspberry,
    } //ChoiceOption

}
