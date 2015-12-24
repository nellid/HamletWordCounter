using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HamletWordCounter
{
    class HamletWordCounterMain
    {
        static void Main(string[] args)
        {
            // Establist the expected 
            string[] expected = {"the", "and", "to", "of", "i", "you", "a", "my", "hamlet", "in"};

            string[] answer = count_words();

            Console.WriteLine("Most Frequent Words...");
            Console.WriteLine("['" + answer[0] + "', '" + answer[1] + "', '" + answer[2] + "', '" + answer[3] + "', '" + answer[4] + "', '" + answer[5] + "', '" + answer[6] + "', '" + answer[7] + "', '" + answer[8] + "', '" + answer[9] + "']");
            
            // If the answer equals the expected answer ...
            if (expected.OrderBy(a => a).SequenceEqual(answer.OrderBy(a => a)))
            {
                Console.WriteLine("SUCCESS!");
            }

            Console.Read();
        }

        static string[] count_words()
        {
            // A collection of key/value pairs that will keep track of words in 
            // hamlet.txt and the number of times each word appears in the file
            Dictionary<string, int> hamletPlayFull = new Dictionary<string, int>();

            // An array that will contain all the words in the file
            string[] hamletPlayString = { "" };

            try
            {
                // Create a new instance of the StreamReader class to read the contents of the hamlet.txt file
                using (StreamReader hamletTextFileReader = new StreamReader("hamlet.txt"))
                {
                    // Read the entire file, splitting on null (white space) and storing the words in an array
                    hamletPlayString = hamletTextFileReader.ReadToEnd().Split(null);
                }
            }
            // If something goes wrong reading the text file, print out the error for the user
            catch (Exception e)
            {
                Console.WriteLine("Hamlet.txt could not be read because " + e.Message);
            }

            // For each word in hamlet.text: strip punctuation, make lowercase, check if it is
            // already in the dictionary (if so, add 1 to its value, if not, add it to the dictionary
            // with a value of 1)
            foreach (string s in hamletPlayString)
            {
                // Make the string lowercase
                string lowercaseString = s.ToLower();

                // Remove any leading and trailing whitespace characters from the string
                lowercaseString = lowercaseString.Trim();

                // Use a StringBuilder to collect the parts of words that are not punctuation
                StringBuilder punctuationStrip = new StringBuilder();

                // For each character in a string, keep only the parts that are not punctuation
                foreach (char c in lowercaseString)
                {
                    if (!char.IsPunctuation(c))
                    {
                        punctuationStrip.Append(c);
                    }
                }

                // The resulting punctuation-stripped string
                string strippedString = punctuationStrip.ToString();

                // If the punctuation-stripped string is not white space, see if it is in the dictionary
                if (!String.IsNullOrEmpty(strippedString))
                {
                    // If the dictionary already contains that word, add one to its count
                    if (hamletPlayFull.ContainsKey(strippedString))
                    {
                        hamletPlayFull[strippedString] += 1;
                    }
                    // Otherwise, add the word to the dictionary with a count of 1
                    else
                    {
                        hamletPlayFull.Add(strippedString, 1);
                    }
                }
            }

            // Copy the dictionary to a sortable list
            List<KeyValuePair<string, int>> hamletSortedByFrequency = hamletPlayFull.ToList();

            // Sort the list in descending order
            hamletSortedByFrequency.Sort((x, y) => y.Value.CompareTo(x.Value));

            // Create an array to return to the main method
            string[] hamletTopTenWords = new string[10];

            // Copy the list's top ten values into the array
            for (int i = 0; i < 10; i++)
            {
                hamletTopTenWords[i] = hamletSortedByFrequency[i].Key;
            }

            return hamletTopTenWords;
        }
    }
}
