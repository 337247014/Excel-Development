using AutomationExcelOperation.Helpers;
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
        private static UnitOfWork unitOfWork = new UnitOfWork();
        private static ExcelProcessor excelProcessor = new ExcelProcessor();

        static void Main(string[] args)
        {
            ConsoleHelper.ShowHeadlineMessage(false);
            ConsoleHelper.PrepareToOperateExcel();

            if (StringHelper.IsInvalidParameters(args))
            {
                ConsoleHelper.ShowErrorMessage();
                return;
            }

            if (args.ArgsContain(ConstantHelper.HelpArguments))
            {
                ConsoleHelper.PrintHelp();
                return;
            }

            ProcessTestData(args);
            ProcessOtherData(args);

            //finally save all of excel data
            //unitOfWork.Save();
            ConsoleHelper.ShowLineBreak();
            Console.WriteLine("Finished!");
            Console.ReadLine();
        }

        private static void ProcessTestData(string[] args)
        {
            ConsoleHelper.ShowLineBreak();
            if (args.ArgsContain(ConstantHelper.LoadTestDataArgs))
            {
                Console.WriteLine(@"Loading TestData Excel file");
                ConsoleHelper.ShowLineBreak();
                LoadTestData();
            }
            else
            {
                Console.WriteLine(@"***Test Data is not being loaded***");
                ConsoleHelper.ShowLineBreak();
            }
        }

        private static void ProcessOtherData(string[] args)
        {
            ConsoleHelper.ShowLineBreak();
            if (args.ArgsContain(ConstantHelper.LoadOtherDataArgs))
            {
                Console.WriteLine(@"Loading OtherData Excel file");
                ConsoleHelper.ShowLineBreak();
                LoadOtherData();
            }
            else
            {
                Console.WriteLine(@"***Other Data is not being loaded***");
                ConsoleHelper.ShowLineBreak();
            }
        }

        /// <summary>
        /// read testData.xlsx and save it into db
        /// </summary>
        private static void LoadTestData()
        {
            FileInfo testDataExcelFile = new FileInfo("..\\..\\..\\AutomationExcelOperation\\Data\\RawExcel\\TestData.xlsx");
            GeneralFactory testDataExcelLoaderFactory = new TestDataExcelLoaderFactory(unitOfWork);
            IExcelDao testData = excelProcessor.LoadExcel(testDataExcelLoaderFactory, testDataExcelFile);
            excelProcessor.SaveDataIntoDB(testDataExcelLoaderFactory, testData);
        }

        /// <summary>
        /// read otherData.xlsx and save it into db
        /// </summary>
        private static void LoadOtherData()
        {
            FileInfo otherDataExcelFile = new FileInfo("..\\..\\..\\AutomationExcelOperation\\Data\\RawExcel\\OtherData.xlsx");
            GeneralFactory otherDataExcelLoaderFactory = new OtherDataExcelLoaderFactory(unitOfWork);
            IExcelDao otherData = excelProcessor.LoadExcel(otherDataExcelLoaderFactory, otherDataExcelFile);
            excelProcessor.SaveDataIntoDB(otherDataExcelLoaderFactory, otherData);
        }

    }
}
