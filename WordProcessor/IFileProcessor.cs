using System.Collections.Generic;

namespace WordProcessor
{
    public interface IFileProcessor
    {
        IEnumerable<KeyValuePair<string, int>> GetWordCount();
        string GetConcatenatedWords(int wordSize = 6);
    }
}
