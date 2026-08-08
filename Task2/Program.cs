namespace Task2;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Путь к папке: ");
        string path = Console.ReadLine();

        Console.WriteLine("Файлы для копирования: ");
        string input = Console.ReadLine();
        string[] keywords = input
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(k => k.Trim())
            .ToArray();
        
        if (keywords.Length == 0)
        {
            Console.WriteLine("Не указано ни одного ключевого слова для поиска.");
            return;
        }
        
        FileOrganizerService organizer = new FileOrganizerService();
        organizer.ProcessFiles(path, keywords);

        Console.WriteLine("\nКопирование завершено! Проверьте целевую папку.");
    }
}