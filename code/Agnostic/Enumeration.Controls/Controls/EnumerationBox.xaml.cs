namespace SA.Agnostic.UI.Controls {
    using System.Windows.Controls;
    using IEnumerable = System.Collections.IEnumerable;
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
            Type type = typeof(Enumerations.Enumeration<>);
            Type enumerationType = type.MakeGenericType(new Type[] { enumType });
            object enumeration = System.Activator.CreateInstance(enumerationType);
            IEnumerable enumerable = (IEnumerable)enumeration;
            foreach (object @object in enumerable)
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

        public string TargetObjectName {
            get => targetObjectName;
            set {
                targetObjectName = value;
                textBlockName.Text = value;
            } //set TargetObjectName 
        } //TargetObjectName 

        string targetObjectName;
        Type enumType;
        private protected object target;
        readonly MemberList memberList = new();

    } //class EnumerationBox

}

