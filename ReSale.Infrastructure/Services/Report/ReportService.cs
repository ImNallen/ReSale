using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ReSale.Application.Abstractions.Services;

namespace ReSale.Infrastructure.Services.Report;

public class ReportService : IReportService
{
    public void GenerateSimpleReport(string outputPath) =>
        Document.Create(container => container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.Header().Text("Simple Report").SemiBold().FontSize(36);
                    page.Content().PaddingVertical(1, Unit.Centimetre).Column(column =>
                    {
                        column.Spacing(20);
                        column.Item().Text("This is a simple report generated using QuestPDF.");
                        column.Item().Text(Placeholders.LoremIpsum());
                    });
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                    });
                }))
            .GeneratePdf(outputPath);
}
