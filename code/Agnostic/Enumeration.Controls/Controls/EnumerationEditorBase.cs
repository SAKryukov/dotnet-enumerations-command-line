namespace SA.Agnostic.UI.Controls {
    using System.Windows;
    using System.Windows.Controls;
    using MemberList = System.Collections.Generic.List<Enumerations.EnumerationItemBase>;
    using Type = System.Type;

    public abstract class EnumerationEditorBase : Border {

        private protected void SetupResourceDictionary() {
            ResourceDictionarySource source = new();
            Resources = source.Resources;
        } //SetupResourceDictionary

        private protected abstract void SetTarget(object value);

        private protected static void SetupRows(Grid grid, bool[] stars, UIElement[] children) {
            for (int index = 0; index < stars.Length; ++index)
                grid.RowDefinitions.Add(new RowDefinition() {
                    Height = stars[index] ? new GridLength(1, GridUnitType.Star) : GridLength.Auto
                });
            for (int index = 0; index < children.Length; ++index) {
                Grid.SetRow(children[index], index);
                grid.Children.Add(children[index]);
            } //loop
        } //SetupRows

        public object Target {
            get => target;
            set {
                SetTarget(value);
            } //Target set
        } //set

        private protected object target;
        private protected Type enumType;
        private protected readonly MemberList memberList = new();
        private protected readonly StyledTextBlockValue textBlockValue = new();

    } //EnumerationEditorBase

}
