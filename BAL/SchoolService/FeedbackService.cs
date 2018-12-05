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
    public class FeedbackService : iFeedbackService
    {
        private readonly cUnitOfWork _unitOfWork;
        private commonclass clsobj;


        public FeedbackService(cUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this.clsobj = new commonclass(this._unitOfWork);
        }

        public Guid Create(FeedbackModel tentity,string dbn)
        {
            using (var scope = new TransactionScope())
            {
                clsobj.SetDataBase(dbn);

                _unitOfWork.FeedbackRepository.Insert(tentity);
                _unitOfWork.Save();
                scope.Complete();
                return tentity.FeedbackModelid;
            }
        }

        public bool Delete(Guid tid, string dbn)
        {
            throw new NotImplementedException();
            
        }

        public IEnumerable<FeedbackModel> GetAll(string dbn)
        {
            clsobj.SetDataBase(dbn);
            var includes = new string[] { "ParentOfStudent" };
            var results = _unitOfWork.FeedbackRepository.GetWithInclude2(includes);
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public FeedbackModel GetById(Guid tid, string dbn)
        {
            clsobj.SetDataBase(dbn);

            var result = _unitOfWork.FeedbackRepository.GetByID(tid);
            if (result != null)
            {
                return result;
            }
            return null;
        }


        public bool Update(FeedbackModel tentity, string dbn)
        {
            var success = false;
            if (tentity != null)
            {
                using (var scope = new TransactionScope())
                {
                    _unitOfWork.FeedbackRepository.Update(tentity);
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

        public bool Update(Guid tid, FeedbackModel tentity, string dbn)
        {
            throw new NotImplementedException();
        }

        public bool Delete(FeedbackModel tentity, string dbn)
        {
            var success = false;

            using (var scope = new TransactionScope())
            {
                _unitOfWork.FeedbackRepository.Delete(tentity);
                _unitOfWork.Save();
                scope.Complete();
                success = true;
            }
            return success;
        }
    }




}
