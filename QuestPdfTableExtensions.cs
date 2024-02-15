using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public static class QuestPdfTableExtensions
{
    private static IContainer Cell(this IContainer container, bool dark)
    {
        return container
            .Border(1)
            .Background(dark ? Colors.Grey.Lighten2 : Colors.White)
            .Padding(10);
    }

    // displays only text label
    public static void LabelCell(this IContainer container, string text) =>
    container.Cell(true).Text(text).Bold().FontFamily(Fonts.Arial);

    // allows you to inject any type of content, e.g. image
    public static IContainer ValueCell(this IContainer container) => container.Cell(false);

    public static TextSpanDescriptor ValueCell(this IContainer container, string text) =>
        container.Cell(false).Text(text).FontFamily(Fonts.Arial);
}


