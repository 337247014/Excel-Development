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
        private UnitOfWork unitOfWork;
        public GeneralFactory GeneralFactory { get; set; }
        public ExcelProcessor()
        {
            unitOfWork = new UnitOfWork();
            GeneralFactory = new ExcelLoaderFactory(unitOfWork);
        }
        public IEnumerable<Country> CountryList { get; set; }
        private TestDataExcelDao testDataExcel = new TestDataExcelDao();

        public void LoadExcel()
        {
            FileInfo testDataExcelFile = new FileInfo("..\\..\\..\\AutomationExcelOperation\\Data\\RawExcel\\TestData.xlsx");
            if (!testDataExcelFile.Exists) throw new Exception("TestData excel file doesn't exist.");
            ExcelPackage package = new ExcelPackage(testDataExcelFile);
            List<ExcelWorksheet> worksheets = new List<ExcelWorksheet>();
            worksheets.Add(package.Workbook.Worksheets.FirstOrDefault());

            testDataExcel = GeneralFactory.TestDataExcelLoader.LoadWorkbook(package.Workbook);
        }

        public void SaveDataIntoDB()
        {
            GeneralFactory.TestDataExcelLoader.SaveWorkbookIntoDB(testDataExcel);
        }

    }
}
