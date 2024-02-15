using Newtonsoft.Json.Linq;

public class JsonHelper
{
    public Dictionary<string, string> FlattenJson(string jsonString)
    {
        var obj = JObject.Parse(jsonString);
        return FlattenJsonObject(obj);
    }

    private static Dictionary<string, string> FlattenJsonObject(JToken token)
    {
        var result = new Dictionary<string, string>();

        if (token is JProperty property)
        {
            var key = property.Name;
            var value = property.Value.ToString();
            result.Add(key, value);
        }
        else if (token is JObject obj)
        {
            foreach (JProperty prop in obj.Properties())
            {
                // Handle nested objects recursively
                var nested = FlattenJsonObject(prop.Value);
                foreach (var pair in nested)
                {
                    // string nestedKey = $"{prop.Name}.{pair.Key}";
                    //string nestedKey = $"{pair.Key}";
                    result.Add(pair.Key, pair.Value);
                }
            }
        }
        else if (token is JArray array)
        {
            // Handle arrays by flattening each element
            foreach (JToken item in array)
            {
                var nested = FlattenJsonObject(item);
                // Use a prefix for array elements to avoid potential key collisions
                foreach (var pair in nested)
                {
                    // var arrayKey = $"{array.Parent.Path}.[{array.IndexOf(item)}].{pair.Key}";
                    var arrayKey = $"{pair.Key}";
                    result.Add(arrayKey, pair.Value);
                }
            }
        }
        else if (token is JValue value)
        {
            if (value.Parent is not null)
            {
                result.Add(value.Parent.Path.ToString(), value?.Value?.ToString() ?? "");
            }
        }

        return result;
    }
}