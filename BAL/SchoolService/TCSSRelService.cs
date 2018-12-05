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
    public class TCSSRelService : iTCSSRelService
    {
        private readonly cUnitOfWork _unitOfWork;
        private commonclass clsobj;


        public TCSSRelService(cUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this.clsobj = new commonclass(this._unitOfWork);
        }

        public Guid Create(TCSSRelModel tentity, string dbn)
        {
            using (var scope = new TransactionScope())
            {
                clsobj.SetDataBase(dbn);

                _unitOfWork.TCSSRelRepository.Insert(tentity);
                _unitOfWork.Save();
                scope.Complete();
                return tentity.TCSSRelModelid;
            }
        }

        public bool Delete(Guid tid, string dbn)
        {
            throw new NotImplementedException();
            
        }

        public IEnumerable<TCSSRelModel> GetAll(string dbn)
        {
            clsobj.SetDataBase(dbn);

            var results = _unitOfWork.TCSSRelRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public TCSSRelModel GetById(Guid tid, string dbn)
        {
            clsobj.SetDataBase(dbn);

            var result = _unitOfWork.TCSSRelRepository.GetByID(tid);
            if (result != null)
            {
                return result;
            }
            return null;
        }


        public bool Update(TCSSRelModel tentity, string dbn)
        {
            var success = false;
            if (tentity != null)
            {
                using (var scope = new TransactionScope())
                {
                    _unitOfWork.TCSSRelRepository.Update(tentity);
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

        public bool Update(Guid tid, TCSSRelModel tentity, string dbn)
        {
            throw new NotImplementedException();
        }

        public bool Delete(TCSSRelModel tentity, string dbn)
        {
            var success = false;

            using (var scope = new TransactionScope())
            {
                _unitOfWork.TCSSRelRepository.Delete(tentity);
                _unitOfWork.Save();
                scope.Complete();
                success = true;
            }
            return success;
        }

        public IEnumerable<TCSSRelModel> GetAllByAdmin(Guid teacherid, string dbn)
        {
            clsobj.SetDataBase(dbn);
            var includes = new string[] { "ForClass", "ForSection", "ForSubject" };
            var results = _unitOfWork.TCSSRelRepository.GetWithInclude(i=>i.Teacherid==teacherid, includes);
            if (results.Any())
            {
                return results;
            }
            return null;
        }
    }



}
