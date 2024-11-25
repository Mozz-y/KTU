using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Program
{
    // Main program entry point
    internal class Program
    {
        static void Main(string[] args)
        {
            // Define file paths for input and output files
            const string CFd1 = "Knyga1.txt";  // First book file
            const string CFd2 = "Knyga2.txt";  // Second book file
            const string CFr = "Rodikliai.txt"; // Output file for word counts
            const string CFm = "ManoKnyga.txt"; // Output file for merged text
            const string CFrezultatai = "rezultatai.txt"; // Output file for contents of Knyga1 and Knyga2

            // Task 0: Output contents of Knyga1.txt and Knyga2.txt to rezultatai.txt
            OutputWriter.OutputFilesContents(CFd1, CFd2, CFrezultatai);

            // Create instances of utility classes
            PunctuationStore punctuationStore = new PunctuationStore();
            WordCounter wordCounter = new WordCounter(punctuationStore);
            SentenceAnalyzer sentenceAnalyzer = new SentenceAnalyzer(punctuationStore);
            TextMerger textMerger = new TextMerger(punctuationStore);

            // Task 1: Find and output unique words in Knyga2.txt not in Knyga1.txt
            Dictionary<string, int> uniqueWords = wordCounter.FindUniqueWords(CFd1, CFd2);
            OutputWriter.WriteWordTable(uniqueWords, CFr);

            // Task 2: Find the longest sentence in each file
            var analysis1 = sentenceAnalyzer.Analyze(CFd1);
            var analysis2 = sentenceAnalyzer.Analyze(CFd2);
            OutputWriter.WriteLongestSentences(analysis1, analysis2, CFr);

            // Task 3: Merge texts into ManoKnyga.txt
            textMerger.MergeFiles(CFd1, CFd2, CFm);
        }
    }

    // Class to store punctuation marks that we want to recognize in text
    class PunctuationStore
    {
        // A set of punctuation characters
        public HashSet<char> Punctuation { get; }

        // Constructor: Initializes the set of punctuation characters
        public PunctuationStore()
        {
            // Populate with a set of commonly used punctuation marks
            Punctuation = new HashSet<char>(".,!?;:(){}[]\"'-„“”–");
        }
    }

    // Class responsible for counting and finding unique words
    class WordCounter
    {
        private readonly PunctuationStore _punctuationStore;

        // Constructor: Takes an instance of PunctuationStore
        public WordCounter(PunctuationStore punctuationStore)
        {
            _punctuationStore = punctuationStore;
        }

        // Method to find unique words in second file (file2) not found in first file (file1)
        public Dictionary<string, int> FindUniqueWords(string file1, string file2)
        {
            // Get all words in file1
            HashSet<string> wordsInFile1 = GetWords(file1);

            // Dictionary to store words and their counts from file2
            Dictionary<string, int> wordCounts = new Dictionary<string, int>();

            // Read through file2 to find words not in file1
            using (var reader = new StreamReader(file2, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    foreach (var word in ExtractWords(line))
                    {
                        // If word is not in file1, count it
                        if (!wordsInFile1.Contains(word))
                        {
                            if (!wordCounts.ContainsKey(word))
                                wordCounts[word] = 0;
                            wordCounts[word]++;
                        }
                    }
                }
            }

            // Sort the words by frequency and alphabetically
            return SortByCountAndAlphabetical(wordCounts);
        }

        // Helper method to get all words from a given file
        private HashSet<string> GetWords(string file)
        {
            HashSet<string> words = new HashSet<string>();
            using (var reader = new StreamReader(file, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    foreach (var word in ExtractWords(line))
                        words.Add(word); // Add word to set (set ensures uniqueness)
                }
            }
            return words;
        }

        // Extract words from a line of text (non-letter characters are skipped)
        private IEnumerable<string> ExtractWords(string line)
        {
            StringBuilder word = new StringBuilder();
            foreach (char c in line)
            {
                if (char.IsLetter(c)) // Append letter characters to the word
                {
                    word.Append(c);
                }
                else if (word.Length > 0) // Non-letter character: return the current word
                {
                    yield return word.ToString().ToLower(); // Return word in lowercase
                    word.Clear(); // Reset for the next word
                }
            }
            if (word.Length > 0) // Return the last word if it exists
                yield return word.ToString().ToLower();
        }

        // Sort the words first by frequency (descending), then alphabetically
        private Dictionary<string, int> SortByCountAndAlphabetical(Dictionary<string, int> wordCounts)
        {
            var sorted = new List<KeyValuePair<string, int>>(wordCounts);
            sorted.Sort((a, b) =>
            {
                // Compare by count first
                int compareCount = b.Value.CompareTo(a.Value);
                return compareCount != 0 ? compareCount : string.Compare(a.Key, b.Key, StringComparison.Ordinal);
            });

            // Rebuild dictionary with sorted order
            var result = new Dictionary<string, int>();
            foreach (var kvp in sorted)
                result[kvp.Key] = kvp.Value;

            return result;
        }
    }

    // Class responsible for analyzing sentences in a file
    class SentenceAnalyzer
    {
        private readonly PunctuationStore _punctuationStore;

        // Constructor: Takes an instance of PunctuationStore
        public SentenceAnalyzer(PunctuationStore punctuationStore)
        {
            _punctuationStore = punctuationStore;
        }

        // Analyze the file to find the longest sentence
        public (string Sentence, int WordCount, int CharCount, int LineNumber) Analyze(string file)
        {
            string longestSentence = string.Empty;
            int maxWordCount = 0;
            int maxCharCount = 0;
            int lineNumber = 0;

            // Read the file line by line
            using (var reader = new StreamReader(file, Encoding.UTF8))
            {
                string line;
                int currentLineNumber = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    currentLineNumber++;
                    foreach (var sentence in ExtractSentences(line))
                    {
                        // Count words and characters in each sentence
                        int wordCount = CountWords(sentence);
                        int charCount = sentence.Length;

                        // Update if this sentence is the longest one encountered
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

            // Return the longest sentence with its word and character count, and the line number
            return (longestSentence, maxWordCount, maxCharCount, lineNumber);
        }

        // Split the line into sentences based on punctuation marks
        private IEnumerable<string> ExtractSentences(string line)
        {
            return Regex.Split(line, @"(?<=[.!?])"); // Split by period, exclamation mark, or question mark
        }

        // Count the number of words in a sentence
        private int CountWords(string sentence)
        {
            int count = 0;
            bool inWord = false;

            // Loop through each character in the sentence
            foreach (char c in sentence)
            {
                if (char.IsLetter(c)) // Letter characters are part of words
                {
                    if (!inWord) // New word starts
                        count++;
                    inWord = true;
                }
                else
                {
                    inWord = false; // Non-letter characters break the word
                }
            }
            return count;
        }
    }

    // Class responsible for merging two text files while alternating words from each
    class TextMerger
    {
        private readonly PunctuationStore _punctuationStore;

        // Constructor: Takes an instance of PunctuationStore
        public TextMerger(PunctuationStore punctuationStore)
        {
            _punctuationStore = punctuationStore;
        }

        // Method to merge two files into one, alternating words from each file
        public void MergeFiles(string file1, string file2, string output)
        {
            // Get the set of words from both files
            HashSet<string> wordsInFile1 = GetWords(file1);
            HashSet<string> wordsInFile2 = GetWords(file2);

            using (var writer = new StreamWriter(output, false, Encoding.UTF8))
            using (var reader1 = new StreamReader(file1, Encoding.UTF8))
            using (var reader2 = new StreamReader(file2, Encoding.UTF8))
            {
                bool switchToSecondFile = false;
                string word1 = GetNextWord(reader1);
                string word2 = GetNextWord(reader2);

                // While there are words left in either file, keep merging
                while (word1 != null || word2 != null)
                {
                    if (!switchToSecondFile && word1 != null)
                    {
                        writer.Write(word1 + " "); // Write word from file1
                        switchToSecondFile = !wordsInFile2.Contains(word1); // Switch if word is not in file2
                        word1 = GetNextWord(reader1); // Get the next word from file1
                    }
                    else if (word2 != null)
                    {
                        writer.Write(word2 + " "); // Write word from file2
                        switchToSecondFile = wordsInFile1.Contains(word2); // Switch if word is in file1
                        word2 = GetNextWord(reader2); // Get the next word from file2
                    }
                }
            }
        }

        // Helper method to extract all words from a given file
        private HashSet<string> GetWords(string file)
        {
            var words = new HashSet<string>();
            using (var reader = new StreamReader(file, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    foreach (var word in ExtractWords(line))
                        words.Add(word); // Add each word to set
                }
            }
            return words;
        }

        // Helper method to extract individual words from a line of text
        private IEnumerable<string> ExtractWords(string line)
        {
            StringBuilder word = new StringBuilder();
            foreach (char c in line)
            {
                if (char.IsLetter(c))
                    word.Append(c); // Build word from letters
                else if (word.Length > 0)
                {
                    yield return word.ToString().ToLower(); // Return the built word in lowercase
                    word.Clear(); // Reset for the next word
                }
            }
            if (word.Length > 0)
                yield return word.ToString().ToLower(); // Return the last word if any
        }

        // Helper method to get the next word from a file reader
        private string GetNextWord(StreamReader reader)
        {
            StringBuilder word = new StringBuilder();
            while (reader.Peek() != -1) // While there are characters left to read
            {
                char c = (char)reader.Read(); // Read the next character
                if (char.IsLetter(c)) // Letter character is part of the word
                {
                    word.Append(c);
                }
                else if (word.Length > 0)
                {
                    return word.ToString().ToLower(); // Return the complete word
                }
            }
            return word.Length > 0 ? word.ToString().ToLower() : null; // Return the last word or null if none
        }
    }

    // Helper class for writing results to output files
    class OutputWriter
    {
        // Task 0: Method to output contents of Knyga1.txt and Knyga2.txt to rezultatai.txt
        public static void OutputFilesContents(string file1, string file2, string outputFile)
        {
            using (var writer = new StreamWriter(outputFile, false, Encoding.UTF8))
            {
                // Write contents of Knyga1.txt
                writer.WriteLine($"{Path.GetFileName(file1)}:");
                writer.WriteLine(File.ReadAllText(file1));
                writer.WriteLine();

                // Write contents of Knyga2.txt
                writer.WriteLine($"{Path.GetFileName(file2)}:");
                writer.WriteLine(File.ReadAllText(file2));
            }
        }

        // Method to write the word count table to a file
        public static void WriteWordTable(Dictionary<string, int> wordCounts, string outputFile)
        {
            using (var writer = new StreamWriter(outputFile, false, Encoding.UTF8))
            {
                if (wordCounts.Count == 0) // Check if the dictionary is empty
                {
                    writer.WriteLine("Unikalių žodžių tarp failų nėra.");
                }
                else
                {
                    writer.WriteLine("Žodis              Kiekis"); // Column headers
                    writer.WriteLine("--------------------------");
                    foreach (var kvp in wordCounts) // Iterate through word counts and write them
                    {
                        writer.WriteLine($"{kvp.Key.PadRight(20)}{kvp.Value}");
                    }
                }
            }
        }


        // Method to write the longest sentences from both files to the output file
        public static void WriteLongestSentences((string Sentence, int WordCount, int CharCount, int LineNumber) analysis1,
            (string Sentence, int WordCount, int CharCount, int LineNumber) analysis2, string outputFile)
        {
            using (var writer = new StreamWriter(outputFile, true, Encoding.UTF8))
            {
                writer.WriteLine();
                writer.WriteLine("Ilgiausia sakinys iš Knyga1:");
                writer.WriteLine($"Sakinys: {analysis1.Sentence}"); // Write sentence from Knyga1
                writer.WriteLine($"Žodžių kiekis: {analysis1.WordCount}, Simbolių kiekis: {analysis1.CharCount}, Eilutė: {analysis1.LineNumber}");

                writer.WriteLine();
                writer.WriteLine("Ilgiausia sakinys iš Knyga2:");
                writer.WriteLine($"Sakinys: {analysis2.Sentence}"); // Write sentence from Knyga2
                writer.WriteLine($"Žodžių kiekis: {analysis2.WordCount}, Simbolių kiekis: {analysis2.CharCount}, Eilutė: {analysis2.LineNumber}");
            }
        }
    }
}
