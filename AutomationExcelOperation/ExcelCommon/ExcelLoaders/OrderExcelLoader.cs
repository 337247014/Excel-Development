using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ExcelCommon.Model;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.IO;
using OfficeOpenXml.Style;
using System.Drawing;

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
            //make all of columns auto fit width
            wsProducts.Cells[wsProducts.Dimension.Address].AutoFitColumns();
        }

        private void BuildUserWorkSheet(ExcelPackage package)
        {
            Console.WriteLine(@"build User worksheet by data");
            var wsUser = package.Workbook.Worksheets.Add("User");
            var list = unitOfWork.UserRepository.GetAll();

            //1.write data from D1 cell
            //2.just write specified columns 
            //3.order by UserName
            wsUser.Cells["D1"].LoadFromCollection(from item in list
                                                  orderby item.UserName ascending
                                                  select new {
                                                      UserId = item.UserId,
                                                      UserName = item.UserName,
                                                      Password = item.Password
                                                  }, true, TableStyles.Light10);
            //4.make all of columns auto fit width
            wsUser.Cells[wsUser.Dimension.Address].AutoFitColumns();
            //5.set font to Arial for all all cells
            wsUser.Cells.Style.Font.Name = "Arial";
            //6.set font to Bold from D1 to F1
            //  and set color to White (D1:F1) equals (1,4,1,6)
            wsUser.Cells["D1:F1"].Style.Font.Bold = true;
            wsUser.Cells[1,4,1,6].Style.Font.Color.SetColor(Color.Black);
        }
    }
}
