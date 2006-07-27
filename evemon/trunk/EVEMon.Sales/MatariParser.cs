using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using EVEMon.Common;

namespace EVEMon.Sales
{
    [DefaultMineralParser("matari")]
    public class MatariParser: IMineralParser
    {
        private static Regex mineralLineScan = new Regex(@"\<table.*last.updated", RegexOptions.Compiled);
        private static Regex mineralTokenizer = new Regex(@"\<td.*?\>(?<name>\w*)\</td\>\<td.*?align.*?\>(?<price>(\d|\.|,)*)\</td\>\<td", RegexOptions.Compiled);

        #region IMineralParser Members

        public string Title
        {
            get { return "Matari Mineral Index"; }
        }

        public string CourtesyUrl
        {
            get { return "http://www.evegeek.com/"; }
        }

        public string CourtesyText
        {
            get { return "EVE[geek]"; }
        }

        public IEnumerable<Pair<string, decimal>> GetPrices()
        {
            string phoenixContent = null;
            try
            {
                phoenixContent = EVEMonWebRequest.GetUrlString("http://www.evegeek.com/mineralindex.php");
            }
            catch (EVEMonNetworkException ne)
            {
                ExceptionHandler.LogException(ne, true);
                throw new MineralParserException(ne.Message);
            }

            //scan for prices
            Match m = mineralLineScan.Match(phoenixContent);

            string mLine = m.Captures[0].Value;

            MatchCollection mc = mineralTokenizer.Matches(mLine);

            Decimal price = 0;
            foreach (Match mineral in mc)
            {
                string name = mineral.Groups["name"].Value;
                
                try
                {
                    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                    System.Globalization.NumberFormatInfo numInfo = culture.NumberFormat;
                    price = Decimal.Parse(mineral.Groups["price"].Value, System.Globalization.NumberStyles.Currency, numInfo);
                }
                catch (FormatException fe)
                {
                    ExceptionHandler.LogException(fe, true);
                    throw new MineralParserException(fe.Message + " (value was : " + mineral.Groups["price"].Value + ")");
                }
                yield return new Pair<string, Decimal>(name, price);
            }
        }

        #endregion
    }
}
