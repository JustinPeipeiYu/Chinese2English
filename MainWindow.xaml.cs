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
        public MainWindow()
        {
            InitializeComponent();
            string c;
            List<string> e= new List<string>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
            using (var reader = new StreamReader("list of pinyin hanyu.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<Word>();
                
                foreach (var record in records)
                {
                    //search all matching pinyin in Ienumerable collection
                    if (record.Pinyin.Equals("yě"))
                    {
                        //add corresponding hanyu to a list
                        c = Regex.Unescape(record.Unicode);
                        e.Add(c);
                    }
                }
                lblPinyin.Content = e.Count();
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

        }

        private void btnW_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnE_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnR_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnT_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnY_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnU_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnI_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnO_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnP_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnA_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnS_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnD_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnF_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnG_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnH_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnJ_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnK_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnL_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnZ_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnX_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnC_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnV_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnB_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnM_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUdot_Click(object sender, RoutedEventArgs e)
        {

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

        }

        private void btnTone2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTone3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTone4_Click(object sender, RoutedEventArgs e)
        {

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
