using Labor.Elastic.Models;
using TinyCsvParser;

namespace Labor.CSV
{
    public static class Parsers
    {
        public static CsvParser<SomcData> SomcDataMapper
        {
            get
            {
                CsvParserOptions csvParserOptions = new CsvParserOptions(true, ';');

                return new CsvParser<SomcData>(csvParserOptions, new SomcDataMapper());
            }
        }

        public static CsvParser<NocPostitle> NocPostitleMapper
        {
            get
            {
                CsvParserOptions csvParserOptions = new CsvParserOptions(true, ';');

                return new CsvParser<NocPostitle>(csvParserOptions, new NocPostitleMapper());
            }
        }

        public static CsvParser<SelProcess> SelProcessMapper
        {
            get
            {
                CsvParserOptions csvParserOptions = new CsvParserOptions(true, ';');

                return new CsvParser<SelProcess>(csvParserOptions, new SelProcessMapper());
            }
        }
    }
}
