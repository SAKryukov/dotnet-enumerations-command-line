namespace SA.Agnostic.UI.Controls {
    using System.Windows.Controls;
    using MemberList = System.Collections.Generic.List<Enumerations.EnumerationItemBase>;
    using Type = System.Type;

    public partial class EnumerationBox : UserControl {

        public EnumerationBox() {
            InitializeComponent();
        } //EnumerationBox

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
        public static readonly System.Windows.DependencyProperty EnumerationObjectNameProperty = System.Windows.DependencyProperty.Register(
        name: nameof(EnumerationObjectName),
        propertyType: typeof(string),
        ownerType: typeof(EnumerationBox),
        typeMetadata: new System.Windows.FrameworkPropertyMetadata(
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

    } //class EnumerationBox

}

