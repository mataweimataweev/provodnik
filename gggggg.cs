using System;
using System.IO;

public static class FileExplorer
{
    public static void Start()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Выберите диск:");
            DriveInfo[] drives = DriveInfo.GetDrives();

            for (int i = 0; i < drives.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {drives[i].Name}");
            }

            int choice = ReadChoice(drives.Length);
            string selectedDrive = drives[choice - 1].RootDirectory.FullName;

            ExploreFolder(selectedDrive);
        }
    }

    private static void ExploreFolder(string path)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Текущая папка: {path}");

            DirectoryInfo directory = new DirectoryInfo(path);

            foreach (var subdirectory in directory.GetDirectories())
            {
                Console.WriteLine($"Папка: {subdirectory.Name}");
            }

            foreach (var file in directory.GetFiles())
            {
                Console.WriteLine($"Файл: {file.Name}");
            }

            Console.WriteLine("\nВыберите папку или файл:");

            string choice = Console.ReadLine();

            if (choice == "..")
            {
                if (directory.Parent != null)
                {
                    path = directory.Parent.FullName;
                }
                else
                {
                    break;
                }
            }
            else
            {
                string newPath = Path.Combine(path, choice);

                if (File.Exists(newPath))
                {
                    OpenFile(newPath);
                }
                else if (Directory.Exists(newPath))
                {
                    path = newPath;
                }
                else
                {
                    Console.WriteLine("Неверный выбор!");
                    Console.ReadKey();
                }
            }
        }
    }

    private static void OpenFile(string path)
    {
        string extension = Path.GetExtension(path);

        switch (extension)
        {
            case ".txt":
                Console.WriteLine($"Открываем файл {path} через блокнот");
                // Здесь можно добавить код для открытия файла через блокнот
                break;
            case ".docx":
                Console.WriteLine($"Открываем файл {path} через Word");
                // Здесь можно добавить код для открытия файла через Word
                break;
            default:
                Console.WriteLine($"Невозможно открыть файл {path}");
                break;
        }

        Console.ReadKey();
    }

    private static int ReadChoice(int maxChoice)
    {
        int choice;

        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > maxChoice)
        {
            Console.WriteLine("Неверный выбор! Попробуйте еще раз:");
        }

        return choice;
    }
}

public class Program
{
    public static void Main()
    {
        FileExplorer.Start();
    }
}