using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelCommon.ExcelLoaders
{
    public abstract class GeneralFactory
    {
        protected UnitOfWork unitOfWork;
        public GeneralFactory(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public abstract ExcelLoader CreateInstance();
    }
}
