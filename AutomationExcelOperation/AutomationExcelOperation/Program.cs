using DAL;
using ExcelCommon;
using ExcelCommon.ExcelLoaders;
using ExcelCommon.Model;
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
            UnitOfWork unitOfWork = new UnitOfWork();
            ExcelProcessor excelProcessor = new ExcelProcessor();

            //read testData.xlsx and save it into db
            FileInfo testDataExcelFile = new FileInfo("..\\..\\..\\AutomationExcelOperation\\Data\\RawExcel\\TestData.xlsx");
            GeneralFactory testDataExcelLoaderFactory = new TestDataExcelLoaderFactory(unitOfWork);
            IExcelDao testData = excelProcessor.LoadExcel(testDataExcelLoaderFactory, testDataExcelFile);
            excelProcessor.SaveDataIntoDB(testDataExcelLoaderFactory, testData);

            //read otherData.xlsx and save it into db
            FileInfo otherDataExcelFile = new FileInfo("..\\..\\..\\AutomationExcelOperation\\Data\\RawExcel\\OtherData.xlsx");
            GeneralFactory otherDataExcelLoaderFactory = new OtherDataExcelLoaderFactory(unitOfWork);
            IExcelDao otherData = excelProcessor.LoadExcel(otherDataExcelLoaderFactory, otherDataExcelFile);
            excelProcessor.SaveDataIntoDB(otherDataExcelLoaderFactory, otherData);

            //finally save all of excel data
            unitOfWork.Save();
            Console.ReadLine();
        }

    }
}
