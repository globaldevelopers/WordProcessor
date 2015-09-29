using System;

namespace WordProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            IFileProcessor fileProcessor = new FileProcessor("fileinput.txt");

            var wordCount = fileProcessor.GetWordCount();

            Console.WriteLine("Word             Count");

            foreach (var word in wordCount)
            {
                Console.WriteLine($"{word.Key}              {word.Value}");
            }

            //fileProcessor = new FileProcessor("ConcateWords.txt");
            Console.WriteLine();
            Console.WriteLine("================= Concatenated words ===============");
            Console.WriteLine();

            Console.WriteLine(fileProcessor.GetConcatenatedWords());

            Console.ReadLine();
        }
    }
}
