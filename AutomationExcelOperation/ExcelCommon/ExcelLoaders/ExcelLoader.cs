using DAL;
using ExcelCommon.Model;
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
        protected UnitOfWork unitOfWork;
        public ExcelLoader(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public abstract TestDataExcelDao LoadWorkbook(ExcelWorkbook workbook);
        public abstract TestDataExcelDao LoadWorksheets(IEnumerable<ExcelWorksheet> worksheets);
        public abstract void SaveWorkbookIntoDB(TestDataExcelDao testDataExcel);
        public abstract void SaveWorksheetsIntoDB(TestDataExcelDao testDataExcel);
    }
}
