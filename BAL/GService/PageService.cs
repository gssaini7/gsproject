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
    public class PageService : iPageService
    {
        private readonly cUnitOfWork _unitOfWork;

        private commonclass clsobj;

        public PageService(cUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this.clsobj = new commonclass(this._unitOfWork);
        }

        public Guid Create(PageModel tentity, string dbn)
        {
            using (var scope = new TransactionScope())
            {
                //_unitOfWork.SetDatabase(dbn);

                _unitOfWork.PageRepository.Insert(tentity);
                _unitOfWork.Save();
                scope.Complete();
                return tentity.PageModelid;
            }
        }

        public bool Delete(PageModel tentity, string dbn)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid tid, string dbn)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PageModel> GetAll(string dbn)
        {
            clsobj.SetDataBase(dbn);
            //_unitOfWork.SetDatabase(dbn);

            var results = _unitOfWork.PageRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public PageModel GetById(Guid tid, string dbn)
        {
            clsobj.SetDataBase(dbn);
            //_unitOfWork.SetDatabase(dbn);

            var result = _unitOfWork.PageRepository.GetByID(tid);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public PageModel GetByURL(string pageurl, string dbn)
        {
            clsobj.SetDataBase(dbn);
            //_unitOfWork.SetDatabase(dbn);
            var result = _unitOfWork.PageRepository.Get(u => u.pageurl == pageurl);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public bool Update(PageModel tentity, string dbn)
        {
            var success = false;
            if (tentity != null)
            {
                using (var scope = new TransactionScope())
                {
                    _unitOfWork.PageRepository.Update(tentity);
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

        public bool Update(Guid tid, PageModel tentity, string dbn)
        {
            throw new NotImplementedException();
        }
    }
}
