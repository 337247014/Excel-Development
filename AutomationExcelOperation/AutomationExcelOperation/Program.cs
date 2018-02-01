//using AutomationExcelOperation.Model;
using DAL;
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

            //DirectoryInfo outputDir = new DirectoryInfo(@"C:\");
            //if (!outputDir.Exists) throw new Exception("Output Directory doesn't exists.");
            
            FileInfo testDataExcelFile = new FileInfo("..\\..\\Data\\RawExcel\\TestData.xlsx");
            if (!testDataExcelFile.Exists) throw new Exception("TestData excel file doesn't exist.");

            ExcelPackage package = new ExcelPackage(testDataExcelFile);
            ExcelWorksheet workSheet = package.Workbook.Worksheets.FirstOrDefault();
            IEnumerable<Country> CountryList = PopulateCountry(workSheet, true);

            UnitOfWork unitOfWork = new UnitOfWork();
            foreach(var item in CountryList)
            {
                unitOfWork.CountryRepository.Insert(item);
                unitOfWork.Save();
            }

            Console.WriteLine("Countrys count: " + CountryList.Count());
            Console.ReadLine();
        }

        static IEnumerable<Country> PopulateCountry(ExcelWorksheet workSheet, bool firstRowHeader)
        {
            IList<Country> Countrys = new List<Country>();

            if (workSheet != null)
            {
                Dictionary<string, int> header = new Dictionary<string, int>();

                for (int rowIndex = workSheet.Dimension.Start.Row; rowIndex <= workSheet.Dimension.End.Row; rowIndex++)
                {
                    //Assume the first row is the header. Then use the column match ups by name to determine the index.
                    //This will allow you to have the order of the columns change without any affect.
                    if (rowIndex == 1 && firstRowHeader)
                    {
                        header = ExcelHelper.GetExcelHeader(workSheet, rowIndex);
                    }
                    else
                    {
                        Countrys.Add(new Country
                        {
                            CountryKey = ExcelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "CountryKey"),
                            CountryName = ExcelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "CountryName"),
                            CurrencyCode = ExcelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "CurrencyCode"),
                            AllowsImpatriateExpenses = Convert.ToBoolean(ExcelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "AllowsImpatriateExpenses")),
                            CountryKeySAP = ExcelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "CountryKeySAP"),
                            AmexCountryISOCd = ExcelHelper.ConvertValueToInt(ExcelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "AmexCountryISOCd"))
                        });

                    }
                }
            }

            return Countrys;
        }

        public static class ExcelHelper
        {
            public static Dictionary<string, int> GetExcelHeader(ExcelWorksheet workSheet, int rowIndex)
            {
                Dictionary<string, int> header = new Dictionary<string, int>();

                if (workSheet != null)
                {
                    for (int columnIndex = workSheet.Dimension.Start.Column; columnIndex <= workSheet.Dimension.End.Column; columnIndex++)
                    {
                        if (workSheet.Cells[rowIndex, columnIndex].Value != null)
                        {
                            string columnName = workSheet.Cells[rowIndex, columnIndex].Value.ToString();

                            if (!header.ContainsKey(columnName) && !string.IsNullOrEmpty(columnName))
                            {
                                header.Add(columnName, columnIndex);
                            }
                        }
                    }
                }

                return header;
            }
            public static string ParseWorksheetValue(ExcelWorksheet workSheet, Dictionary<string, int> header, int rowIndex, string columnName)
            {
                string value = string.Empty;
                int? columnIndex = header.ContainsKey(columnName) ? header[columnName] : (int?)null;

                if (workSheet != null && columnIndex != null && workSheet.Cells[rowIndex, columnIndex.Value].Value != null)
                {
                    value = workSheet.Cells[rowIndex, columnIndex.Value].Value.ToString();
                }

                return value;
            }
            public static int? ConvertValueToInt(string value)
            {
                int? temp;
                if (string.IsNullOrEmpty(value))
                {
                    temp = null;
                }
                else
                {
                    temp = Convert.ToInt32(value);
                }

                return temp;
            }
        }

    }
}
