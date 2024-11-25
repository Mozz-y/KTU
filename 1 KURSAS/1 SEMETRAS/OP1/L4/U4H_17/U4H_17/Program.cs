using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Define file names to be used
            const string CFd1 = "Knyga1.txt";  // First input text file
            const string CFd2 = "Knyga2.txt";  // Second input text file
            const string CFr = "Rodikliai.txt"; // Output file for word frequency counts
            const string CFm = "ManoKnyga.txt"; // Output file for merged text from the two books
            const string CFrezultatai = "rezultatai.txt"; // Output file for the contents of both books

            // Task 0: Write the contents of both text files to a result file
            OutputWriter.OutputFilesContents(CFd1, CFd2, CFrezultatai);

            // Create instances of utility classes for various tasks
            PunctuationStore punctuationStore = new PunctuationStore(); // Handles punctuation-related logic
            WordCounter wordCounter = new WordCounter();               // Counts words and finds unique ones
            SentenceAnalyzer sentenceAnalyzer = new SentenceAnalyzer(); // Analyzes sentences for length and word count
            TextMerger textMerger = new TextMerger();                  // Merges text files alternately by word

            // Task 1: Find and output unique words in the second file not present in the first
            Dictionary<string, int> uniqueWords = wordCounter.FindUniqueWords(CFd1, CFd2);
            OutputWriter.WriteWordTable(uniqueWords, CFr);

            // Task 2: Identify the longest sentence in each file and output the results
            var analysis1 = sentenceAnalyzer.Analyze(CFd1);
            var analysis2 = sentenceAnalyzer.Analyze(CFd2);
            OutputWriter.WriteLongestSentences(analysis1, analysis2, CFr);

            // Task 3: Merge the two text files into a single file, alternating words from each
            textMerger.MergeFiles(CFd1, CFd2, CFm);
        }
    }

    // Stores a set of common punctuation marks to be handled in text parsing
    class PunctuationStore
    {
        public HashSet<char> Punctuation { get; }

        public PunctuationStore()
        {
            // Initialize the set with common punctuation characters
            Punctuation = new HashSet<char>(".,!?;:(){}[]\"'-„“”–");
        }
    }

    // Provides reusable helper methods for text processing
    class Helper
    {
        // Reads a file and extracts all unique words into a HashSet
        public static HashSet<string> GetWords(string file)
        {
            var words = new HashSet<string>(); // Stores unique words
            using (var reader = new StreamReader(file, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    foreach (var word in ExtractWords(line)) // Extract words from each line
                        words.Add(word);
                }
            }
            return words;
        }

        // Extracts individual words from a line of text
        public static IEnumerable<string> ExtractWords(string line)
        {
            StringBuilder word = new StringBuilder(); // Temporary buffer for building words
            foreach (char c in line)
            {
                if (char.IsLetter(c)) // If the character is a letter, add it to the current word
                    word.Append(c);
                else if (word.Length > 0) // If a non-letter ends a word, yield the word
                {
                    yield return word.ToString().ToLower(); // Return lowercase word
                    word.Clear(); // Clear the buffer
                }
            }
            if (word.Length > 0) // Return the final word if one remains
                yield return word.ToString().ToLower();
        }

        // Sorts a dictionary by word frequency and alphabetically
        public static Dictionary<string, int> SortByCountAndAlphabetical(Dictionary<string, int> wordCounts)
        {
            var sorted = new List<KeyValuePair<string, int>>(wordCounts);
            sorted.Sort((a, b) =>
            {
                int compareCount = b.Value.CompareTo(a.Value); // Compare by count (descending)
                return compareCount != 0 ? compareCount : string.Compare(a.Key, b.Key, StringComparison.Ordinal);
            });

            // Rebuild a sorted dictionary
            var result = new Dictionary<string, int>();
            foreach (var kvp in sorted)
                result[kvp.Key] = kvp.Value;

            return result;
        }

    }

    // Handles word counting and identification of unique words between files
    class WordCounter
    {
        // Finds words that are unique to the second file compared to the first
        public Dictionary<string, int> FindUniqueWords(string file1, string file2)
        {
            HashSet<string> wordsInFile1 = Helper.GetWords(file1); // Get all words from the first file
            Dictionary<string, int> wordCounts = new Dictionary<string, int>();

            using (var reader = new StreamReader(file2, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    foreach (var word in Helper.ExtractWords(line))
                    {
                        if (!wordsInFile1.Contains(word)) // If the word is not in the first file
                        {
                            if (!wordCounts.ContainsKey(word))
                                wordCounts[word] = 0;
                            wordCounts[word]++;
                        }
                    }
                }
            }

            // Sort words by count (descending) and then alphabetically
            return Helper.SortByCountAndAlphabetical(wordCounts);
        }
    }

    // Analyzes sentences in a text file to find the longest one
    class SentenceAnalyzer
    {
        public (string Sentence, int WordCount, int CharCount, int LineNumber) Analyze(string file)
        {
            string longestSentence = string.Empty; // Store the longest sentence
            int maxWordCount = 0;                 // Maximum word count in a sentence
            int maxCharCount = 0;                 // Maximum character count in a sentence
            int lineNumber = 0;                   // Line number of the longest sentence

            using (var reader = new StreamReader(file, Encoding.UTF8))
            {
                string line;
                int currentLineNumber = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    currentLineNumber++;
                    foreach (var sentence in ExtractSentences(line)) // Split lines into sentences
                    {
                        int wordCount = CountWords(sentence);
                        int charCount = sentence.Length;

                        // Update if this sentence is longer
                        if (wordCount > maxWordCount)
                        {
                            longestSentence = sentence;
                            maxWordCount = wordCount;
                            maxCharCount = charCount;
                            lineNumber = currentLineNumber;
                        }
                    }
                }
            }

            return (longestSentence, maxWordCount, maxCharCount, lineNumber);
        }

        // Splits a line into individual sentences based on punctuation
        private IEnumerable<string> ExtractSentences(string line)
        {
            return Regex.Split(line, @"(?<=[.!?])"); // Regex for sentence-ending punctuation
        }

        // Counts the number of words in a sentence
        private int CountWords(string sentence)
        {
            int count = 0;
            bool inWord = false;

            foreach (char c in sentence)
            {
                if (char.IsLetter(c))
                {
                    if (!inWord) // Start of a new word
                        count++;
                    inWord = true;
                }
                else
                {
                    inWord = false; // Non-letter marks the end of a word
                }
            }
            return count;
        }
    }

    // Merges two files by alternating words from each file
    class TextMerger
    {
        public void MergeFiles(string file1, string file2, string output)
        {
            using (var writer = new StreamWriter(output, false, Encoding.UTF8))
            using (var reader1 = new StreamReader(file1, Encoding.UTF8))
            using (var reader2 = new StreamReader(file2, Encoding.UTF8))
            {
                bool switchToSecondFile = false; // Toggle between files
                string word1 = GetNextWord(reader1);
                string word2 = GetNextWord(reader2);

                while (word1 != null || word2 != null) // While there are words in either file
                {
                    if (!switchToSecondFile && word1 != null)
                    {
                        writer.Write(word1 + " ");
                        word1 = GetNextWord(reader1); // Fetch next word from file1
                        switchToSecondFile = true;
                    }
                    else if (word2 != null)
                    {
                        writer.Write(word2 + " ");
                        word2 = GetNextWord(reader2); // Fetch next word from file2
                        switchToSecondFile = false;
                    }
                }
            }
        }

        // Reads the next word from a file, skipping non-letters
        private string GetNextWord(StreamReader reader)
        {
            StringBuilder word = new StringBuilder();
            while (reader.Peek() != -1) // While not EOF
            {
                char c = (char)reader.Read();
                if (char.IsLetter(c))
                {
                    word.Append(c);
                }
                else if (word.Length > 0)
                {
                    return word.ToString().ToLower(); // Return word in lowercase
                }
            }
            return word.Length > 0 ? word.ToString().ToLower() : null; // Return last word, if any
        }
    }

    // Handles writing results and outputs to files
    class OutputWriter
    {
        // Writes the contents of two files into a single output file
        public static void OutputFilesContents(string file1, string file2, string outputFile)
        {
            using (var writer = new StreamWriter(outputFile, false, Encoding.UTF8))
            {
                writer.WriteLine($"{Path.GetFileName(file1)}:");
                writer.WriteLine(File.ReadAllText(file1));
                writer.WriteLine();

                writer.WriteLine($"{Path.GetFileName(file2)}:");
                writer.WriteLine(File.ReadAllText(file2));
            }
        }

        // Writes a formatted table of word counts to an output file
        public static void WriteWordTable(Dictionary<string, int> wordCounts, string outputFile)
        {
            using (var writer = new StreamWriter(outputFile, false, Encoding.UTF8))
            {
                if (wordCounts.Count == 0)
                {
                    writer.WriteLine("Unikalių žodžių tarp failų nėra.");
                }
                else
                {
                    // Write header for the table
                    writer.WriteLine("{0,-20} {1,10}", "Žodis", "Kiekis");
                    writer.WriteLine(new string('-', 30)); // Separator line for clarity

                    // Write each word and its count, aligned in a table format
                    foreach (var kvp in wordCounts)
                    {
                        writer.WriteLine("{0,-20} {1,10}", kvp.Key, kvp.Value);
                    }
                }
            }
        }

        // Writes the longest sentence information from two files to an output file
        public static void WriteLongestSentences((string Sentence, int WordCount, int CharCount, int LineNumber) analysis1,
                                                 (string Sentence, int WordCount, int CharCount, int LineNumber) analysis2,
                                                 string outputFile)
        {
            using (var writer = new StreamWriter(outputFile, true, Encoding.UTF8))
            {
                writer.WriteLine("Ilgiausios sakiniai kiekviename faile:");
                writer.WriteLine($"Knyga1: \"{analysis1.Sentence}\" (žodžių: {analysis1.WordCount}, simbolių: {analysis1.CharCount}, eilutė: {analysis1.LineNumber})");
                writer.WriteLine($"Knyga2: \"{analysis2.Sentence}\" (žodžių: {analysis2.WordCount}, simbolių: {analysis2.CharCount}, eilutė: {analysis2.LineNumber})");
            }
        }
    }
}
