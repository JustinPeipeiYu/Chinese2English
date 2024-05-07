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
        string vowels = "aeiouü";
        List<Idiom_Translator.Word> pinyinHanyuRecords;
        List<Idiom_Translator.Vowels> tonedVowelsRecords;

        public MainWindow()
        {
            InitializeComponent();
            //capture the unicode (hanyu) character
            string c;
            //list of all hanyu characters with matching pinyin 
            List<string> e= new List<string>();
            pinyinHanyuRecords = readPinyinHanyuRecords();
            tonedVowelsRecords = readtonedVowelsRecords();
        }

        //read 8105 entries from pinyin hanyu datatable
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
                /*
                //search all matching pinyin in Ienumerable collection
                foreach (var record in pinyinHanyuRecords)
                {
                    if (record.Pinyin.Equals("yě"))
                    {
                        //add corresponding hanyu to a list
                        c = Regex.Unescape(record.Unicode);
                        e.Add(c);
                    }
                }*/
                return pinyinHanyuRecords.ToList();
            }
        }

        //read 6 entries from tone on vowel datatable
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
        }

        private void btnW_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "w";
            lblPinyin.Content = searchWord;
        }

        private void btnE_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "e";
            lblPinyin.Content = searchWord;
        }

        private void btnR_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "r";
            lblPinyin.Content = searchWord;
        }

        private void btnT_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "t";
            lblPinyin.Content = searchWord;
        }

        private void btnY_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "y";
            lblPinyin.Content = searchWord;
        }

        private void btnU_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "u";
            lblPinyin.Content = searchWord;
        }

        private void btnI_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "i";
            lblPinyin.Content = searchWord;
        }

        private void btnO_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "o";
            lblPinyin.Content = searchWord;
        }

        private void btnP_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "p";
            lblPinyin.Content = searchWord;
        }

        private void btnA_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "a";
            lblPinyin.Content = searchWord;
        }

        private void btnS_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "s";
            lblPinyin.Content = searchWord;
        }

        private void btnD_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "d";
            lblPinyin.Content = searchWord;
        }

        private void btnF_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "f";
            lblPinyin.Content = searchWord;
        }

        private void btnG_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "g";
            lblPinyin.Content = searchWord;
        }

        private void btnH_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "h";
            lblPinyin.Content = searchWord;
        }

        private void btnJ_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "j";
            lblPinyin.Content = searchWord;
        }

        private void btnK_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "k";
            lblPinyin.Content = searchWord;
        }

        private void btnL_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "l";
            lblPinyin.Content = searchWord;
        }

        private void btnZ_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "z";
            lblPinyin.Content = searchWord;
        }

        private void btnX_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "x";
            lblPinyin.Content = searchWord;
        }

        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "c";
            lblPinyin.Content = searchWord;
        }

        private void btnV_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "v";
            lblPinyin.Content = searchWord;
        }

        private void btnB_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "b";
            lblPinyin.Content = searchWord;
        }

        private void btnN_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "n";
            lblPinyin.Content = searchWord;
        }

        private void btnM_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "m";
            lblPinyin.Content = searchWord;
        }

        private void btnUdot_Click(object sender, RoutedEventArgs e)
        {
            searchWord += "ü";
            lblPinyin.Content = searchWord; 
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
            string lastLetter = searchWord.Substring(searchWord.Length - 1, 1);
            if (vowels.Contains(lastLetter))
            {
                foreach (var record in tonedVowelsRecords)
                {
                    if (record.vowel.Equals(lastLetter))
                    {
                        searchWord += record.vowelTone1;
                    }
                }
                lblPinyin.Content = searchWord;
            }
        }

        private void btnTone2_Click(object sender, RoutedEventArgs e)
        {
            string lastLetter = searchWord.Substring(searchWord.Length - 1, 1);
            if (vowels.Contains(lastLetter))
            {
                foreach (var record in tonedVowelsRecords)
                {
                    if (record.vowel.Equals(lastLetter))
                    {
                        searchWord += record.vowelTone2;
                    }
                }
                lblPinyin.Content = searchWord;
            } 
        }

        private void btnTone3_Click(object sender, RoutedEventArgs e)
        {
            string lastLetter = searchWord.Substring(searchWord.Length - 1, 1);
            if (vowels.Contains(lastLetter))
            {
                foreach (var record in tonedVowelsRecords)
                {
                    if (record.vowel.Equals(lastLetter))
                    {
                        searchWord += record.vowelTone3;
                    }
                }
                lblPinyin.Content = searchWord;
            }
        }

        private void btnTone4_Click(object sender, RoutedEventArgs e)
        {
            string lastLetter = searchWord.Substring(searchWord.Length - 1, 1);
            if (vowels.Contains(lastLetter))
            {
                foreach (var record in tonedVowelsRecords)
                {
                    if (record.vowel.Equals(lastLetter))
                    {
                        searchWord += record.vowelTone4;
                    }
                }
                lblPinyin.Content = searchWord;
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
