using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Sort_Library;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        //Получение текстового файла. Нужно ввести полный путь к файлу
        string filePath = Console.ReadLine();

        //Получение приватного метода из dll
        var library = typeof(Library);
        MethodInfo method = library.GetMethod("GetWordCounts", BindingFlags.NonPublic | BindingFlags.Static);
        Dictionary<string, int> wordCounts = (Dictionary<string, int>)method.Invoke(library, new object[] {filePath});

        //Проверяем содержит ли словарь какие-либо элементы, если да - создаем файл "output.txt", перечисляя слова в порядке убывания их кол-ва
        if (wordCounts.Any())
        {
            string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "output.txt");

            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                foreach (KeyValuePair<string, int> v in wordCounts.OrderByDescending(x => x.Value))
                {
                    writer.WriteLine($"{v.Key} - {v.Value}");
                }
            }

            Console.WriteLine($"Результат сохранен в файл {outputFilePath}");
        }
        else
        {
            Console.WriteLine("Файл не содержит слов");
        }

        Console.ReadLine();
    }
}