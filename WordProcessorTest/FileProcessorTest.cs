using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordProcessor;

namespace WordProcessorTest
{
    [TestClass]
    public class FileProcessorTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "File name: is invalid.")]
        public void FileProcessorMustBeConstructedWithValidFileName()
        {
            IFileProcessor processor = new FileProcessor("");
            processor.GetWordCount();
        }

        [TestMethod]
        public void ProcessFileReturnsWordCount()
        {
            IFileProcessor processor = new FileProcessor("fileinput.txt");

            var output = processor.GetWordCount();

            Assert.AreEqual("Alice", output.FirstOrDefault(a => a.Key == "Alice").Key);
        }

        [TestMethod]
        public void ProcessFileForConcatenatedWordsReturnsUniqueWords()
        {
            string sampleFile = "ConcateWords.txt";

            string expectedResult = "albums, barely, befoul, convex, hereby, jigsaw, tailor, weaver";


            IFileProcessor processor = new FileProcessor(sampleFile);

            var output = processor.GetConcatenatedWords();

            Assert.AreEqual(expectedResult, output);

        }
    }
}
