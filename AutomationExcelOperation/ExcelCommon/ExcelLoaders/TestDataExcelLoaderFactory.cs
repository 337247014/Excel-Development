using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelCommon.ExcelLoaders
{
    public class TestDataExcelLoaderFactory : GeneralFactory
    {
        public TestDataExcelLoaderFactory(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override ExcelLoader CreateInstance()
        {
            return new TestDataExcelLoader(unitOfWork);
        }
    }
}
