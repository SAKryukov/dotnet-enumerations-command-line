namespace SA.Agnostic.UI.Controls {
    using System.Windows;
    using System.Windows.Controls;

    public class EnumerationComboBox : EnumerationEditorBase {

        public EnumerationComboBox() {
            SetupResourceDictionary();
            Grid gridOuter = new();
            StyledBorderName borderName = new();
            textBlockName = new();
            borderName.Child = textBlockName;
            Border borderListBox = new();
            comboBox = new() { BorderThickness = new Thickness(0) };
            borderListBox.Child = comboBox;
            StyledBorderValue borderValue = new();
            textBlockValue = new();
            borderValue.Child = textBlockValue;
            int index = 0;
            gridOuter.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            gridOuter.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            gridOuter.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            gridOuter.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            foreach (Border border in new Border[] { borderName, borderListBox, new Border(), borderValue }) {
                Grid.SetRow(border, index++);
                gridOuter.Children.Add(border);
            } //loop
            Child = gridOuter;
        } //EditorPrototype

        private protected override void SetTarget(object value) {
            memberList.Clear();
            comboBox.Items.Clear();
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
                comboBox.Items.Add(visualItem);
            } //loop
            comboBox.SelectionChanged += (sender, eventArgs) => {
                if (sender is not ComboBox comboBoxSender) return;
                if (comboBoxSender.SelectedIndex < 0) return;
                if (comboBoxSender.SelectedItem is not ListBoxItem item) return;
                int index = (int)item.DataContext;
                target = memberList[index].GenericEnumValue;
                textBlockValue.Text = memberList[index].DisplayName;
            }; //listBox.SelectionChanged
            comboBox.SelectedIndex = indexToSelect;
        } //Populate

        #region property
        public static readonly DependencyProperty EnumerationObjectNameProperty = RegisterEnumerationObjectNameProperty(typeof(EnumerationComboBox));
        new public string EnumerationObjectName {
            get => (string)GetValue(EnumerationObjectNameProperty);
            set => SetValue(EnumerationObjectNameProperty, value);
        } //EnumerationObjectName
        #endregion property

        readonly ComboBox comboBox;

    } //EnumerationComboBox

}
