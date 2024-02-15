using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

public class Program
{
    public class PreparedData
    {
        public string Section { get; set; }
        public List<string> Props { get; set; }
    }


    private static List<PreparedData> PrepareData(Dictionary<string, string> data)
    {
        var result = new List<PreparedData>();

        var props = new List<string>();

        var keys = data.Select(x => x.Key).ToList();

        foreach (var key in keys)
        {
            string section = key.Split('.')[^2];
            var prop = key.Split('.').Last();

            if (result.Any(x => x.Section == section))
            {
                props.Add(prop);
                result.Add(new PreparedData { Section = section, Props = props });
            }
            else
            {
                result.Add(new PreparedData { Section = section, Props = props });
            }
        }


        return result;
    }


    public static void Main(string[] args)
    {
        var remoteData = DataClient.Read("C:\\work\\dev\\PdfDemo\\data2.json");
        JsonHelper jsonHelper = new JsonHelper();
        var result = jsonHelper.FlattenJson(remoteData);


        var data = PrepareData(result);

        foreach (var item in data)
        {
            Console.WriteLine("section:" + item.Section);

            Console.WriteLine("section items:");

            item.Props.ForEach(Console.WriteLine);
        }




        //foreach (var item in result)
        //{
        //var splittedArray = item.Key.Split('.');
        //string section = splittedArray[splittedArray.Length - 2];


        //Console.WriteLine(item.Key + " : " + item.Value);

        //data.Add(new Data(item.Key.Split('.').Last(), item.Value));
        //}


        //sections.Distinct().ToList().ForEach(Console.WriteLine);


        //QuestPDF.Settings.License = LicenseType.Community;
        //Document.Create(container =>
        //{
        //    container.Page(page =>
        //    {
        //        page
        //        .Content()
        //        .ContentFromRightToLeft()
        //        .Padding(8)
        //        .Border(1)
        //        .Table(table =>
        //        {
        //            table.ColumnsDefinition(columns =>
        //            {
        //                columns.RelativeColumn();
        //                columns.RelativeColumn();
        //                columns.RelativeColumn();
        //                columns.RelativeColumn();
        //                columns.RelativeColumn();
        //                columns.RelativeColumn();
        //            });

        //            table.ExtendLastCellsToTableBottom();
        //            var text = "نظام تخصيص الطاقة وشبكات أنابيب الغاز";

        //            table.Cell().ValueCell().Image("logo-dark.png");
        //            table.Cell().ColumnSpan(5).ValueCell(text);

        //            data.ForEach(item =>
        //            {
        //                table.Cell().LabelCell(item.Label);
        //                table.Cell().ValueCell(item.Value.ToString());
        //            });

        //            // table.Cell().LabelCell("Remarks");
        //            // table.Cell().LabelCell("Remarks");
        //            // table.Cell().LabelCell("Remarks");
        //            // table.Cell().ColumnSpan(5).ValueCell().Text("Lorem, ipsum dolor sit amet consectetur adipisicing elit. Aspernatur omnis asperiores doloremque maiores officiis harum assumenda doloribus. Unde, sit quia. Nemo ipsam quisquam similique vero tempore dolorem obcaecati assumenda iste.");
        //        });
        //    });
        //})
        //.ShowInPreviewer();
    }
}




public class DataClient
{
    public static string Read(string path)
    {
        using StreamReader file = new StreamReader(path);

        try
        {
            string json = file.ReadToEnd();
            return json;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Cannot read file {e}");
            return "";
        }
    }
}

public record Data(string Label, object? Value);


