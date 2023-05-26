using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Sort_Library;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        //Получение текстового файла. Нужно ввести полный путь к файлу
        string filePath = Console.ReadLine();

        //Время выполнения приватного метода 
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        var library = typeof(Library);
        MethodInfo method = library.GetMethod("GetWordCounts", BindingFlags.NonPublic | BindingFlags.Static);
        Dictionary<string, int> wordCounts = (Dictionary<string, int>)method.Invoke(library,new object[] { filePath });
        Console.WriteLine("Время выполнения приватного метода: {0} ms", stopwatch.ElapsedMilliseconds);
        stopwatch.Stop();

        //Проверка, создание файла и перечисление слов в порядке убывания
        if (wordCounts.Any())
        {
            string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "PrivateOutput.txt");

            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                foreach (KeyValuePair<string, int> v in wordCounts.OrderByDescending(x => x.Value))
                {
                    writer.WriteLine($"{v.Key} - {v.Value}");
                }
            }
            Console.WriteLine($"Приватный метод сохранен в файл {outputFilePath}");
        }
        else
        {
            Console.WriteLine("Файл не содержит слов");
        }
        Console.ReadLine();


        //Время выполнения публичного метода 
        Stopwatch stopwatch1 = new Stopwatch();
        stopwatch1.Start();
        Dictionary<string, int> wordCountsParallel = Library.GetWordCountsParallel(filePath);
        Console.WriteLine("Время выполнения публичного метода: {0} ms", stopwatch1.ElapsedMilliseconds);
        stopwatch1.Stop();

        //Проверка, создание файла и перечисление слов в порядке убывания
        if (wordCountsParallel.Any())
        {
            string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "PublicOutput.txt");

            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                foreach (KeyValuePair<string, int> v in wordCountsParallel.OrderByDescending(x => x.Value))
                {
                    writer.WriteLine($"{v.Key} - {v.Value}");
                }
            }
            Console.WriteLine($"Публичный метод сохранен в файл {outputFilePath}");
        }
        else
        {
            Console.WriteLine("Файл не содержит слов");
        }
        Console.ReadLine();
    }
}