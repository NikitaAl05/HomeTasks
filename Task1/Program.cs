namespace Task1;
using System.IO;
class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введте запрос: ");
        string? query = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(query))
        {
            Console.WriteLine("Ошибка: запрос не может быть пустым.");
            return;
        }
        
        string safeFileName = string.Join("_", query.Split(Path.GetInvalidFileNameChars()));
        string pdfPath = $"{safeFileName}_report.pdf";
        
        byte[]? mainBytes = null;
        byte[]? imagesBytes = null;

        using (var searchService = new BrowserSearchService())
        {
            var (mainScreenshot, imagesScreenshot) = searchService.CaptureAllScreenshots(query);

            mainBytes = mainScreenshot?.AsByteArray;
            imagesBytes = imagesScreenshot?.AsByteArray;
        }
        
        var exporter = new SearchPdfExporter();
        exporter.ExportToPdf(mainBytes, imagesBytes, pdfPath);

        Console.WriteLine($"Готово! Отчет сохранен в файл {pdfPath}");
    }
}

