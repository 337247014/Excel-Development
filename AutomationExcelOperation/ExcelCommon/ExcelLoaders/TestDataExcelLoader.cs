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

        public override IExcelDao LoadWorkbook(ExcelWorkbook workbook)
        {
            TestDataExcelDao testDataExcel = new TestDataExcelDao();

            testDataExcel.Country = this.CreateCountry(workbook.Worksheets["Country"], true);
            testDataExcel.Company = this.CreateCompany(workbook.Worksheets["Company"], true);
            testDataExcel.ValidationRule = this.CreateValidationRule(workbook.Worksheets["ValidationRule"], true);

            return testDataExcel;
        }

        public override IExcelDao LoadWorksheets(IEnumerable<ExcelWorksheet> worksheets)
        {
            throw new NotImplementedException();
        }

        public override void SaveWorkbookIntoDB(IExcelDao data )
        {
            TestDataExcelDao testData = (TestDataExcelDao)data;
            testData.Country.ToList().ForEach(x => unitOfWork.CountryRepository.Insert(x));
            testData.Company.ToList().ForEach(x => unitOfWork.CompanyRepository.Insert(x));
            testData.ValidationRule.ToList().ForEach(x => unitOfWork.ValidationRuleRepository.Insert(x));
            unitOfWork.Save();
            Console.WriteLine("Countrys count: " + testData.Country.Count());
        }

        public override void SaveWorksheetsIntoDB(IExcelDao data)
        {
            TestDataExcelDao testData = (TestDataExcelDao)data;
            if (testData.Country.ToList().Count != 0)
            {
                testData.Country.ToList().ForEach(x => unitOfWork.CountryRepository.Insert(x));
            }
            unitOfWork.Save();
            Console.WriteLine("Countrys count: " + testData.Country.Count());
        }

        private IEnumerable<Country> CreateCountry(ExcelWorksheet workSheet, bool firstRowHeader)
        {
            IList<Country> Countries = new List<Country>();

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
                        Countries.Add(new Country
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

            return Countries;
        }

        private IEnumerable<Company> CreateCompany(ExcelWorksheet workSheet, bool firstRowHeader)
        {
            IList<Company> companyList = new List<Company>();

            if (workSheet != null)
            {
                Dictionary<string, int> header = new Dictionary<string, int>();

                for (int rowIndex = workSheet.Dimension.Start.Row; rowIndex <= workSheet.Dimension.End.Row; rowIndex++)
                {
                    if (rowIndex == 1 && firstRowHeader)
                    {
                        header = excelHelper.GetExcelHeader(workSheet, rowIndex);
                    }
                    else
                    {
                        companyList.Add(new Company
                        {
                            CountryKey = excelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "CountryKey"),
                            CompanyCode = excelHelper.ParseWorksheetValue(workSheet,header,rowIndex, "CompanyCode")
                        });

                    }
                }
            }

            return companyList;
        }

        private IEnumerable<ValidationRule> CreateValidationRule(ExcelWorksheet workSheet, bool firstRowHeader)
        {
            IList<ValidationRule> ValidationRules = new List<ValidationRule>();

            if (workSheet != null)
            {
                Dictionary<string, int> header = new Dictionary<string, int>();

                for (int rowIndex = workSheet.Dimension.Start.Row; rowIndex <= workSheet.Dimension.End.Row; rowIndex++)
                {
                    if (rowIndex == 1 && firstRowHeader)
                    {
                        header = excelHelper.GetExcelHeader(workSheet, rowIndex);
                    }
                    else
                    {
                        ValidationRules.Add(new ValidationRule
                        {
                            ExpenseTypeCd = excelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "ExpenseTypeCd"),
                            TriggeredOn = excelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "TriggeredOn"),
                            IsEnabledInd = Convert.ToBoolean(excelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "IsEnabledInd")),
                            SeverityDesc = excelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "SeverityDesc"),
                            RuleNm = excelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "RuleNm"),
                            ValidationRuleActionTypeGroupNbr = excelHelper.ConvertValueToInt(excelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "ValidationRuleActionTypeGroupNbr")),
                            ErrorMsgDesc = excelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "ErrorMsgDesc"),
                            Sequence = excelHelper.ConvertValueToInt(excelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "Sequence"))
                        });

                    }
                }
            }

            return ValidationRules;
        }
    }
    
}
