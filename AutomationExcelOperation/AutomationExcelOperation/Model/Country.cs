using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationExcelOperation.Model
{
    public class Country
    {
        public string CountryKey { get; set; }
        public string CountryName { get; set; }
        public string CurrencyCode { get; set; }
        public int? AllowsImpatriateExpenses { get; set; }
        public string CountryKeySAP { get; set; }
        public int? AmexCountryISOCd { get; set; }
    }
}
