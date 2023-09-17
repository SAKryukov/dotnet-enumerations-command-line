namespace SA.Agnostic.UI.Controls {
    using System.Windows;
    using System.Windows.Controls;

    public class EnumerationBox : EnumerationEditorBase {

        public EnumerationBox() {
            SetupResourceDictionary();
            Grid gridOuter = new();
            Border borderListBox = new();
            listBox = new() { BorderThickness = new Thickness(0) };
            borderListBox.Child = listBox;
            StyledBorderValue borderValue = new();
            borderValue.Child = textBlockValue;
            SetupRows(gridOuter,
                new bool[] { false, false },
                new UIElement[] { borderListBox, borderValue });
            Child = gridOuter;
        } //EditorPrototype

        private protected override void SetTarget(object value) {
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

        readonly ListBox listBox;

    } //EnumerationBox

}
