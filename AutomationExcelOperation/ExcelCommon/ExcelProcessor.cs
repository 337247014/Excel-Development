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

        private IExcelDao testDataExcel = new TestDataExcelDao();
        private IExcelDao otherDataExcel = new OtherDataExcelDao();

        public void LoadExcel()
        {
            FileInfo testDataExcelFile = new FileInfo("..\\..\\..\\AutomationExcelOperation\\Data\\RawExcel\\TestData.xlsx");
            if (!testDataExcelFile.Exists) throw new Exception("the excel file doesn't exist.");
            ExcelPackage package = new ExcelPackage(testDataExcelFile);
            testDataExcel = GeneralFactory.TestDataExcelLoader.LoadWorkbook(package.Workbook);

            FileInfo otherDataExcelFile = new FileInfo("..\\..\\..\\AutomationExcelOperation\\Data\\RawExcel\\OtherData.xlsx");
            if (!otherDataExcelFile.Exists) throw new Exception("OtherData excel file doesn't exist.");
            ExcelPackage package1 = new ExcelPackage(otherDataExcelFile);
            otherDataExcel = GeneralFactory.OtherDataExcelLoader.LoadWorkbook(package1.Workbook);

        }

        public void SaveDataIntoDB()
        {
            GeneralFactory.TestDataExcelLoader.SaveWorkbookIntoDB(testDataExcel);
            GeneralFactory.OtherDataExcelLoader.SaveWorkbookIntoDB(otherDataExcel);
        }

    }
}
