﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ExcelCommon.Model;
using OfficeOpenXml;
using System.Diagnostics;

namespace ExcelCommon.ExcelLoaders
{
    public class OtherDataExcelLoader : ExcelLoader
    {
        private ExcelHelper excelHelper;
        public OtherDataExcelLoader(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            excelHelper = new ExcelHelper();
        }

        public override void GenerateWorkbook(string fileLocation)
        {
            throw new NotImplementedException();
        }

        public override IExcelDao LoadWorkbook(ExcelWorkbook workbook)
        {
            OtherDataExcelDao otherDataExcel = new OtherDataExcelDao();

            otherDataExcel.WebChatLinks = this.CreateWebChatLink(workbook.Worksheets["WebChatLinks"], true);

            return otherDataExcel;
        }

        public override IExcelDao LoadWorksheets(IEnumerable<ExcelWorksheet> worksheets)
        {
            throw new NotImplementedException();
        }

        public override void SaveWorkbookIntoDB(IExcelDao data)
        {
            Console.WriteLine(@"it is going to save excel data into DB");
            OtherDataExcelDao otherData = (OtherDataExcelDao)data;
            otherData.WebChatLinks.ToList().ForEach(x => unitOfWork.WebChatLinkRepository.Insert(x));

            Stopwatch sw = new Stopwatch();
            sw.Start();
            unitOfWork.Save();
            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            Console.WriteLine(@"it take {0}ms to save", ts.TotalMilliseconds);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"Save sucessfully");
        }

        public override void SaveWorksheetsIntoDB(IExcelDao data)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<WebChatLink> CreateWebChatLink(ExcelWorksheet workSheet, bool firstRowHeader)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"Read data from WebChatLink worksheet");
            IList<WebChatLink> webChatLinks = new List<WebChatLink>();

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
                        webChatLinks.Add(new WebChatLink
                        {
                            CountryKey = excelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "CountryKey"),
                            Language = excelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "Language"),
                            Link = excelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "Link"),
                        });

                    }
                }
            }

            return webChatLinks;
        }
    }
}
