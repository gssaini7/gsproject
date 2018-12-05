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
    public class LedgerSumaryService : iLedgerSumaryService
    {
        private readonly cUnitOfWork _unitOfWork;

        private commonclass clsobj;

        public LedgerSumaryService(cUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this.clsobj = new commonclass(this._unitOfWork);
        }

        public IEnumerable<LedgerSumaryModel> GetAll(string dbn)
        {
            clsobj.SetDataBase(dbn);
            //_unitOfWork.SetDatabase(dbn);
            var results = _unitOfWork.LedgerSumaryRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public LedgerSumaryModel GetById(int tid, string dbn)
        {
            clsobj.SetDataBase(dbn);
            //_unitOfWork.SetDatabase(dbn);
            var result = _unitOfWork.LedgerSumaryRepository.Get(s=>s.StudentModelID==tid);
            if (result != null)
            {
                return result;
            }
            return null;
        }
    }


}
