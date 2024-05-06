using CsvHelper.Configuration.Attributes;

namespace Idiom_Translator
{
    internal class Word
    {
        [Index(3)]
        public string Unicode {  get; set; }
        [Index(1)]
        public string Pinyin {  get; set; }
    }
}