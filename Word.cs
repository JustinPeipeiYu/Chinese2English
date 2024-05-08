using CsvHelper.Configuration.Attributes;

namespace Idiom_Translator
{
    internal class Word
    {
        [Index(4)]
        public string Unicode {  get; set; }
        [Index(1)]
        public string Pinyin {  get; set; }
        [Index(2)]
        public int Frequency { get; set; }
    }
}