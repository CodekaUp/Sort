using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Sort_Library
{
    public class Library
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


        public static Dictionary<string, int> GetWordCountsParallel(string filePath)
        {

            Dictionary<string, int> wordCounts = new Dictionary<string, int>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line = " ";

                    while ((line = reader.ReadLine()) != null)
                    {
                        //многопоточность
                        Parallel.ForEach(Regex.Split(line.ToLower(), @"\W+"), word =>
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
                        });
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