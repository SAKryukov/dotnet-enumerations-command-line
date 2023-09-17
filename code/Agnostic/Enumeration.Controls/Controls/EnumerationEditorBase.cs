﻿namespace SA.Agnostic.UI.Controls {
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

        public object Target {
            get => target;
            set {
                SetTarget(value);
            } //Target set
        } //set

        private protected object target;
        private protected Type enumType;
        private protected MemberList memberList = new();
        private protected StyledTextBlockName textBlockName;
        private protected StyledTextBlockValue textBlockValue;

    } //EnumerationEditorBase

}
