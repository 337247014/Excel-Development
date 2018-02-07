using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationExcelOperation.Helpers
{
    public static class ConstantHelper
    {
        public static readonly string[] HelpArguments = { @"-help", @"-h", @"?" };
        public static readonly string[] DeleteArguments = { @"-delete", @"-d" };
        public static readonly string[] LoadTestDataArgs = { @"-test", @"-testdata" };
        public static readonly string[] LoadOtherDataArgs = { @"-other", @"-otherdata" };

        public static readonly string[] BuildOrderExcelArgs = { @"-order", @"-orderexcel" };

        public static readonly string TestDataExcelFileLocation = "..\\..\\Data\\RawExcel\\TestData.xlsx";
        public static readonly string OtherDataExcelFileLocation = "..\\..\\Data\\RawExcel\\OtherData.xlsx";
        public static readonly string OrderExcelFileLocation = "..\\..\\Data\\GeneratedExcel\\Order.xlsx";
    }
}
