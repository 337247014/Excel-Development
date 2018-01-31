using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationExcelOperation
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo outputDir = new DirectoryInfo(@"C:\");
            if (!outputDir.Exists) throw new Exception("Output Directory doesn't exists.");
            string output = "";

            FileInfo testDataExcelFile = new FileInfo("..\\..\\Data\\RawExcel\\TestData.xlsx");
            if (!testDataExcelFile.Exists) throw new Exception("TestData excel file doesn't exist.");
        }
    }
}
