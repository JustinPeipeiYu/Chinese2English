using CsvHelper.Configuration.Attributes;

namespace PiaoTranslator
{
    internal class Word
    {
        [Index(4)]
        public string Unicode {  get; set; }
        [Index(1)]
        public string Pinyin {  get; set; }
        [Index(2)]
        public string Frequency { get; set; }
    }
}