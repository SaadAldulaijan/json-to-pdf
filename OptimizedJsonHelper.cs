
using Newtonsoft.Json.Linq;

public class OptimizedJsonHelper
{
    public Dictionary<string, string> FlattenJson(string jsonString)
    {
        var obj = JObject.Parse(jsonString);
        return FlattenJsonObject(obj);
    }

    private static Dictionary<string, string> FlattenJsonObject(JToken token)
    {
        var result = new Dictionary<string, string>();

        // var res = new List<JsonReportModel>();

        if (token is JProperty property)
        {
            var key = property.Name;
            var value = property.Value.ToString();
            result.Add(key, value);
            // res.Add(new JsonReportModel(ReportItemType.Text, result));
        }
        else if (token is JObject obj)
        {
            foreach (JProperty prop in obj.Properties())
            {
                var nested = FlattenJsonObject(prop.Value);
                foreach (var pair in nested)
                {
                    //string nestedKey = $"{pair.Key}";
                    result.Add(pair.Key, pair.Value);
                }
            }

            // res.Add(new JsonReportModel(ReportItemType.Section, result));
        }
        else if (token is JArray array)
        {
            foreach (JToken item in array)
            {
                var nested = FlattenJsonObject(item);
                foreach (var pair in nested)
                {
                    result.Add(pair.Key, pair.Value);
                }
            }

            // res.Add(new JsonReportModel(ReportItemType.Array, result));
        }
        else if (token is JValue value)
        {
            if (value.Parent is not null)
            {
                result.Add(value.Parent.Path.ToString(), value?.Value?.ToString() ?? "");
                // res.Add(new JsonReportModel(ReportItemType.Text, result));
            }
        }

        return result;
        // return res;
    }
}


public record JsonReportModel(string parent, Dictionary<string, string> Result);
