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
            listWordRecords = readWordCSV();
            listVowelRecords = readVowelCSV();
            removeButtons();
        }

        private List<Word> stringToList(string searchWord)
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
        private List<Word> readWordCSV()
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
        private List<Vowels> readVowelCSV()
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

        private string replaceVowel(string searchWord, int tone)
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
        private string removeLastLetter(string searchWord)
        {
            return searchWord.Substring(0, searchWord.Length - 1);
        }
        private string buildString(string searchWord, string letter)
        {
            searchWord += letter;
            lblPinyin.Content = searchWord;
            return searchWord;
        }
        private List<Word> sortList(List<Word> listSearchWord)
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
        private Dictionary<int, List<Word>> makeDict(List<Word> listSearchWord)
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
        private List<Word> keyList(Dictionary<int, List<Word>> dictionarySearchWord, int index)
        {
            return dictionarySearchWord[index];
        }
        private void showButtonValue(List<Word> listPageSearchWord)
        {
            if (listPageSearchWord.Count == 1)
            {
                btnSuggest1.Content = Regex.Unescape(listPageSearchWord[0].Unicode);
            }
            else if (listPageSearchWord.Count == 2)
            {
                btnSuggest1.Content = Regex.Unescape(listPageSearchWord[0].Unicode);
                btnSuggest2.Content = Regex.Unescape(listPageSearchWord[1].Unicode);
            }
            else if (listPageSearchWord.Count == 3)
            {
                btnSuggest1.Content = Regex.Unescape(listPageSearchWord[0].Unicode);
                btnSuggest2.Content = Regex.Unescape(listPageSearchWord[1].Unicode);
                btnSuggest3.Content = Regex.Unescape(listPageSearchWord[2].Unicode);
            }
            else if (listPageSearchWord.Count == 4)
            {
                btnSuggest1.Content = Regex.Unescape(listPageSearchWord[0].Unicode);
                btnSuggest2.Content = Regex.Unescape(listPageSearchWord[1].Unicode);
                btnSuggest3.Content = Regex.Unescape(listPageSearchWord[2].Unicode);
                btnSuggest4.Content = Regex.Unescape(listPageSearchWord[3].Unicode);
            }
            else if (listPageSearchWord.Count == 5)
            {
                btnSuggest1.Content = Regex.Unescape(listPageSearchWord[0].Unicode);
                btnSuggest2.Content = Regex.Unescape(listPageSearchWord[1].Unicode);
                btnSuggest3.Content = Regex.Unescape(listPageSearchWord[2].Unicode);
                btnSuggest4.Content = Regex.Unescape(listPageSearchWord[3].Unicode);
                btnSuggest5.Content = Regex.Unescape(listPageSearchWord[4].Unicode);
            }
        }
        private void displayButton(int pageCounter, int numItems)
        {
        }
        private void removeButtons()
        {
            btnSuggest1.Visibility = Visibility.Hidden;
            btnSuggest2.Visibility = Visibility.Hidden;
            btnSuggest3.Visibility = Visibility.Hidden;
            btnSuggest4.Visibility = Visibility.Hidden;
            btnSuggest5.Visibility = Visibility.Hidden;
            btnNextSuggest.Visibility = Visibility.Hidden;
            btnBackSuggest.Visibility = Visibility.Hidden;
        }
        private void btnSuggest1_Click(object sender, RoutedEventArgs e)
        {
            txtSource.Text = Regex.Unescape(listPageSearchWord[0].Unicode); 
        }

        private void btnSuggest5_Click(object sender, RoutedEventArgs e)
        {
            txtSource.Text = Regex.Unescape(listPageSearchWord[4].Unicode); ;
        }

        private void btnSuggest4_Click(object sender, RoutedEventArgs e)
        {
            txtSource.Text = Regex.Unescape(listPageSearchWord[3].Unicode);
        }

        private void btnSuggest3_Click(object sender, RoutedEventArgs e)
        {
            txtSource.Text = Regex.Unescape(listPageSearchWord[2].Unicode);
        }

        private void btnSuggest2_Click(object sender, RoutedEventArgs e)
        {
            txtSource.Text = Regex.Unescape(listPageSearchWord[1].Unicode);
        }

        private void btnQ_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "q");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnW_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "w");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnE_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "e");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnR_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "r");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnT_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "t");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnY_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "y");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnU_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "u");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnI_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "i");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnO_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "o");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnP_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "p");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnA_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "a");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnS_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "s");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnD_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "d");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnF_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "f");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnG_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "g");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnH_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "h");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnJ_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "j");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnK_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "k");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnL_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "l");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnZ_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "z");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnX_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "x");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "c");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnV_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "v");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnB_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "b");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnN_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "n");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnM_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "m");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnUdot_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "ü");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnExclamation_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "!");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnSpace_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, " ");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnComma_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, ",");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnPeriod_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, ".");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnQuestion_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = buildString(stringSearchWord, "?");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnTone1_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord=replaceVowel(stringSearchWord,1);
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnTone2_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = replaceVowel(stringSearchWord,2);
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnTone3_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = replaceVowel(stringSearchWord,3);
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnTone4_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = replaceVowel(stringSearchWord,4);
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnBackspace_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = removeLastLetter(stringSearchWord);
            stringSearchWord = buildString(stringSearchWord, "");
            listSearchWord = stringToList(stringSearchWord);
            listSearchWord = sortList(listSearchWord);
            //display suggestions
            dictionarySearchWord = makeDict(listSearchWord);
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            removeButtons();
            showButtonValue(listPageSearchWord);
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            
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
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            showButtonValue(listPageSearchWord);
        }

        private void btnBackSuggest_Click(object sender, RoutedEventArgs e)
        {
            pageCounter--;
            if (pageCounter < 1) { 
                pageCounter = 1; 
            }
            listPageSearchWord = keyList(dictionarySearchWord, pageCounter);
            showButtonValue(listPageSearchWord);
        }
    }
}
