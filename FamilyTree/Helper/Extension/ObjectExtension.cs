using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace FamilyTree.Helper.Extension
{
    public static class ObjectExtension
    {
        public static string ToJason(this object obj, bool camelCase = false)
        {
            if (obj == null) return string.Empty;

            if (obj.GetType() == typeof(string)) return (string)obj;

            var setting = new JsonSerializerSettings();
            if (camelCase == true) setting.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return JsonConvert.SerializeObject(obj, setting);
        }
    }
}
