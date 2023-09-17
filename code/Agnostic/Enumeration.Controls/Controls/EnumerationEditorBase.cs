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

        private protected static DependencyProperty RegisterEnumerationObjectNameProperty(Type ownerType) {
            return DependencyProperty.Register(
                name: nameof(EnumerationObjectName),
                propertyType: typeof(string),
                ownerType: ownerType,
                typeMetadata: new FrameworkPropertyMetadata(
                    (sender, eventArgs) => {
                        if (sender is not EnumerationEditorBase dependencyObject) return;
                        dependencyObject.textBlockName.Text = (string)eventArgs.NewValue;
                    }));
        } //RegisterEnumerationObjectNameProperty
        public string EnumerationObjectName { get; set; }
        //
        private protected static DependencyProperty RegisterIsLabelVisibleProperty(Type ownerType) {
            return DependencyProperty.Register(
                name: nameof(IsLabelVisible),
                propertyType: typeof(bool),
                ownerType: ownerType,
                typeMetadata: new FrameworkPropertyMetadata(
                    (sender, eventArgs) => {
                        if (sender is not EnumerationEditorBase dependencyObject) return;
                        dependencyObject.borderName.Visibility = (bool)eventArgs.NewValue
                            ? Visibility.Visible
                            : Visibility.Collapsed;
                    }));
        } //RegisterIsLabelVisibleProperty
        public bool IsLabelVisible { get; set; }

        public object Target {
            get => target;
            set {
                SetTarget(value);
            } //Target set
        } //set

        private protected object target;
        private protected Type enumType;
        private protected readonly MemberList memberList = new();
        private protected readonly StyledBorderName borderName = new() { Visibility = Visibility.Collapsed };
        private protected readonly StyledTextBlockName textBlockName = new();
        private protected readonly StyledTextBlockValue textBlockValue = new();

    } //EnumerationEditorBase

}
