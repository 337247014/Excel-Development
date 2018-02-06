using System;
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
                                : @"*******************Running Excel Processor in Update Mode*************************");
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
            Console.WriteLine(@"Usage:");
            Console.WriteLine(AsteriksLine);
            Console.WriteLine();
            Console.WriteLine(@"No Arguments - Normal mode (default: Update)");
            Console.WriteLine(@"{0} -> help", ConstantHelper.HelpArguments.ToOrSeparated());
            Console.WriteLine(@"{0} -> run in Delete & Create Mode", ConstantHelper.DeleteArguments.ToOrSeparated());
            Console.WriteLine();
            Console.WriteLine(@"{0} -> run Test Data excel file", ConstantHelper.LoadTestDataArgs.ToOrSeparated());
            Console.WriteLine(@"{0} -> run Other Data excel file", ConstantHelper.LoadOtherDataArgs.ToOrSeparated());
            ShowLineBreak();
            Console.ReadLine();
        }
    }
}
