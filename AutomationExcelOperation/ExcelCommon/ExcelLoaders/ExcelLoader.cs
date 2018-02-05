﻿using DAL;
using ExcelCommon.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelCommon.ExcelLoaders
{
    public abstract class ExcelLoader
    {
        protected UnitOfWork unitOfWork;
        public ExcelLoader(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public abstract IExcelDao LoadWorkbook(ExcelWorkbook workbook);
        public abstract IExcelDao LoadWorksheets(IEnumerable<ExcelWorksheet> worksheets);
        public abstract void SaveWorkbookIntoDB(IExcelDao data);
        public abstract void SaveWorksheetsIntoDB(IExcelDao data);
    }
}
