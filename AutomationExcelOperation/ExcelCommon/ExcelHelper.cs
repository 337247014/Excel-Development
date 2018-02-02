using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelCommon
{
    public class ExcelHelper
    {
        public Dictionary<string, int> GetExcelHeader(ExcelWorksheet workSheet, int rowIndex)
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
        public string ParseWorksheetValue(ExcelWorksheet workSheet, Dictionary<string, int> header, int rowIndex, string columnName)
        {
            string value = string.Empty;
            int? columnIndex = header.ContainsKey(columnName) ? header[columnName] : (int?)null;

            if (workSheet != null && columnIndex != null && workSheet.Cells[rowIndex, columnIndex.Value].Value != null)
            {
                value = workSheet.Cells[rowIndex, columnIndex.Value].Value.ToString();
            }

            return value;
        }
        public int? ConvertValueToInt(string value)
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
