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
        static string stringVowels = "aeiouü";
        static List<Word> listWordRecords;
        static List<Vowels> listVowelRecords;

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
        private void btnSuggest1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSuggest5_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSuggest4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSuggest3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSuggest2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnQ_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "q");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnW_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "w");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnE_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "e");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnR_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "r");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnT_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "t");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnY_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "y");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnU_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "u");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnI_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "i");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnO_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "o");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnP_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "p");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnA_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "a");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnS_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "s");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnD_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "d");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnF_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "f");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnG_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "g");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnH_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "h");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnJ_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "j");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnK_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "k");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnL_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "l");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnZ_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "z");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnX_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "x");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "c");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnV_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "v");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnB_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "b");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnN_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "n");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnM_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "m");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnUdot_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "ü");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnExclamation_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "!");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnSpace_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, " ");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnComma_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, ",");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnPeriod_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, ".");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnQuestion_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "?");
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnTone1_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord=methodUpdateToneVowel(stringSearchWord,1);
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnTone2_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateToneVowel(stringSearchWord,2);
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnTone3_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateToneVowel(stringSearchWord,3);
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnTone4_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodUpdateToneVowel(stringSearchWord,4);
            listSearchWord = methodFindWord(stringSearchWord);
        }

        private void btnBackspace_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord = methodRemoveLastLetter(stringSearchWord);
            stringSearchWord = methodUpdateSearchWord(stringSearchWord, "");
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

        private void btnSuggest6_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSuggest7_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSuggest8_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSuggest9_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSuggest10_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSuggest11_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSuggest12_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
