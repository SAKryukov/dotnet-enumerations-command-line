namespace SA.Agnostic.UI.Controls {
    using System.Windows;
    using System.Windows.Controls;
    using MemberList = System.Collections.Generic.List<Enumerations.EnumerationItemBase>;
    using Type = System.Type;

    public class EnumerationBox : Border {

        public EnumerationBox() {
            SetupResourceDictionary();
            Grid gridOuter = new();
            gridOuter.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            gridOuter.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            gridOuter.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            gridOuter.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            StyledBorderName borderName = new();
            textBlockName = new();
            borderName.Child = textBlockName;
            Border borderListBox = new();
            listBox = new() { BorderThickness = new Thickness(0) };
            borderListBox.Child = listBox;
            StyledBorderValue borderValue = new();
            textBlockValue = new();
            borderValue.Child = textBlockValue;
            int index = 0;
            foreach (Border border in new Border[] { borderName, borderListBox, borderValue }) {
                Grid.SetRow(border, index++);
                gridOuter.Children.Add(border);
            } //loop
            Child = gridOuter;
        } //EditorPrototype

        void SetupResourceDictionary() {
            ResourceDictionarySource source = new();
            Resources = source.Resources;
        } //SetupResourceDictionary

        void SetTarget(object value) {
            memberList.Clear();
            listBox.Items.Clear();
            textBlockValue.Text = null;
            target = value;
            enumType = value.GetType();
            foreach (object @object in Helper.CreateEnumerationEnumerator(enumType))
                memberList.Add((Enumerations.EnumerationItemBase)@object);
            Populate();
        } //SetTarget

        void Populate() {
            int index = 0;
            int indexToSelect = 0;
            foreach (Enumerations.EnumerationItemBase item in memberList) {
                ListBoxItem visualItem = new() { Content = item.DisplayName, DataContext = index++ };
                listBox.Items.Add(visualItem);
            } //loop
            listBox.SelectionChanged += (sender, eventArgs) => {
                if (sender is not ListBox listBoxSender) return;
                if (listBoxSender.SelectedIndex < 0) return;
                if (listBoxSender.SelectedItem is not ListBoxItem item) return;
                int index = (int)item.DataContext;
                target = memberList[index].GenericEnumValue;
                textBlockValue.Text = memberList[index].DisplayName;
            }; //listBox.SelectionChanged
            listBox.SelectedIndex = indexToSelect;
        } //Populate

        public object Target {
            get => target;
            set {
                SetTarget(value);
            } //Target set
        } //set

        #region property
        public static readonly DependencyProperty EnumerationObjectNameProperty = DependencyProperty.Register(
        name: nameof(EnumerationObjectName),
        propertyType: typeof(string),
        ownerType: typeof(EnumerationBox),
        typeMetadata: new FrameworkPropertyMetadata(
            (sender, eventArgs) => {
                if (sender is not EnumerationBox dependencyObject) return;
                dependencyObject.textBlockName.Text = (string)eventArgs.NewValue;
            }));
        public string EnumerationObjectName {
            get => (string)GetValue(EnumerationObjectNameProperty);
            set => SetValue(EnumerationObjectNameProperty, value);
        } //EnumerationObjectName
        #endregion property

        Type enumType;
        private protected object target;
        readonly MemberList memberList = new();

        readonly StyledTextBlockName textBlockName;
        readonly StyledTextBlockValue textBlockValue;
        readonly ListBox listBox;

    } //EnumerationBox

}
