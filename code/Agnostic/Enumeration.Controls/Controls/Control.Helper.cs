namespace SA.Agnostic.UI.Controls {
    using Type = System.Type;
    using Convert = System.Convert;
    using IEnumerable = System.Collections.IEnumerable;

    static class Helper {

        internal static IEnumerable CreateEnumerationEnumerator(Type enumType) {
            Type type = typeof(Enumerations.Enumeration<>);
            Type enumerationType = type.MakeGenericType(new Type[] { enumType });
            object enumeration = System.Activator.CreateInstance(enumerationType, new object[] { true });
            return (IEnumerable)enumeration;
        } //CreateEnumerationEnumerator

        internal static bool TestIfSigned(Type underlyingType) {
            sbyte testSignValue = -1;
            try {
                Convert.ChangeType(testSignValue, underlyingType);
                return true;
            } catch {
                return false;
            } //exception
        } //TestIfSigned

    } //class Helper

}