namespace FamilyTree.Helper.Extension
{
    public static class StringExtension
    {
        public static bool IsEmpty(this string obj)=>string.IsNullOrEmpty(obj) || string.IsNullOrWhiteSpace(obj);
        public static bool NullableIsEmpty(this string? obj)=>string.IsNullOrEmpty(obj) || string.IsNullOrWhiteSpace(obj);
    }
}
