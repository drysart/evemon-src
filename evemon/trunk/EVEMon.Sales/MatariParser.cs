using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace EVEMon.Sales
{
    class MatariParser:IMineralParser
    {
        private static Regex mineralLineScan = new Regex(@"\<table.*last.updated", RegexOptions.Compiled);
        private static Regex mineralTokenizer = new Regex(@"\<td.*?\>(?<name>\w*)\</td\>\<td.*?align.*?\>(?<price>(\d|\.|,)*)\</td\>\<td", RegexOptions.Compiled);

        private SortedList<string, Single> _values;

        public MatariParser(string url)
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
