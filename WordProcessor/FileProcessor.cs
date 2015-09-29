using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordProcessor
{
    public class FileProcessor : IFileProcessor
    {
        private readonly string fullFilePath;
        private readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["FilePath"]);

        public FileProcessor(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException($"File name: {fileName} is invalid.");
            }
            
            fullFilePath = Path.Combine(filePath, fileName);
        }

        public IEnumerable<KeyValuePair<string, int>> GetWordCount()
        {
            var words = GetWords();

            var wordCount = words
                .GroupBy(w => w)
                .ToDictionary(g=> g.Key, g=> g.Count())
                .OrderBy(a=>a.Key)
                .ToList();

            return wordCount;
        }

        private string[] GetWords()
        {
            if (!File.Exists(fullFilePath))
            {
                throw new FileNotFoundException("File not found", fullFilePath);
            }

            var fileContent = Regex.Replace(File.ReadAllText(fullFilePath), @"[\r\n\t\W\s\ ]+", " ");

            fileContent = Regex.Replace(fileContent, @"[\s ]+", " ");

            var words = fileContent.Split(' ');
            return words;
        }

        public string GetConcatenatedWords(int wordSize = 6)
        {
            var concatenatedWords = new List<string>();

            var words = GetWords();

            foreach (var word in words.Where(a => a.Length == wordSize))
            {
                for (int i = 0; i < words.Count(); i++)
                {
                    for (int j = 0; j < words.Count() - 1; j++)
                    {

                        if (i != j)
                        {
                            if (string.Concat(words[i], words[j]) == word)
                            {
                                if (!concatenatedWords.Contains(word, StringComparer.OrdinalIgnoreCase))
                                {
                                    concatenatedWords.Add(word);
                                }
                            }
                        }
                    }
                }
            }

            return string.Join(", ", concatenatedWords);
        }
    }
}
