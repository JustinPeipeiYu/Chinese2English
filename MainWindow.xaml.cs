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

namespace Rudimentary_Chinese_English
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //search query
        string pinyin="";

        //list of the searched Words
        List<Word> longWordList;
        
        //dictionary of the searched Words
        Dictionary<int, List<Word>> wordDict;
        
        //list of 5 searched Words
        List<Word> shortWordList = new List<Word>();
        
        //vowels
        public static string vowels = "aeiouü";
        
        //list of all Words in the record
        List<Word> wordRecords;
        
        //list of all Vowels in record
        List<Vowels> vowelRecords;
        
        //the key of the dictionary to display
        int pageCounter = 1;
        
        //dictionary of all search Words and list of 5 search Words
        Tuple<Dictionary<int, List<Word>>, List<Word>> wordDict_shortList;

        //source phrase
        string sourcePhrase = string.Empty;


        public MainWindow()
        {
            InitializeComponent();
            wordRecords = Methods.readWordCSV();
            vowelRecords = Methods.readVowelCSV();
            removeButtons();
        }
        private void showButtonValue(List<Word> listPageSearchWord, string pinyin)
        {
            if (pinyin.Length > 1)
            {
                if (listPageSearchWord.Count > 0)
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
        
        private void displayButtons(int pageCounter, int totalPagesCounter, int numItems, string pinyin)
        {
            if (pinyin.Length > 1)
            {
                if (numItems > 0)
                {
                    if (pageCounter == totalPagesCounter)
                    {
                        btnNextSuggest.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        btnNextSuggest.Visibility = Visibility.Visible;
                    }
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
            }
        }
        private void btnSuggest1_Click(object sender, RoutedEventArgs e)
        {
            sourcePhrase = Methods.updateSourcePhrase(sourcePhrase, shortWordList[0].Unicode);
            txtSource.Text = Regex.Unescape(sourcePhrase); 
        }

        private void btnSuggest5_Click(object sender, RoutedEventArgs e)
        {
            sourcePhrase = Methods.updateSourcePhrase(sourcePhrase, shortWordList[4].Unicode);
            txtSource.Text = Regex.Unescape(sourcePhrase);
        }

        private void btnSuggest4_Click(object sender, RoutedEventArgs e)
        {
            sourcePhrase = Methods.updateSourcePhrase(sourcePhrase, shortWordList[3].Unicode);
            txtSource.Text = Regex.Unescape(sourcePhrase);
        }

        private void btnSuggest3_Click(object sender, RoutedEventArgs e)
        {
            sourcePhrase = Methods.updateSourcePhrase(sourcePhrase, shortWordList[2].Unicode);
            txtSource.Text = Regex.Unescape(sourcePhrase);
        }

        private void btnSuggest2_Click(object sender, RoutedEventArgs e)
        {
            sourcePhrase = Methods.updateSourcePhrase(sourcePhrase, shortWordList[1].Unicode);
            txtSource.Text = Regex.Unescape(sourcePhrase);
        }

        private void btnQ_Click(object sender, RoutedEventArgs e)
        {
            //remove buttons temporarily
            removeButtons();
            //add letter to the search string
            pinyin = Methods.buildString(pinyin, "q");
            //display search string on the label
            lblPinyin.Content = pinyin;
            //search for the string in records, by default return the first page
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            //extrac the dictionary
            wordDict = wordDict_shortList.Item1;
            //extract the page from dictionary
            shortWordList = wordDict_shortList.Item2;
            //display buttons
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count,pinyin);
            //display suggestions in buttons
            showButtonValue(shortWordList,pinyin);
        }

        private void btnW_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "w");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnE_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "e");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnR_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "r");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count,pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnT_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "t");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count,pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnY_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "y");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count,pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnU_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "u");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count,pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnI_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "i");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count,pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnO_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "o");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnP_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "p");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnA_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "a");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnS_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "s");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnD_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "d");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnF_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "f");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnG_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "g");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnH_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "h");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnJ_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "j");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnK_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "k");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnL_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "l");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnZ_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "z");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnX_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "x");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "c");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnV_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "v");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnB_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "b");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnN_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "n");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnM_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "m");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnUdot_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.buildString(pinyin, "ü");
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnExclamation_Click(object sender, RoutedEventArgs e)
        {
            sourcePhrase = Methods.addExclamationMark(sourcePhrase);
            txtSource.Text = Regex.Unescape(sourcePhrase);
        }

        private void btnSpace_Click(object sender, RoutedEventArgs e)
        {
            sourcePhrase = Methods.addSpace(sourcePhrase);
            txtSource.Text = Regex.Unescape(sourcePhrase);
        }

        private void btnComma_Click(object sender, RoutedEventArgs e)
        {
            sourcePhrase = Methods.addComma(sourcePhrase);
            txtSource.Text = Regex.Unescape(sourcePhrase);
        }

        private void btnPeriod_Click(object sender, RoutedEventArgs e)
        {
            sourcePhrase = Methods.addPeriod(sourcePhrase);
            txtSource.Text = Regex.Unescape(sourcePhrase);
        }

        private void btnQuestion_Click(object sender, RoutedEventArgs e)
        {
            sourcePhrase = Methods.addQuestionMark(sourcePhrase);
            txtSource.Text = Regex.Unescape(sourcePhrase);
        }

        private void btnTone1_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin =Methods.replaceVowel(pinyin,1, vowelRecords);
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnTone2_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.replaceVowel(pinyin, 2, vowelRecords);
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnTone3_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.replaceVowel(pinyin, 3, vowelRecords);
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnTone4_Click(object sender, RoutedEventArgs e)
        {
            removeButtons();
            pinyin = Methods.replaceVowel(pinyin, 4, vowelRecords);
            lblPinyin.Content = pinyin;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (!pinyin.Equals(""))
            {
                pinyin = Methods.removeLastLetter(pinyin);
            }
            removeButtons();
            lblPinyin.Content = pinyin;
            pageCounter = 1;
            wordDict_shortList = Methods.searchWord(pinyin, pageCounter, wordRecords);
            wordDict = wordDict_shortList.Item1;
            shortWordList = wordDict_shortList.Item2;
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            sourcePhrase = Methods.addNewLine(sourcePhrase);
            txtSource.Text = Regex.Unescape(sourcePhrase);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            sourcePhrase = string.Empty;
            txtSource.Text = Regex.Unescape(sourcePhrase);
        }

        private void BtnConvert_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnNextSuggest_Click(object sender, RoutedEventArgs e)
        {
            pageCounter++;
            if (pageCounter > wordDict.Count) { 
                pageCounter = wordDict.Count; 
            }
            shortWordList = Methods.keyList(wordDict, pageCounter);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }

        private void btnBackSuggest_Click(object sender, RoutedEventArgs e)
        {
            pageCounter--;
            if (pageCounter < 1) { 
                pageCounter = 1; 
            }
            shortWordList = Methods.keyList(wordDict, pageCounter);
            displayButtons(pageCounter, wordDict.Count, shortWordList.Count, pinyin);
            showButtonValue(shortWordList, pinyin);
        }
    }
}
