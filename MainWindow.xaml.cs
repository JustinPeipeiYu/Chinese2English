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
        List<Word> listWord;
        string stringVowels = "aeiouü";
        List<Word> listWordRecords;
        List<Vowels> listVowelRecords;

        public MainWindow()
        {
            InitializeComponent();
            listWordRecords = methodReadWords();
            listVowelRecords = methodReadVowels();
        }

        private List<Word> methodFindWord(List<Word> pinyinHanyuRecords)
        {
            //list of all hanyu characters with matching pinyin 
            List<Word> e = new List<Word>();
            //search all matching pinyin in List
            foreach (var record in pinyinHanyuRecords)
            {
                //proceed if there is a matching pinyin in Record
                if (record.Pinyin.Equals(stringSearchWord))
                {
                    //add pinyin's corresponding hanyu to List
                    e.Add(record);
                } 
            }
            //may return empty list
            return e;
        }

        //read 8105 entries from pinyin hanyu database
        private List<Word> methodReadWords()
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
        private List<Vowels> methodReadVowels()
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
            stringSearchWord += "q";
            lblPinyin.Content=stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnW_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "w";
            lblPinyin.Content = stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnE_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "e";
            lblPinyin.Content = stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnR_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "r";
            lblPinyin.Content = stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnT_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "t";
            lblPinyin.Content = stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnY_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "y";
            lblPinyin.Content = stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnU_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "u";
            lblPinyin.Content = stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnI_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "i";
            lblPinyin.Content = stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnO_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "o";
            lblPinyin.Content = stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnP_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "p";
            lblPinyin.Content = stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnA_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "a";
            lblPinyin.Content = stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnS_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "s";
            lblPinyin.Content = stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnD_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "d";
            lblPinyin.Content = stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnF_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "f";
            lblPinyin.Content = stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnG_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "g";
            lblPinyin.Content = stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnH_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "h";
            lblPinyin.Content = stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnJ_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "j";
            lblPinyin.Content = stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnK_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "k";
            lblPinyin.Content = stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnL_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "l";
            lblPinyin.Content = stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnZ_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "z";
            lblPinyin.Content = stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnX_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "x";
            lblPinyin.Content = stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "c";
            lblPinyin.Content = stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnV_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "v";
            lblPinyin.Content = stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnB_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "b";
            lblPinyin.Content = stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnN_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "n";
            lblPinyin.Content = stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnM_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "m";
            lblPinyin.Content = stringSearchWord;
            listWord = methodFindWord(listWordRecords);
        }

        private void btnUdot_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "ü";
            lblPinyin.Content = stringSearchWord; 
            listWord = methodFindWord(listWordRecords);
        }

        private void btnExclamation_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "!";
            lblPinyin.Content = stringSearchWord;
        }

        private void btnSpace_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += " ";
            lblPinyin.Content = stringSearchWord;
        }

        private void btnComma_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += ",";
            lblPinyin.Content = stringSearchWord;
        }

        private void btnPeriod_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += ".";
            lblPinyin.Content = stringSearchWord;
        }

        private void btnQuestion_Click(object sender, RoutedEventArgs e)
        {
            stringSearchWord += "?";
            lblPinyin.Content = stringSearchWord;
        }

        private void btnTone1_Click(object sender, RoutedEventArgs e)
        {
            //get last letter
            string lastLetter = stringSearchWord.Substring(stringSearchWord.Length - 1, 1);
            //proceed if the letter is vowel
            if (stringVowels.Contains(lastLetter))
            {
                foreach (var record in listVowelRecords)
                {
                    //specify which vowel to tone
                    if (record.vowel.Equals(lastLetter))
                    {
                        //remove the last letter
                        stringSearchWord = stringSearchWord.Substring(0, stringSearchWord.Length - 1);
                        //add the toned vowel
                        stringSearchWord += record.vowelTone1;
                    }
                }
                //display to label
                lblPinyin.Content = stringSearchWord;
                listWord = methodFindWord(listWordRecords);
            }
        }

        private void btnTone2_Click(object sender, RoutedEventArgs e)
        {
            //get last letter
            string lastLetter = stringSearchWord.Substring(stringSearchWord.Length - 1, 1);
            //proceed if the letter is vowel
            if (stringVowels.Contains(lastLetter))
            {
                foreach (var record in listVowelRecords)
                {
                    //specify which vowel to tone
                    if (record.vowel.Equals(lastLetter))
                    {
                        //remove the last letter
                        stringSearchWord = stringSearchWord.Substring(0, stringSearchWord.Length - 1);
                        //add the toned vowel
                        stringSearchWord += record.vowelTone1;
                    }
                }
                //display to label
                lblPinyin.Content = stringSearchWord;
                listWord = methodFindWord(listWordRecords);
            }
        }

        private void btnTone3_Click(object sender, RoutedEventArgs e)
        {
            //get last letter
            string lastLetter = stringSearchWord.Substring(stringSearchWord.Length - 1, 1);
            //proceed if the letter is vowel
            if (stringVowels.Contains(lastLetter))
            {
                foreach (var record in listVowelRecords)
                {
                    //specify which vowel to tone
                    if (record.vowel.Equals(lastLetter))
                    {
                        //remove the last letter
                        stringSearchWord = stringSearchWord.Substring(0, stringSearchWord.Length - 1);
                        //add the toned vowel
                        stringSearchWord += record.vowelTone1;
                    }
                }
                //display to label
                lblPinyin.Content = stringSearchWord;
                listWord = methodFindWord(listWordRecords);
            }
        }

        private void btnTone4_Click(object sender, RoutedEventArgs e)
        {
            //get last letter
            string lastLetter = stringSearchWord.Substring(stringSearchWord.Length - 1, 1);
            //proceed if the letter is vowel
            if (stringVowels.Contains(lastLetter))
            {
                foreach (var record in listVowelRecords)
                {
                    //specify which vowel to tone
                    if (record.vowel.Equals(lastLetter))
                    {
                        //remove the last letter
                        stringSearchWord = stringSearchWord.Substring(0, stringSearchWord.Length - 1);
                        //add the toned vowel
                        stringSearchWord += record.vowelTone1;
                    }
                }
                //display to label
                lblPinyin.Content = stringSearchWord;
                listWord = methodFindWord(listWordRecords);
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
