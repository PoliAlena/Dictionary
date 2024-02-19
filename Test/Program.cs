using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string pathIn = @"C:\Users\юзер\Desktop\Test\tolstoj_lew_nikolaewich-text_0060.txt";
            string pathOut = @"C:\Users\юзер\Desktop\Test\Dictionary.txt";
            try
            {
                using (FileStream fs = new FileStream(pathIn, FileMode.Open, FileAccess.Read))
                using (StreamReader reader = new StreamReader(fs))
                {
                    string text = reader.ReadToEnd();
                    text = Regex.Replace(text, @"[IVX]+", "").ToLower();
                    text = Regex.Replace(text, @"\s+", " ");
                    string punctuation = @"[,.!?;0-9\[\]\(\):\*]|(\s-)|(-\s)";
                    text = Regex.Replace(text, punctuation, "");
                    text = text.Replace('"', ' ');
                    string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    Dictionary<string, int> countW = new Dictionary<string, int>();

                    foreach (string word in words)
                    {
                        if (countW.ContainsKey(word))
                        {
                            countW[word]++;
                        }
                        else
                        {
                            countW[word] = 1;
                        }
                    }

                    var sorted = countW.OrderByDescending(pair => pair.Value);

                    using (StreamWriter writer = new StreamWriter(pathOut))
                    {
                        foreach (var pair in sorted)
                        {
                            writer.WriteLine($"{pair.Key} {pair.Value}");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }
    }
}
