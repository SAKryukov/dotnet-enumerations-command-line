namespace SA.Agnostic.UI.Controls {
    using System.Windows.Controls;
    using Type = System.Type;
    using Enum = System.Enum;
    using Convert = System.Convert;
    using IEnumerable = System.Collections.IEnumerable;
    using MemberList = System.Collections.Generic.List<Enumerations.EnumerationItemBase>;

    public partial class EnumerationBitsetBox : UserControl {

        public EnumerationBitsetBox() {
            InitializeComponent();
        } //EnumerationBitsetBox()

        void SetTarget(object value) {
            memberList.Clear();
            stackPanelItems.Children.Clear();
            unsignedUnderlyingValue = 0;
            signedUnderlyingValue = 0;
            textBlockValue.Text = null;
            target = value;
            textBlockValue.Text = value.ToString(); // SA??? make it DisplayName
            if (value == null) return;
            enumType = value.GetType();
            if (!enumType.IsEnum)
                return;
            underlyingType = Enum.GetUnderlyingType(enumType);
            SetSigned();
            if (isSigned)
                signedUnderlyingValue = (long)Convert.ChangeType(value, typeof(long));
            else
                unsignedUnderlyingValue = (ulong)Convert.ChangeType(value, typeof(ulong));
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
            foreach(Enumerations.EnumerationItemBase item in memberList) {
                CheckBox checkbox = new() { Content = item.DisplayName, DataContext = index++ };
                checkbox.Checked += (sender, _) => CheckboxHandler(sender as CheckBox, isChecked: true);
                checkbox.Unchecked += (sender, _) => CheckboxHandler(sender as CheckBox, isChecked: false);
                stackPanelItems.Children.Add(checkbox);
            } //loop
        } //Populate

        #region most difficult part
        void CheckboxHandler(CheckBox checkbox, bool isChecked = false) {
            int index = (int)checkbox.DataContext;
            var value = memberList[index].GenericEnumValue;
            if (isSigned) {
                long longValue = (long)Convert.ChangeType(value, typeof(long));
                if (isChecked)
                    signedUnderlyingValue += longValue;
                else
                    signedUnderlyingValue &= ~longValue;
                target = Enum.ToObject(enumType, signedUnderlyingValue);
            } else {
                ulong ulongValue = (ulong)Convert.ChangeType(value, typeof(ulong));
                if (isChecked)
                    unsignedUnderlyingValue += ulongValue;
                else
                    unsignedUnderlyingValue &= ~ulongValue;
                target = Enum.ToObject(enumType, unsignedUnderlyingValue);
            } //if
            textBlockValue.Text = target.ToString(); // SA??? make it DisplayName
        } //CheckboxHandler
        void SetSigned() {
            sbyte testSignValue = -1;
            try {
                Convert.ChangeType(testSignValue, underlyingType);
                isSigned = true;
            } catch {
                isSigned = false;
            } //exception
        } //SetSigned
        #endregion most difficult part

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
        object target;
        Type enumType, underlyingType;
        bool isSigned;
        ulong unsignedUnderlyingValue;
        long signedUnderlyingValue;
        readonly MemberList memberList = new();

    } //class EnumerationBitsetBox

}
