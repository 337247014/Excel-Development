﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationExcelOperation.Helpers
{
    public static class ConsoleHelper
    {
        private const string AsteriksLine = @"*****************************************************************************";
        public static void ShowHeadlineMessage(bool isDeleteExistData)
        {
            Console.WriteLine(AsteriksLine);
            Console.WriteLine(isDeleteExistData
                                ? @"*******************Running Excel Processor in Delete & Create Mode********************"
                                : @"*******************Running Excel Processor in Update Mode********************");
            Console.WriteLine(AsteriksLine);
            Console.WriteLine();
        }
        public static void ShowLineBreak()
        {
            Console.WriteLine();
            Console.WriteLine(AsteriksLine);
            Console.WriteLine();
        }
        public static void PrepareToOperateExcel()
        {
            Console.WriteLine();
            Console.WriteLine(@"Preparing to populate database.");
            Console.WriteLine();
        }
        public static void ShowErrorMessage()
        {
            Console.WriteLine(AsteriksLine);
            Console.WriteLine(@"NOT Running Data Processor: There is at least one parameter that was not recognized.");
            Console.WriteLine(@"If you don't know about parameters please run it with '-help' to show info");
            Console.WriteLine(AsteriksLine);
            Console.WriteLine();
        }
        public static void PrintHelp()
        {
            Console.WriteLine(AsteriksLine);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"Usage:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(AsteriksLine);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"No Arguments - Normal mode (default: Update)");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"{0} -> help", ConstantHelper.HelpArguments.ToOrSeparated());
            Console.WriteLine(@"{0} -> run in Delete & Create Mode", ConstantHelper.DeleteArguments.ToOrSeparated());
            Console.WriteLine();
            Console.WriteLine(@"{0} -> read TestData excel file", ConstantHelper.LoadTestDataArgs.ToOrSeparated());
            Console.WriteLine(@"{0} -> read OtherData excel file", ConstantHelper.LoadOtherDataArgs.ToOrSeparated());
            Console.WriteLine();
            Console.WriteLine(@"{0} -> build Order excel file", ConstantHelper.BuildOrderExcelArgs.ToOrSeparated());
            Console.ForegroundColor = ConsoleColor.White;
            ShowLineBreak();
            Console.ReadLine();
        }
    }
}
