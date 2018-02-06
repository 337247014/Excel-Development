using DAL;
using ExcelCommon.ExcelLoaders;
using ExcelCommon.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelCommon
{
    public class ExcelProcessor
    {
        public ExcelProcessor() { }

        public IExcelDao LoadExcel(GeneralFactory factory,FileInfo file)
        {
            IExcelDao data;

            if (!file.Exists) throw new Exception("the excel file doesn't exist.");
            ExcelPackage package = new ExcelPackage(file);
            data = factory.CreateInstance().LoadWorkbook(package.Workbook);

            return data;
        }

        public void SaveDataIntoDB(GeneralFactory factory, IExcelDao data)
        {
            factory.CreateInstance().SaveWorkbookIntoDB(data);
        }

    }
}
