using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace ExcelCommon.ExcelLoaders
{
    public class OtherDataExcelLoaderFactory : GeneralFactory
    {
        public OtherDataExcelLoaderFactory(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override ExcelLoader CreateInstance()
        {
            return new OtherDataExcelLoader(unitOfWork);
        }
    }
}
