using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelCommon.ExcelLoaders
{
    public class ExcelLoaderFactory : GeneralFactory
    {
        public ExcelLoaderFactory(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override void Factory()
        {
            TestDataExcelLoader = new TestDataExcelLoader(unitOfWork);
            OtherDataExcelLoader = new OtherDataExcelLoader(unitOfWork);
            //here, add other excel files loader
        }
    }
}
