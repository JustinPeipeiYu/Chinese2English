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
        //all variables are accessible by 
        //input as a string of pinyin 
        string pinyin="";
        //output as a list of hanzi 
        List<Word> longWordList;
        //output organized as dictionary
        Dictionary<int, List<Word>> wordDict;
        //single dictionary key's list value
        List<Word> shortWordList;
        //reference vowels
        public static string vowels = "aeiouü";
        //store the contents of "pinying hanzi.csv"
        List<Word> wordRecords;
        //store contents of "tones on vowels.csv"
        List<Vowels> vowelRecords;
        //store current key in dictionary
        int pageCounter = 1;

        public MainWindow()
        {
            InitializeComponent();
            wordRecords = Methods.readWordCSV();
            vowelRecords = Methods.readVowelCSV();
            removeButtons();
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
        
        private void displayButtons(int pageCounter, int totalPagesCounter, int numItems)
        {
            //next button visibility
            if (pageCounter == totalPagesCounter)
            {
                btnNextSuggest.Visibility = Visibility.Hidden;
            }
            else
            {
                btnNextSuggest.Visibility = Visibility.Visible;
            }
            //back button visibility
            if (pageCounter == 1)
            {
                btnBackSuggest.Visibility = Visibility.Hidden;
            }
            else
            {
                btnBackSuggest.Visibility = Visibility.Visible;
            }
            if (numItems == 1)
            {
                btnSuggest1.Visibility = Visibility.Visible;
                btnSuggest2.Visibility = Visibility.Hidden;
                btnSuggest3.Visibility = Visibility.Hidden;
                btnSuggest4.Visibility = Visibility.Hidden;
                btnSuggest5.Visibility = Visibility.Hidden;
            }
            else if (numItems == 2)
            {
                btnSuggest1.Visibility = Visibility.Visible;
                btnSuggest2.Visibility = Visibility.Visible;
                btnSuggest3.Visibility = Visibility.Hidden;
                btnSuggest4.Visibility = Visibility.Hidden;
                btnSuggest5.Visibility = Visibility.Hidden;
            }
            else if (numItems == 3)
            {
                btnSuggest1.Visibility = Visibility.Visible;
                btnSuggest2.Visibility = Visibility.Visible;
                btnSuggest3.Visibility = Visibility.Visible;
                btnSuggest4.Visibility = Visibility.Hidden;
                btnSuggest5.Visibility = Visibility.Hidden;
            }
            else if (numItems == 4)
            {
                btnSuggest1.Visibility = Visibility.Visible;
                btnSuggest2.Visibility = Visibility.Visible;
                btnSuggest3.Visibility = Visibility.Visible;
                btnSuggest4.Visibility = Visibility.Visible;
                btnSuggest5.Visibility = Visibility.Hidden;
            }
            else
            {
                btnSuggest1.Visibility = Visibility.Visible;
                btnSuggest2.Visibility = Visibility.Visible;
                btnSuggest3.Visibility = Visibility.Visible;
                btnSuggest4.Visibility = Visibility.Visible;
                btnSuggest5.Visibility = Visibility.Visible;
            }
        }
        private void btnSuggest1_Click(object sender, RoutedEventArgs e)
        {
            txtSource.Text = Regex.Unescape(shortWordList[0].Unicode); 
        }

        private void btnSuggest5_Click(object sender, RoutedEventArgs e)
        {
            txtSource.Text = Regex.Unescape(shortWordList[4].Unicode); ;
        }

        private void btnSuggest4_Click(object sender, RoutedEventArgs e)
        {
            txtSource.Text = Regex.Unescape(shortWordList[3].Unicode);
        }

        private void btnSuggest3_Click(object sender, RoutedEventArgs e)
        {
            txtSource.Text = Regex.Unescape(shortWordList[2].Unicode);
        }

        private void btnSuggest2_Click(object sender, RoutedEventArgs e)
        {
            txtSource.Text = Regex.Unescape(shortWordList[1].Unicode);
        }

        private void btnQ_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "q");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnW_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "w");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnE_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "e");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnR_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "r");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnT_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "t");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnY_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "y");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnU_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "u");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnI_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "i");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnO_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "o");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnP_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "p");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnA_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "a");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnS_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "s");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnD_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "d");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnF_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "f");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnG_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "g");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnH_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "h");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnJ_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "j");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnK_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "k");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnL_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "l");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnZ_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "z");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnX_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "x");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "c");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnV_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "v");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnB_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "b");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnN_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "n");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnM_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "m");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnUdot_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.buildString(pinyin, "ü");
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnExclamation_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnSpace_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnComma_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnPeriod_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnQuestion_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnTone1_Click(object sender, RoutedEventArgs e)
        {
            pinyin=Methods.replaceVowel(pinyin,1, vowelRecords);
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnTone2_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.replaceVowel(pinyin, 1, vowelRecords);
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnTone3_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.replaceVowel(pinyin, 1, vowelRecords);
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnTone4_Click(object sender, RoutedEventArgs e)
        {
            pinyin = Methods.replaceVowel(pinyin, 1, vowelRecords);
            lblPinyin.Content = pinyin;
            Methods.searchWord(pinyin, pageCounter, wordRecords);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnBackspace_Click(object sender, RoutedEventArgs e)
        {
            //if search word is nothing, then remove suggestion buttons
            if (pinyin.Equals(""))
            {
                removeButtons();
            } else
            {
                pinyin = Methods.removeLastLetter(pinyin);
                pinyin = Methods.buildString(pinyin, "");
                lblPinyin.Content = pinyin;
                Methods.searchWord(pinyin, pageCounter, wordRecords);
            }
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
            if (pageCounter > longWordList.Count / 5 + 1) { 
                pageCounter = longWordList.Count / 5 + 1; 
            }
            shortWordList = Methods.keyList(wordDict, pageCounter);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }

        private void btnBackSuggest_Click(object sender, RoutedEventArgs e)
        {
            pageCounter--;
            if (pageCounter < 1) { 
                pageCounter = 1; 
            }
            shortWordList = Methods.keyList(wordDict, pageCounter);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count);
            showButtonValue(shortWordList);
        }
    }
}
