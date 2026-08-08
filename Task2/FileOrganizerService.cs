namespace Task2;

internal sealed class FileOrganizerService
{
    private const string DestinationPath = "//Users/nikita/Desktop/TestTask/Печать";

    public void CheckPath()
    {
        if (!Directory.Exists(DestinationPath))
        {
            Directory.CreateDirectory(DestinationPath);
        }
    }

    public void ProcessFiles(string sourcePath, string[] keywords)
    {
        if (!Directory.Exists(sourcePath))
        {
            Console.WriteLine("Указанная исходная папка не существует.");
            return;
        }

        CheckPath();
        
        string[] files = Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories);

        foreach (string file in files)
        {
            string fileName = Path.GetFileName(file);
            
            if (keywords.Any(k => fileName.Contains(k, StringComparison.OrdinalIgnoreCase)))
            {
                string destinationPath = Path.Combine(DestinationPath, fileName);
                File.Copy(file, destinationPath, true);
            }
        }
    }
}