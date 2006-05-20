using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Windows.Forms;

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
                throw new MineralParserException(ne.Message);
            }

            //scan for prices
            Match m = mineralLineScan.Match(phoenixContent);

            string mLine = m.Captures[0].Value;

            MatchCollection mc = mineralTokenizer.Matches(mLine);

            foreach (Match mineral in mc)
            {
                string name = mineral.Groups["name"].Value;
                Decimal price = Decimal.Parse(mineral.Groups["price"].Value);
                yield return new Pair<string, Decimal>(name, price);
            }
        }

        #endregion
    }
}
