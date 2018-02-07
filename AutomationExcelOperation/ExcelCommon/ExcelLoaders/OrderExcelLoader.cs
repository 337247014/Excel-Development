﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ExcelCommon.Model;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.IO;

namespace ExcelCommon.ExcelLoaders
{
    public class OrderExcelLoader : ExcelLoader
    {
        public OrderExcelLoader(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override void GenerateWorkbook(string fileLocation)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"Ready for generate Order.xlsx");

            ExcelPackage package = new ExcelPackage();
            this.BuildProductsWorkSheet(package);
            this.BuildUserWorkSheet(package);
            SaveWorkbook(package, fileLocation);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"Generate sucessfully!");
        }

        public override IExcelDao LoadWorkbook(ExcelWorkbook workbook)
        {
            throw new NotImplementedException();
        }

        public override IExcelDao LoadWorksheets(IEnumerable<ExcelWorksheet> worksheets)
        {
            throw new NotImplementedException();
        }

        public override void SaveWorkbookIntoDB(IExcelDao data)
        {
            throw new NotImplementedException();
        }

        public override void SaveWorksheetsIntoDB(IExcelDao data)
        {
            throw new NotImplementedException();
        }

        private void BuildProductsWorkSheet(ExcelPackage package)
        {
            Console.WriteLine(@"build Products worksheet by data");
            var wsProducts = package.Workbook.Worksheets.Add("Products");
            wsProducts.Cells["A1"].LoadFromCollection(unitOfWork.ProductRepository.GetAll().ToList(), true, TableStyles.Medium9);
        }

        private void BuildUserWorkSheet(ExcelPackage package)
        {
            Console.WriteLine(@"build User worksheet by data");
            var wsUser = package.Workbook.Worksheets.Add("User");
            wsUser.Cells["A1"].LoadFromCollection(unitOfWork.UserRepository.GetAll().ToList(), true, TableStyles.Light10);
        }
    }
}
