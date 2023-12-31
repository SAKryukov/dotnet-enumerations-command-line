namespace SA.Agnostic.UI.Controls {
    using System.Windows;
    using System.Windows.Controls;
    using Type = System.Type;
    using Enum = System.Enum;
    using Convert = System.Convert;
    using StringList = System.Collections.Generic.List<string>;

    public class EnumerationBitsetBox : EnumerationEditorBase {

        public EnumerationBitsetBox() {
            SetupResourceDictionary();
            Grid gridOuter = new();
            Border borderListBox = new();
            borderListBox.Child = stackPanelItems;
            StyledBorderValue borderValue = new();
            borderValue.Child = textBlockValue;
            SetupRows(gridOuter,
                new bool[] { false, true, false },
                new UIElement[] { borderListBox, new Border(), borderValue });
            Child = gridOuter;
        } //EditorPrototype

        private protected override void SetTarget(object value) {
            memberList.Clear();
            stackPanelItems.Children.Clear();
            unsignedUnderlyingValue = 0;
            signedUnderlyingValue = 0;
            textBlockValue.Text = null;
            target = value;
            if (value == null) return;
            enumType = value.GetType();
            if (!enumType.IsEnum)
                return;
            underlyingType = Enum.GetUnderlyingType(enumType);
            isSigned = Helper.TestIfSigned(underlyingType);
            if (isSigned)
                signedUnderlyingValue = (long)Convert.ChangeType(value, typeof(long));
            else
                unsignedUnderlyingValue = (ulong)Convert.ChangeType(value, typeof(ulong));
            foreach (object @object in Helper.CreateEnumerationEnumerator(enumType))
                memberList.Add((Enumerations.EnumerationItemBase)@object);
            Populate();
        } //SetTarget

        void Populate() {
            int index = 0;
            foreach (Enumerations.EnumerationItemBase item in memberList) {
                CheckBox checkbox = new() { Content = item.DisplayName, DataContext = index++ };
                checkbox.Checked += (sender, _) => CheckboxHandler(sender as CheckBox, isChecked: true);
                checkbox.Unchecked += (sender, _) => CheckboxHandler(sender as CheckBox, isChecked: false);
                stackPanelItems.Children.Add(checkbox);
            } //loop
            DisplayValue();
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
            DisplayValue();
        } //CheckboxHandler

        void DisplayValue() {
            StringList list = new();
            long longTargetValue = (long)Convert.ChangeType(target, typeof(long));
            ulong ulongTargetValue = (ulong)Convert.ChangeType(target, typeof(ulong));
            foreach (Enumerations.EnumerationItemBase item in memberList) {
                if (isSigned) {
                    long longValue = (long)Convert.ChangeType(item.GenericEnumValue, typeof(long));
                    if ((longTargetValue & longValue) != 0)
                        list.Add(item.DisplayName);
                } else {
                    ulong ulongValue = (ulong)Convert.ChangeType(target, typeof(ulong));
                    if ((ulongTargetValue & ulongValue) != 0)
                        list.Add(item.DisplayName);
                } //if
            } //loop
            textBlockValue.Text = string.Join(flagDelimiter, list);
        } //DisplayValue

        readonly static string flagDelimiter;
        [System.Flags] private enum FlagDelimiterTest { Left = 1, Right = 2, } //used to calculate FlagDelimiter from sample
        static EnumerationBitsetBox() {
            flagDelimiter = (FlagDelimiterTest.Left | FlagDelimiterTest.Right).ToString().Replace(FlagDelimiterTest.Left.ToString(), string.Empty).Replace(FlagDelimiterTest.Right.ToString(), string.Empty);
        } //StringAttributeUtility

        #endregion most difficult part

        Type underlyingType;
        bool isSigned;
        ulong unsignedUnderlyingValue;
        long signedUnderlyingValue;
        readonly StackPanel stackPanelItems = new() { Orientation = Orientation.Vertical };

    } //EnumerationBitsetBox

}
