using R.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.BAL
{
    public class commonclass
    {
        private readonly cUnitOfWork _unitOfWork;

        public commonclass(cUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void SetDataBase(string dbid)
        {
            if (dbid != null)
            {
                var guidid = new Guid(dbid);
                var maindbresult = _unitOfWork.MainDatabasesRepository.GetByID(guidid);
                _unitOfWork.SetDatabase(maindbresult);
            }
        }

    }
}
