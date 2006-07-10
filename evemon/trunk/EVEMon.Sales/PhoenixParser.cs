using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using EVEMon.Common;

namespace EVEMon.Sales
{
    [DefaultMineralParser("phoenix")]
    public class PhoenixParser : IMineralParser
    {
        private static Regex mineralLineScan = new Regex(@"(?<=Corp\sMineral\sPrices\s-\s)(?<mineral>.*\s*\:\s*(\d|\.)*)", RegexOptions.Compiled);
        private static Regex mineralTokenizer = new Regex(@"(?<name>\w*)\:(?<price>(\d|\.)*)\s", RegexOptions.Compiled);

        #region IMineralParser Members

        public string Title
        {
            get { return "Phoenix Industries"; }
        }

        public string CourtesyUrl
        {
            get { return "http://www.phoenix-industries.org/"; }
        }

        public string CourtesyText
        {
            get { return "Phoenix Industries"; }
        }

        public IEnumerable<Pair<string, decimal>> GetPrices()
        {
            string data = null;
            try
            {
                data = EVEMonWebRequest.GetUrlString("http://www.phoenix-industries.org/");
            }
            catch (EVEMonNetworkException ne)
            {
                ExceptionHandler.LogException(ne, true);
                throw new MineralParserException(ne.Message);
            }

            //scan for prices
            Match m = mineralLineScan.Match(data);

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
            Decimal price = 0;
            foreach (Match mineral in mc)
            {
                string name = mineral.Groups["name"].Value;
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                System.Globalization.NumberFormatInfo numInfo = culture.NumberFormat;
                price = Decimal.Parse(mineral.Groups["price"].Value, System.Globalization.NumberStyles.Currency, numInfo);
                yield return new Pair<string, Decimal>(name, price);
            }
        }

        #endregion
    }
}
