using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace EVEMon.Sales
{
    class PhoenixParser
    {
        private static Regex mineralLineScan = new Regex(@"(?<=Corp\sMineral\sPrices\s-\s)(?<mineral>.*\s*\:\s*(\d|\.)*)", RegexOptions.Compiled);
        private static Regex mineralTokenizer = new Regex(@"(?<name>\w*)\:(?<price>(\d|\.)*)\s", RegexOptions.Compiled);

        private SortedList<string, Single> _values;

        public PhoenixParser(string url)
        {
            ParsePage(url);
        }

        private void ParsePage(string url)
        {
            WebRequest request = WebRequest.Create(url);
            _values = new SortedList<string, Single>();
            try
            {
                WebResponse response = request.GetResponse();
                using (StreamReader pageStream = new StreamReader(response.GetResponseStream()))
                {
                    String phoenixContent = pageStream.ReadToEnd();

                    //scan for prices
                    Match m = mineralLineScan.Match(phoenixContent);

                    string mLine = m.Captures[0].Value;
                    //replacements
                    //Trit:1.75 Pye:4.19 Mex:8.84 Iso:130.00 Nocx:333.46 Zyd:4151.80 Meg:4451.80 Morp:13082.00 Ice:1.00
                    mLine = mLine.Replace("Trit", "Tritanium");
                    mLine = mLine.Replace("Pye", "Pyerite");
                    mLine = mLine.Replace("Mex", "Mexallon");
                    mLine = mLine.Replace("Iso", "Isogen");
                    mLine = mLine.Replace("Nocx", "Nocxium");
                    mLine = mLine.Replace("Zyd", "Zydrine");
                    mLine = mLine.Replace("Meg", "Megacyte");
                    mLine = mLine.Replace("Morp", "Morphite");

                    MatchCollection mc = mineralTokenizer.Matches(mLine);

                    foreach (Match mineral in mc)
                    {
                        _values[mineral.Groups["name"].Value] = Single.Parse(mineral.Groups["price"].Value);

                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public float this[string value]
        {
            get
            {
                if (_values.Keys.Contains(value))
                    return _values[value];
                else
                    return 0f;
            }
        }
    }
}
