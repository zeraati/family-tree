namespace FamilyTree.Helper.Extension
{
    public static class DateTimeExtension
    {
        public static string ToDate(this DateTime obj)
        {
            if (obj == null) return null;
            return $"{obj.Year.ToString("00")}-{obj.Month.ToString("00")}-{obj.Day.ToString("00")}";
        }
    }
}
