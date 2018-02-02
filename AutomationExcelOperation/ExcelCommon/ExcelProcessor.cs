using DAL;
using ExcelCommon.ExcelLoaders;
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
            GeneralFactory = new ExcelLoaderFactory();
        }

        public IEnumerable<Country> CountryList { get; set; }

        public void LoadExcel()
        {
            FileInfo testDataExcelFile = new FileInfo("..\\..\\..\\AutomationExcelOperation\\Data\\RawExcel\\TestData.xlsx");
            if (!testDataExcelFile.Exists) throw new Exception("TestData excel file doesn't exist.");
            ExcelPackage package = new ExcelPackage(testDataExcelFile);
            ExcelWorksheet workSheet = package.Workbook.Worksheets.FirstOrDefault();

            CountryList = GeneralFactory.TestDataExcelLoader.LoadExcel(workSheet);
        }

        public void SaveDataIntoDB()
        {
            foreach (var item in CountryList)
            {
                unitOfWork.CountryRepository.Insert(item);
                unitOfWork.Save();
            }
            Console.WriteLine("Countrys count: " + CountryList.Count());
        }

    }
}
