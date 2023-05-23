using System.Text.RegularExpressions;

namespace Sort_Library
{
    public static class Library
    {
        private static Dictionary<string, int> GetWordCounts(string filePath)
        {
            Dictionary<string, int> wordCounts = new Dictionary<string, int>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line = " ";

                    while ((line = reader.ReadLine()) != null)
                    {
                        foreach (string word in Regex.Split(line.ToLower(), @"\W+"))
                        {
                            if (!string.IsNullOrEmpty(word))
                            {
                                if (wordCounts.ContainsKey(word))
                                {
                                    wordCounts[word]++;
                                }
                                else
                                {
                                    wordCounts.Add(word, 1);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return wordCounts;
        }
    }
}