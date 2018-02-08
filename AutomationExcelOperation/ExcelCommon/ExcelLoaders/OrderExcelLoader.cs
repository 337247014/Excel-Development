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
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Drawing;

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
            wsUser.Cells["F2:F5"].Style.Numberformat.Format = "#,##0.00";

            CreatePieChart(wsUser, "User", new int[] { 0, 0, 6, 0 }, new int[] { 400, 400 }, new ExcelAddress(2, 6, 5, 6), "E2:E5");
        }

        /// <summary>
        /// create a pie chart
        /// </summary>
        /// <param name="worksheet">create chart on this worksheet</param>
        /// <param name="title">tiltle of pie chart</param>
        /// <param name="position">specify the detail position on worksheet, like 0,0,5,5 for array value</param>
        /// <param name="size">specify width and height of chart, like 600,300 for array value</param>
        /// <param name="serieAddress">specify the value area of chart, like E2:E6</param>
        /// <param name="xSerieAdress">specify the revelent legend area of chart, like B2:B6</param>
        private void CreatePieChart(ExcelWorksheet worksheet,string title, int[] position,int[] size, ExcelAddress serieAddress, string xSerieAdress)
        {
            var chart = (worksheet.Drawings.AddChart("PieChart", eChartType.Pie3D) as ExcelPieChart);

            chart.Title.Text = title;
            //From row position[0] colum position[2] with some pixels offset
            chart.SetPosition(position[0], position[1], position[2], position[3]);

            //set chart size with size[0]px width and size[1] height
            chart.SetSize(size[0], size[1]);

            //here set data of pie chart, specify serie value and relevent type
            //var ser = (chart.Series.Add(serieAddress, xSerieAdress) as ExcelPieChartSerie);
            ExcelAddress valueAddress = serieAddress;
            var ser = (chart.Series.Add(valueAddress.Address, xSerieAdress) as ExcelPieChartSerie);
            chart.DataLabel.ShowCategory = true;
            chart.DataLabel.ShowPercent = true;

            //set legend format
            chart.Legend.Border.LineStyle = eLineStyle.Solid;
            chart.Legend.Border.Fill.Style = eFillStyle.SolidFill;
            chart.Legend.Border.Fill.Color = Color.DarkBlue;

            //Switch the PageLayoutView back to normal
            worksheet.View.PageLayoutView = false;
        }
    }
}
