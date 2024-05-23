using CsvHelper.Configuration.Attributes;

namespace Rudimentary_Chinese_English
{
    internal class Vowels
    {
        [Index(0)]
        public string vowel { get; set; }
        [Index(1)]
        public string vowelTone1 {  get; set; }
        [Index(2)]
        public string vowelTone2 { get; set; }
        [Index(3)]
        public string vowelTone3 { get; set; }
        [Index(4)]
        public string vowelTone4 { get; set; }

    }
}