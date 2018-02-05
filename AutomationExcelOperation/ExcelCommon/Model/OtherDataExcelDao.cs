using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelCommon.Model
{
    public class OtherDataExcelDao : IExcelDao
    {
        public IEnumerable<WebChatLink> WebChatLinks { get; set; }
    }
}
