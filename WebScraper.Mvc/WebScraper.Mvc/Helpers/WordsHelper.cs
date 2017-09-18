using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using WebScraper.Mvc.Models;

namespace WebScraper.Mvc.Helpers
{
    /// <summary>
    /// This class contain methods that can help when grouping and parsing words
    /// </summary>
    public static class WordsHelper
    {
       
        /// <summary>
        /// Get list of texts from webpage- no html 
        /// Group words and occurrences
        /// Sort grouped words by occurrence -" desc
        /// </summary>
        /// <param name="text">Text to be parsed and grouped</param>
        /// <param name="arrExcludeWords">Words to be excluded</param>
        /// <param name="minimumWordLengthAllowed">Minimum word length allowed</param>
        /// <param name="top">Number of common words to return</param>
        public static void GroupWordsByOccurrences(WebContent content, string[] arrExcludeWords, int minimumWordLengthAllowed = 0, int top = 10)
        {

            //Remove HTML tags
            RemoveHtmlTags(content);

            //Remove Special characters
            content.RawText = ReplaceSpecialCharactersWithDelimeter(content.RawText);

            //Split words into array of strings
            content.AllWords = content.RawText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            //Group words by occurrence
            List<KeyValuePair<string, int>> words = MapWordsAndOccurrences(content.AllWords, arrExcludeWords, minimumWordLengthAllowed).ToList();

            //Sort list of words by number of occurrences in descending order 
            words.Sort((nextPair, firstPair) =>
            {
                return firstPair.Value.CompareTo(nextPair.Value);
            }
            );

            //Set top words
            content.TopWords = words.Take(top).ToList();
        }

        /// <summary>
        /// Maps word and record number of occurrences
        /// We are using dictionary because it conviniently allow use of generic key/value pair
        /// </summary>
        /// <param name="texts"></param>
        /// <returns>a dictionary<string, int> is return</returns>
        public static IDictionary<string, int> MapWordsAndOccurrences(IEnumerable<string> words, IEnumerable<string> arrExcludeWords, int minWordLengthAllowed = 0)
        {
            IDictionary<string, int> groupedWords = new Dictionary<string, int>();

            foreach (var word in words)
            {
                //Exlcude words in exclude word list or word with length small that specified
                if (arrExcludeWords.Contains(word) || word.Length < minWordLengthAllowed)
                {
                    continue;
                }

                //Check if word already stored
                var existingWord = groupedWords.Where(d => d.Key == word);

                //Add new word if not in dictionary of words
                if (existingWord == null || !existingWord.Any())
                {
                    groupedWords.Add(new KeyValuePair<string, int>(word, 1));
                }
                else
                {
                    //else increment occurrence
                    groupedWords[word]++;
                }
            }
            return groupedWords;
        }

        /// <summary>
        /// Replace special characters with space as easy delimeter and convert characters to lower case
        /// </summary>
        /// <param name="text">String to be processed</param>
        /// <param name="delimeter">A character delimeter</param>
        /// <returns>String of text</returns>
        public static string ReplaceSpecialCharactersWithDelimeter(string text, char delimeter = ' ')
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in text)
            {
                if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == ' ' || c == '\'')
                {
                    //
                    sb.Append(char.ToLower(c));
                }
                else
                {
                    //Replace char with space as easy delimeter
                    sb.Append(delimeter);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Removes all HTML tags
        /// </summary>
        /// <param name="text">String to be processed</param>
        /// <returns></returns>
        public static string RemoveHtmlTags(WebContent content)
        {
            return Regex.Replace(content.RawText, "<.*?>", String.Empty, RegexOptions.Singleline);
        }
    }
}