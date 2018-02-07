using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace ExcelCommon.ExcelLoaders
{
    public class OrderExcelFactory : GeneralFactory
    {
        public OrderExcelFactory(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override ExcelLoader CreateInstance()
        {
            return new OrderExcelLoader(unitOfWork);
        }
    }
}
