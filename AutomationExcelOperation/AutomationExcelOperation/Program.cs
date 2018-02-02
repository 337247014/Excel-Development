using ExcelCommon;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
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
            ExcelProcessor excelLoader = new ExcelProcessor();

            excelLoader.LoadExcel();
            excelLoader.SaveDataIntoDB();
            
            Console.ReadLine();
        }

    }
}
