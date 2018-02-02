using DAL;
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
        public TestDataExcelLoader()
        {
            excelHelper = new ExcelHelper();
        }
        public override IEnumerable<Country> LoadExcel(ExcelWorksheet workSheet)
        {
            //FileInfo testDataExcelFile = new FileInfo("..\\..\\..\\..\\AutomationExcelOperation\\Data\\RawExcel\\TestData.xlsx");
            //if (!testDataExcelFile.Exists) throw new Exception("TestData excel file doesn't exist.");
            //ExcelPackage package = new ExcelPackage(testDataExcelFile);
            //ExcelWorksheet workSheet = package.Workbook.Worksheets.FirstOrDefault();

            var countryList = this.PopulateCountry(workSheet, true);
            return countryList;
        }

        public override void LoadWorkSheet()
        {
            throw new NotImplementedException();
        }

        public override void SaveExcelIntoDB()
        {
            throw new NotImplementedException();
        }

        public override void SaveWorkSheetIntoDB()
        {
            throw new NotImplementedException();
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
