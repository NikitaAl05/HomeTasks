using iText.Kernel.Pdf;
using iText.Layout;
using iText.IO.Image;
using iText.Layout.Element;

namespace Task1;

internal sealed class SearchPdfExporter
{
    public void ExportToPdf(byte[] mainSearchBytes, byte[] imagesSearchBytes, string outputPath)
    {
        using var writer = new PdfWriter(outputPath);
        using var pdf = new PdfDocument(writer);
        using var document = new Document(pdf);

        if (mainSearchBytes != null && mainSearchBytes.Length > 0)
        {
            ImageData imageData = ImageDataFactory.Create(mainSearchBytes);
            Image image = new Image(imageData);
            
            image.SetAutoScale(true);
            document.Add(image);
        }

        if (imagesSearchBytes != null && imagesSearchBytes.Length > 0)
        {
            document.Add(new AreaBreak());
            
            ImageData imageData = ImageDataFactory.Create(imagesSearchBytes);
            Image image = new Image(imageData);
            
            image.SetAutoScale(true);
            document.Add(image);
        }
        
    }
}