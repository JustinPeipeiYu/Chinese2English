using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Idiom_Translator
{
    internal class Methods
    {
        /*
         * convert a search string to list of Words
         */
        public static List<Word> stringToList(string searchWord,List<Word> wordRecords)
        {
            List<Word> e = new List<Word>();

            foreach (var record in wordRecords)
            {
                if (record.Pinyin.Equals(searchWord))
                {
                    e.Add(record);
                }
            }

            return e;
        }
        /* 
         * Return an ordered dictionary and the list of Words under the pageCounter key
         */
        public static Tuple<Dictionary<int, List<Word>>,List<Word>> searchWord(string pinyin, int pageCounter, List<Word> wordRecords)
        {
            List<Word> longWordList = new List<Word>();
            Dictionary<int, List<Word>> wordDict = new Dictionary<int, List<Word>>();
            List<Word> shortWordList = new List<Word>();

            if (pinyin.Length > 1)
            {
                longWordList = Methods.stringToList(pinyin, wordRecords);
                longWordList = Methods.sortList(longWordList);
                wordDict = makeDict(longWordList);

                if (wordDict.Count != 0)
                {
                    shortWordList = keyList(wordDict, pageCounter);
                }

                return new Tuple<Dictionary<int, List <Word>>, List<Word>>(wordDict, shortWordList);
            }

            return new Tuple<Dictionary<int, List<Word>>, List<Word>>(wordDict, shortWordList);
        }

        /*
         * create a search string by adding together letters
         */
        public static string buildString(string searchWord, string letter)
        {
            searchWord += letter;
            return searchWord;
        }
        /*
         * replace the last vowel in search string with a toned vowel
         */
        public static string replaceVowel(string searchWord, int tone, List<Vowels> vowelRecords)
        {
            string lastLetter = searchWord.Substring(searchWord.Length - 1, 1);

            if (MainWindow.vowels.Contains(lastLetter))
            {
                foreach (var record in vowelRecords)
                {
                    if (record.vowel.Equals(lastLetter))
                    {
                        searchWord = removeLastLetter(searchWord);
                        if (tone == 1) { searchWord = buildString(searchWord, record.vowelTone1); }
                        else if (tone == 2) { searchWord = buildString(searchWord, record.vowelTone2); }
                        else if (tone == 3) { searchWord = buildString(searchWord, record.vowelTone3); }
                        else if (tone == 4) { searchWord = buildString(searchWord, record.vowelTone4); }
                    }
                }
            }
            return searchWord;
        }

        /*
         * remove the last letter in a search string
         */
        public static string removeLastLetter(string searchWord)
        {
            return searchWord.Substring(0, searchWord.Length - 1);
        }

        /*sort a list of Words by increasing frequency
         */
        public static List<Word> sortList(List<Word> listSearchWord)
        {
            for (int i = 1; i < listSearchWord.Count; i++)
            {
                var key = listSearchWord[i];
                var k = Int32.TryParse(key.Frequency, out int keyFreq);

                if (!k) { 
                    keyFreq = 10000 + i; 
                }

                var flag = 0;
                
                for (int j = i - 1; j >= 0 && flag != 1; j--)
                {
                    var adj = listSearchWord[j];
                    var a = Int32.TryParse(adj.Frequency, out int adjFreq);
                    
                    if (!a) { 
                        adjFreq = 10000 + j; 
                    }
                    
                    if (keyFreq < adjFreq)
                    {
                        listSearchWord[j + 1] = adj;
                        listSearchWord[j] = key;
                    }
                    else { 
                        flag = 1; 
                    }
                }
            }

            return listSearchWord;
        }
        /*
         * Convert a list of Words to a dictionary with a maximum of 5 Words per key
         */
        public static Dictionary<int, List<Word>> makeDict(List<Word> listSearchWord)
        {
            Dictionary<int, List<Word>> dictionarySearchWord = new Dictionary<int, List<Word>>();

            if (listSearchWord.Count <= 5)
            {
                dictionarySearchWord.Add(1, listSearchWord);
            }
            else if (listSearchWord.Count > 5 && listSearchWord.Count <= 10)
            {
                dictionarySearchWord.Add(1, listSearchWord.GetRange(0, 5));
                dictionarySearchWord.Add(2, listSearchWord.GetRange(5, listSearchWord.Count - 5));
            }
            else if (listSearchWord.Count > 10 && listSearchWord.Count <= 15)
            {
                dictionarySearchWord.Add(1, listSearchWord.GetRange(0, 5));
                dictionarySearchWord.Add(2, listSearchWord.GetRange(5, 5));
                dictionarySearchWord.Add(3, listSearchWord.GetRange(10, listSearchWord.Count - 10));
            }
            else if (listSearchWord.Count > 15 && listSearchWord.Count <= 20)
            {
                dictionarySearchWord.Add(1, listSearchWord.GetRange(0, 5));
                dictionarySearchWord.Add(2, listSearchWord.GetRange(5, 5));
                dictionarySearchWord.Add(3, listSearchWord.GetRange(10, 5));
                dictionarySearchWord.Add(4, listSearchWord.GetRange(15, listSearchWord.Count - 15));
            }
            else if (listSearchWord.Count > 20 && listSearchWord.Count <= 25)
            {
                dictionarySearchWord.Add(1, listSearchWord.GetRange(0, 5));
                dictionarySearchWord.Add(2, listSearchWord.GetRange(5, 5));
                dictionarySearchWord.Add(3, listSearchWord.GetRange(10, 5));
                dictionarySearchWord.Add(4, listSearchWord.GetRange(15, 5));
                dictionarySearchWord.Add(4, listSearchWord.GetRange(20, listSearchWord.Count - 20));
            }

            return dictionarySearchWord;
        }

        /*
         return the list of Words under key index
         */
        public static List<Word> keyList(Dictionary<int, List<Word>> dictionarySearchWord, int index)
        {
            return dictionarySearchWord[index];
        }
        
        /*
        * read 8105 rows of csv into 8105 Words
        */
        public static List<Word> readWordCSV()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

            using (var reader = new StreamReader("pinyin hanzi.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<Word>();
                return records.ToList();
            }
        }

        /*
         read 6 rows in csv and store in 6 Vowels
         */
        public static List<Vowels> readVowelCSV()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
            using (var reader = new StreamReader("tones on vowels.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                var tonedVowels = csv.GetRecords<Vowels>();
                return tonedVowels.ToList();
            }
        }

    }
}
