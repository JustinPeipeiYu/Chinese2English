using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Security.Policy;

namespace Idiom_Translator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string stringSearchWord="";
        List<Word> listSearchWord;
        Dictionary<int, List<Word>> dictionarySearchWord;
        List<Word> listPageSearchWord;
        static string stringVowels = "aeiouü";
        static List<Word> listWordRecords;
        static List<Vowels> listVowelRecords;
        int pageCounter = 1;

        public MainWindow()
        {
            InitializeComponent();
            listWordRecords = methodReadWordsIntoList();
            listVowelRecords = methodReadVowelsIntoList();
        }

        private List<Word> methodFindWord(string searchWord)
        {
            //list of all hanyu characters with matching pinyin 
            List<Word> e = new List<Word>();
            //search all matching pinyin in List
            foreach (var record in listWordRecords)
            {
                //proceed if there is a matching pinyin in Record
                if (record.Pinyin.Equals(searchWord))
                {
                    //add pinyin's corresponding hanyu to List
                    e.Add(record);
                } 
            }
            //may return empty list
            return e;
        }

        //read 8105 entries from pinyin hanyu database
        private List<Word> methodReadWordsIntoList()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

            using (var reader = new StreamReader("list of pinyin hanyu.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<Word>();
                return records.ToList();
            }
        }

        //read 6 entries from tone on vowel database
        private List<Vowels> methodReadVowelsIntoList()
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

        private string methodUpdateToneVowel(string searchWord, int tone)
        {
            //get last letter
            string lastLetter = searchWord.Substring(searchWord.Length - 1, 1);
            //proceed if the letter is vowel
            if (stringVowels.Contains(lastLetter))
            {
                foreach (var record in listVowelRecords)
                {
                    //specify which vowel to tone
                    if (record.vowel.Equals(lastLetter))
                    {
                        //remove the last letter
                        searchWord = methodRemoveLastLetter(searchWord);
                        if (tone == 1) { searchWord = methodUpdateSearchWord(searchWord, record.vowelTone1); }
                        else if (tone == 2) { searchWord = methodUpdateSearchWord(searchWord, record.vowelTone2); }
                        else if (tone == 3) { searchWord = methodUpdateSearchWord(searchWord, record.vowelTone3); }
                        else if (tone == 4) { searchWord = methodUpdateSearchWord(searchWord, record.vowelTone4); }
                    }
                }
            }
            return searchWord;
        }
        private string methodRemoveLastLetter(string searchWord)
        {
            return searchWord.Substring(0, searchWord.Length - 1);
        }
        private string methodUpdateSearchWord(string searchWord, string letter)
        {
            searchWord += letter;
            lblPinyin.Content = searchWord;
            return searchWord;
        }
        private List<Word> methodSortWordList(List<Word> listSearchWord)
        {
            for (int i = 1; i < listSearchWord.Count; i++)
            {
                var key = listSearchWord[i];
                var k = Int32.TryParse(key.Frequency, out int keyFreq);
                if (!k) { keyFreq = 10000 + i; }
                var flag = 0;
                for (int j = i - 1; j >= 0 && flag != 1;j--)
                {
                    var adj = listSearchWord[j];
                    var a = Int32.TryParse(adj.Frequency, out int adjFreq);
                    if (!a) { adjFreq = 10000 + j; }
                    if (keyFreq < adjFreq)
                    {
                        listSearchWord[j+1] = adj;
                        listSearchWord[j] = key;
                    } else { flag = 1; }
                }
            }
            return listSearchWord;
        }
        private Dictionary<int, List<Word>> methodPopulateDictionarySearchWord(List<Word> listSearchWord)
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
        private List<Word> methodGetPageSearchWord(Dictionary<int, List<Word>> dictionarySearchWord, int index)
        {
            return dictionarySearchWord[index];
        }
        private void updateSuggestions(List<Word> listPageSearchWord)
        {
            btnSuggest1.Visibility = Visibility.Hidden;
            btnSuggest2.Visibility = Visibility.Hidden;
            btnSuggest3.Visibility = Visibility.Hidden;
            btnSuggest4.Visibility = Visibility.Hidden;
            btnSuggest5.Visibility = Visibility.Hidden;

            if (listPageSearchWord.Count == 1)
            {
                btnSuggest1.Visibility = Visibility.Visible;
                btnSuggest1.Content = listPageSearchWord[0].Frequency;
            }
            else if (listPageSearchWord.Count == 2)
            {
                btnSuggest1.Visibility = Visibility.Visible;
                btnSuggest2.Visibility = Visibility.Visible;
                btnSuggest1.Content = listPageSearchWord[0].Frequency;
                btnSuggest2.Content = listPageSearchWord[1].Frequency;
            }
            else if (listPageSearchWord.Count == 3)
            {
                btnSuggest1.Visibility = Visibility.Visible;
                btnSuggest2.Visibility = Visibility.Visible;
                btnSuggest3.Visibility = Visibility.Visible;
                btnSuggest1.Content = listPageSearchWord[0].Frequency;
                btnSuggest2.Content = listPageSearchWord[1].Frequency;
                btnSuggest3.Content = listPageSearchWord[2].Frequency;
            }
            else if (listPageSearchWord.Count == 4)
            {
                btnSuggest1.Visibility = Visibility.Visible;
                btnSuggest2.Visibility = Visibility.Visible;
                btnSuggest3.Visibility = Visibility.Visible;
                btnSuggest4.Visibility = Visibility.Visible;
                btnSuggest1.Content = listPageSearchWord[0].Frequency;
                btnSuggest2.Content = listPageSearchWord[1].Frequency;
                btnSuggest3.Content = listPageSearchWord[2].Frequency;
                btnSuggest4.Content = listPageSearchWord[3].Frequency;
            }
            else if (listPageSearchWord.Count == 5)
            {
                btnSuggest1.Visibility = Visibility.Visible;
                btnSuggest2.Visibility = Visibility.Visible;
                btnSuggest3.Visibility = Visibility.Visible;
                btnSuggest4.Visibility = Visibility.Visible;
                btnSuggest5.Visibility = Visibility.Visible;
                btnSuggest1.Content = listPageSearchWord[0].Frequency;
                btnSuggest2.Content = listPageSearchWord[1].Frequency;
                btnSuggest3.Content = listPageSearchWord[2].Frequency;
                btnSuggest4.Content = listPageSearchWord[3].Frequency;
                btnSuggest5.Content = listPageSearchWord[4].Frequency;
            }
        }
        private void btnSuggest1_Click(object sender, RoutedEventArgs e)
        {
            txtSource.Text = listPageSearchWord[0].Frequency;
        }

        private void btnSuggest5_Click(object sender, RoutedEventArgs e)
        {
            txtSource.Text = listPageSearchWord[4].Frequency;
        }

        private void btnSuggest4_Click(object sender, RoutedEventArgs e)
        {
            txtSource.Text = listPageSearchWord[3].Frequency;
        }

        private void btnSuggest3_Click(object sender, RoutedEventArgs e)
        {
            txtSource.Text = listPageSearchWord[2].Frequency;
        }

        private void btnSuggest2_Click(object sender, RoutedEventArgs e)
        {
            txtSource.Text = listPageSearchWord[1].Frequency;
        }

        private void btnQ_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "q");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnW_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "w");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnE_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "e");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnR_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "r");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnT_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "t");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnY_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "y");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnU_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "u");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnI_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "i");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnO_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "o");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnP_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "p");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnA_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "a");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnS_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "s");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnD_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "d");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnF_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "f");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnG_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "g");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnH_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "h");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnJ_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "j");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnK_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "k");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnL_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "l");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnZ_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "z");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnX_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "x");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "c");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnV_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "v");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnB_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "b");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnN_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "n");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnM_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "m");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnUdot_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "ü");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnExclamation_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "!");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnSpace_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, " ");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnComma_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, ",");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnPeriod_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, ".");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnQuestion_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "?");
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnTone1_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord=methodUpdateToneVowel(stringSearchWord,1);
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnTone2_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateToneVowel(stringSearchWord,2);
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnTone3_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateToneVowel(stringSearchWord,3);
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnTone4_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateToneVowel(stringSearchWord,4);
            listSearchWord = methodFindWord(stringSearchWord);
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnBackspace_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodRemoveLastLetter(stringSearchWord);
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "");
            listSearchWord = methodSortWordList(listSearchWord);
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            dictionarySearchWord = methodPopulateDictionarySearchWord(listSearchWord);
            listPageSearchWord = methodGetPageSearchWord(dictionarySearchWord, pageCounter);
            updateSuggestions(listPageSearchWord);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnConvert_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNextSuggest_Click(object sender, RoutedEventArgs e)
        {
            pageCounter++;
            if (pageCounter > listSearchWord.Count/5 + 1) { 
                pageCounter = listSearchWord.Count / 5 + 1; 
            }
            listPageSearchWord = methodGetPageSearchWord(dictionarySearchWord, pageCounter);
            updateSuggestions(listPageSearchWord);
        }

        private void btnBackSuggest_Click(object sender, RoutedEventArgs e)
        {
            pageCounter--;
            if (pageCounter < 1) { 
                pageCounter = 1; 
            }
            listPageSearchWord = methodGetPageSearchWord(dictionarySearchWord, pageCounter);
            updateSuggestions(listPageSearchWord);
        }
    }
}
