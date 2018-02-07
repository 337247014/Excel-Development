using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationExcelOperation.Helpers
{
    /// <summary>
    /// this is a extension class of string and its extension methods
    /// </summary>
    public static class StringHelper
    {
        public static bool ArgsContain(this string[] args, params string[] findMatch)
        {
            return args != null && args.Length > 0 && findMatch.Length > 0 &&
                   (from find in findMatch where args.Contains(find) select find).Any();
        }

        public static string ToOrSeparated(this string[] args)
        {
            var orSep = new StringBuilder();
            for (var i = 0; i < args.Length; i++)
            {
                orSep.Append(args[i]);
                if ((i + 1) != args.Length)
                    orSep.Append(@" or ");
            }
            return orSep.ToString();
        }

        public static bool IsInvalidParameters(IEnumerable<string> args)
        {
            return args.Any(arg => (!ConstantHelper.LoadTestDataArgs.Contains(arg) &&
                                    !ConstantHelper.LoadOtherDataArgs.Contains(arg) &&
                                    !ConstantHelper.HelpArguments.Contains(arg) &&
                                    !ConstantHelper.DeleteArguments.Contains(arg) &&
                                    !ConstantHelper.BuildOrderExcelArgs.Contains(arg)
                                    ));
        }

        public static int ToInt(this string str)
        {
            int value;
            int.TryParse(str, out value);
            return value;
        }

        public static int WordCount(this String str)
        {
            return str.Split(new char[] { ' ', '.', '?' },
                             StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
}
