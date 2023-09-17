namespace SA.Agnostic.UI.Controls {
    using System.Windows.Controls;
    using MemberList = System.Collections.Generic.List<Enumerations.EnumerationItemBase>;
    using Type = System.Type;

    public partial class EnumerationComboBox : UserControl {

        public EnumerationComboBox() {
            InitializeComponent();
        } //EnumerationBox

        void SetTarget(object value) {
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

        public object Target {
            get => target;
            set {
                SetTarget(value);
            } //Target set
        } //set

        #region property
        public static readonly System.Windows.DependencyProperty EnumerationObjectNameProperty = System.Windows.DependencyProperty.Register(
        name: nameof(EnumerationObjectName),
        propertyType: typeof(string),
        ownerType: typeof(EnumerationComboBox),
        typeMetadata: new System.Windows.FrameworkPropertyMetadata(
            (sender, eventArgs) => {
                if (sender is not EnumerationComboBox dependencyObject) return;
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

    } //class EnumerationComboBox

}

