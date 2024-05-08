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

namespace Idiom_Translator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string searchWord="";
        List<string> returnWords;
        string vowels = "aeiouü";
        List<Idiom_Translator.Word> pinyinHanyuRecords;
        List<Idiom_Translator.Vowels> tonedVowelsRecords;

        public MainWindow()
        {
            InitializeComponent();
            pinyinHanyuRecords = readPinyinHanyuRecords();
            tonedVowelsRecords = readtonedVowelsRecords();
        }

        private List<string> searchPinyinReturnHanyu(List<Idiom_Translator.Word> pinyinHanyuRecords)
        {
            //list of all hanyu characters with matching pinyin 
            List<string> e = new List<string>();
            //search all matching pinyin in List
            foreach (var record in pinyinHanyuRecords)
            {
                //proceed if there is a matching pinyin in Record
                if (record.Pinyin.Equals(searchWord))
                {
                    //add pinyin's corresponding hanyu to List
                    e.Add(Regex.Unescape(record.Unicode));
                } else
                {

                }
            }
            string o = "";
            foreach (string s in e)
            {
                o += s;
            }
            txtSource.Text= o;
            //may return empty list
            return e;
        }

        //read 8105 entries from pinyin hanyu database
        private List<Idiom_Translator.Word> readPinyinHanyuRecords()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
            using (var reader = new StreamReader("list of pinyin hanyu.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                var pinyinHanyuRecords = csv.GetRecords<Word>();
                return pinyinHanyuRecords.ToList();
            }
        }

        //read 6 entries from tone on vowel database
        private List<Idiom_Translator.Vowels> readtonedVowelsRecords()
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
            searchWord += "q";
            lblPinyin.Content=searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnW_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "w";
            lblPinyin.Content = searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnE_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "e";
            lblPinyin.Content = searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnR_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "r";
            lblPinyin.Content = searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnT_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "t";
            lblPinyin.Content = searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnY_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "y";
            lblPinyin.Content = searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnU_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "u";
            lblPinyin.Content = searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnI_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "i";
            lblPinyin.Content = searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnO_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "o";
            lblPinyin.Content = searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnP_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "p";
            lblPinyin.Content = searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnA_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "a";
            lblPinyin.Content = searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnS_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "s";
            lblPinyin.Content = searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnD_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "d";
            lblPinyin.Content = searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnF_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "f";
            lblPinyin.Content = searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnG_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "g";
            lblPinyin.Content = searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnH_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "h";
            lblPinyin.Content = searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnJ_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "j";
            lblPinyin.Content = searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnK_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "k";
            lblPinyin.Content = searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnL_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "l";
            lblPinyin.Content = searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnZ_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "z";
            lblPinyin.Content = searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnX_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "x";
            lblPinyin.Content = searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "c";
            lblPinyin.Content = searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnV_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "v";
            lblPinyin.Content = searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnB_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "b";
            lblPinyin.Content = searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnN_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "n";
            lblPinyin.Content = searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnM_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "m";
            lblPinyin.Content = searchWord;
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnUdot_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "ü";
            lblPinyin.Content = searchWord; 
            returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
        }

        private void btnExclamation_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "!";
            lblPinyin.Content = searchWord;
        }

        private void btnSpace_Click(object sender, RoutedEventArgs e)
        {
            searchWord += " ";
            lblPinyin.Content = searchWord;
        }

        private void btnComma_Click(object sender, RoutedEventArgs e)
        {
            searchWord += ",";
            lblPinyin.Content = searchWord;
        }

        private void btnPeriod_Click(object sender, RoutedEventArgs e)
        {
            searchWord += ".";
            lblPinyin.Content = searchWord;
        }

        private void btnQuestion_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "?";
            lblPinyin.Content = searchWord;
        }

        private void btnTone1_Click(object sender, RoutedEventArgs e)
        {
            //get last letter
            string lastLetter = searchWord.Substring(searchWord.Length - 1, 1);
            //proceed if the letter is vowel
            if (vowels.Contains(lastLetter))
            {
                foreach (var record in tonedVowelsRecords)
                {
                    //specify which vowel to tone
                    if (record.vowel.Equals(lastLetter))
                    {
                        //remove the last letter
                        searchWord = searchWord.Substring(0, searchWord.Length - 1);
                        //add the toned vowel
                        searchWord += record.vowelTone1;
                    }
                }
                //display to label
                lblPinyin.Content = searchWord;
                returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
            }
        }

        private void btnTone2_Click(object sender, RoutedEventArgs e)
        {
            //get last letter
            string lastLetter = searchWord.Substring(searchWord.Length - 1, 1);
            //proceed if the letter is vowel
            if (vowels.Contains(lastLetter))
            {
                foreach (var record in tonedVowelsRecords)
                {
                    //specify which vowel to tone
                    if (record.vowel.Equals(lastLetter))
                    {
                        //remove the last letter
                        searchWord = searchWord.Substring(0, searchWord.Length - 1);
                        //add the toned vowel
                        searchWord += record.vowelTone1;
                    }
                }
                //display to label
                lblPinyin.Content = searchWord;
                returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
            }
        }

        private void btnTone3_Click(object sender, RoutedEventArgs e)
        {
            //get last letter
            string lastLetter = searchWord.Substring(searchWord.Length - 1, 1);
            //proceed if the letter is vowel
            if (vowels.Contains(lastLetter))
            {
                foreach (var record in tonedVowelsRecords)
                {
                    //specify which vowel to tone
                    if (record.vowel.Equals(lastLetter))
                    {
                        //remove the last letter
                        searchWord = searchWord.Substring(0, searchWord.Length - 1);
                        //add the toned vowel
                        searchWord += record.vowelTone1;
                    }
                }
                //display to label
                lblPinyin.Content = searchWord;
                returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
            }
        }

        private void btnTone4_Click(object sender, RoutedEventArgs e)
        {
            //get last letter
            string lastLetter = searchWord.Substring(searchWord.Length - 1, 1);
            //proceed if the letter is vowel
            if (vowels.Contains(lastLetter))
            {
                foreach (var record in tonedVowelsRecords)
                {
                    //specify which vowel to tone
                    if (record.vowel.Equals(lastLetter))
                    {
                        //remove the last letter
                        searchWord = searchWord.Substring(0, searchWord.Length - 1);
                        //add the toned vowel
                        searchWord += record.vowelTone1;
                    }
                }
                //display to label
                lblPinyin.Content = searchWord;
                returnWords = searchPinyinReturnHanyu(pinyinHanyuRecords);
            }
        }

        private void btnBackspace_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
