namespace FamilyTree.Helper.Extension
{
    public static class IEnumerableExtension
    {
        public static bool IsEmpty<T>(this IEnumerable<T> obj)=>obj==null || obj.Any()==false;
    }
}
