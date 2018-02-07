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

            BuildOrderWorkbook(args);

            //finally save all of excel data
            //unitOfWork.Save();
            Console.ForegroundColor = ConsoleColor.White;
            ConsoleHelper.ShowLineBreak();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Finished!");
            Console.ReadLine();
        }

        private static void ProcessTestData(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            ConsoleHelper.ShowLineBreak();
            if (args.ArgsContain(ConstantHelper.LoadTestDataArgs))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(@"Loading TestData Excel file");
                Console.WriteLine();
                LoadTestData();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(@"***Test Data is not being loaded***");
            }
        }

        private static void ProcessOtherData(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            ConsoleHelper.ShowLineBreak();
            if (args.ArgsContain(ConstantHelper.LoadOtherDataArgs))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(@"Loading OtherData Excel file");
                Console.WriteLine();
                LoadOtherData();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(@"***Other Data is not being loaded***");
            }
        }

        private static void BuildOrderWorkbook(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            ConsoleHelper.ShowLineBreak();
            if (args.ArgsContain(ConstantHelper.BuildOrderExcelArgs))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(@"Build Order Excel file");
                Console.WriteLine();
                GeneralFactory factory = new OrderExcelFactory(unitOfWork);
                excelProcessor.GenerateWorkbook(factory, ConstantHelper.OrderExcelFileLocation);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(@"***Order excel is not being built***");
            }
        }

        /// <summary>
        /// read testData.xlsx and save it into db
        /// </summary>
        private static void LoadTestData()
        {
            //FileInfo testDataExcelFile = new FileInfo("..\\..\\..\\AutomationExcelOperation\\Data\\RawExcel\\TestData.xlsx");
            FileInfo testDataExcelFile = new FileInfo(ConstantHelper.TestDataExcelFileLocation);
            GeneralFactory testDataExcelLoaderFactory = new TestDataExcelLoaderFactory(unitOfWork);
            IExcelDao testData = excelProcessor.LoadExcel(testDataExcelLoaderFactory, testDataExcelFile);
            excelProcessor.SaveDataIntoDB(testDataExcelLoaderFactory, testData);
        }

        /// <summary>
        /// read otherData.xlsx and save it into db
        /// </summary>
        private static void LoadOtherData()
        {
            //FileInfo otherDataExcelFile = new FileInfo("..\\..\\..\\AutomationExcelOperation\\Data\\RawExcel\\OtherData.xlsx");
            FileInfo otherDataExcelFile = new FileInfo(ConstantHelper.OtherDataExcelFileLocation);
            GeneralFactory otherDataExcelLoaderFactory = new OtherDataExcelLoaderFactory(unitOfWork);
            IExcelDao otherData = excelProcessor.LoadExcel(otherDataExcelLoaderFactory, otherDataExcelFile);
            excelProcessor.SaveDataIntoDB(otherDataExcelLoaderFactory, otherData);
        }

    }
}
