using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelCommon.Model
{
    public class TestDataExcelDao
    {
        public IEnumerable<Country> Country { get; set; }
        public IEnumerable<Company> Company { get; set; }
        public IEnumerable<ValidationRule> ValidationRule { get; set; }
    }
}
