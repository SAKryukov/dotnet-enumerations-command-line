namespace SA.Agnostic.UI.Controls {
    using System.Windows;
    using System.Windows.Controls;
    using Type = System.Type;

    public class EnumerationEditorBase : Border {

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

        private protected StyledTextBlockName textBlockName;
        private protected StyledTextBlockValue textBlockValue;

    } //EnumerationEditorBase

}

