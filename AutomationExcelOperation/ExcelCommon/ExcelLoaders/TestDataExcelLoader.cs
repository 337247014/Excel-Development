using DAL;
using ExcelCommon.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelCommon.ExcelLoaders
{
    public class TestDataExcelLoader : ExcelLoader
    {
        private ExcelHelper excelHelper;

        public TestDataExcelLoader(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            excelHelper = new ExcelHelper();
        }

        public override TestDataExcelDao LoadWorkbook(ExcelWorkbook workbook)
        {
            TestDataExcelDao testDataExcel = new TestDataExcelDao();

            testDataExcel.Country = this.PopulateCountry(workbook.Worksheets["Country"], true);

            return testDataExcel;
        }

        public override TestDataExcelDao LoadWorksheets(IEnumerable<ExcelWorksheet> worksheets)
        {
            throw new NotImplementedException();
        }

        public override void SaveWorkbookIntoDB(TestDataExcelDao testDataExcel)
        {
            testDataExcel.Country.ToList().ForEach(x => unitOfWork.CountryRepository.Insert(x));
            unitOfWork.Save();
            Console.WriteLine("Countrys count: " + testDataExcel.Country.Count());
        }

        public override void SaveWorksheetsIntoDB(TestDataExcelDao testDataExcel)
        {
            if(testDataExcel.Country.ToList().Count != 0)
            {
                testDataExcel.Country.ToList().ForEach(x => unitOfWork.CountryRepository.Insert(x));
            }
            unitOfWork.Save();
            Console.WriteLine("Countrys count: " + testDataExcel.Country.Count());
        }

        private IEnumerable<Country> PopulateCountry(ExcelWorksheet workSheet, bool firstRowHeader)
        {
            IList<Country> Countrys = new List<Country>();

            if (workSheet != null)
            {
                Dictionary<string, int> header = new Dictionary<string, int>();

                for (int rowIndex = workSheet.Dimension.Start.Row; rowIndex <= workSheet.Dimension.End.Row; rowIndex++)
                {
                    //Assume the first row is the header. Then use the column match ups by name to determine the index.
                    if (rowIndex == 1 && firstRowHeader)
                    {
                        header = excelHelper.GetExcelHeader(workSheet, rowIndex);
                    }
                    else
                    {
                        Countrys.Add(new Country
                        {
                            CountryKey = excelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "CountryKey"),
                            CountryName = excelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "CountryName"),
                            CurrencyCode = excelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "CurrencyCode"),
                            AllowsImpatriateExpenses = Convert.ToBoolean(excelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "AllowsImpatriateExpenses")),
                            CountryKeySAP = excelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "CountryKeySAP"),
                            AmexCountryISOCd = excelHelper.ConvertValueToInt(excelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "AmexCountryISOCd"))
                        });

                    }
                }
            }

            return Countrys;
        }
    }
    
}
