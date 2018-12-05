using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using R.DAL.UnitOfWork;
using R.BusinessEntities;
using System.Transactions;
using DAL;
using System.Net.Mail;
using System.Security.Cryptography;
using System.IO;
using System.Net;

namespace R.BAL
{
    public class LedgerDetailService : iLedgerDetailService
    {
        private readonly cUnitOfWork _unitOfWork;

        private commonclass clsobj;

        public LedgerDetailService(cUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this.clsobj = new commonclass(this._unitOfWork);
        }

        public IEnumerable<LedgerDetailModel> GetAll(string dbn)
        {
            clsobj.SetDataBase(dbn);
            //_unitOfWork.SetDatabase(dbn);
            var results = _unitOfWork.LedgerDetailRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public IEnumerable<LedgerDetailModel> GetAllByStudent(int stuid, string dbn)
        {
            clsobj.SetDataBase(dbn);
            //_unitOfWork.SetDatabase(dbn);
            var results = _unitOfWork.LedgerDetailRepository.GetMany(s=>s.StudentModelID==stuid);
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public LedgerDetailModel GetById(int tid, string dbn)
        {
            throw new NotImplementedException();

        }
    }


}
