using DAL;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelCommon.ExcelLoaders
{
    public abstract class ExcelLoader
    {
        public abstract IEnumerable<Country> LoadExcel(ExcelWorksheet workSheet);
        public abstract void LoadWorkSheet();
        public abstract void SaveExcelIntoDB();
        public abstract void SaveWorkSheetIntoDB();
    }
}
