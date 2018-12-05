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
    public class TitleMasterServiceG : iTitleMasterService
    {
        private readonly cUnitOfWork _unitOfWork;

        public TitleMasterServiceG(cUnitOfWork unitOfWork) 
        {
            this._unitOfWork = unitOfWork;
        }

        public TitleMasterModel GetById(long tid)
        {
            var result = _unitOfWork.TitleMasterRepository.GetByID(tid);
            if (result != null)
            {
                var resultModel = GsMapperConfig.TitleMasterMapper(result);
                return resultModel;
            }
            return null;
        }

        public IEnumerable<TitleMasterModel> GetAll()
        {
            var results = _unitOfWork.TitleMasterRepository.GetAll().ToList();
            if (results.Any())
            {
                var resultsmodel = GsMapperConfig.TitleMasterMapperList(results);
                return resultsmodel;
            }
            return null;
        }


        public long Create(TitleMasterModel tentity)
        {
            using (var scope = new TransactionScope())
            {
                var NewRecord = new ta_ussbk_TitleMaster
                {
                    titlename = tentity.titlename,
                };
                _unitOfWork.TitleMasterRepository.Insert(NewRecord);
                _unitOfWork.Save();
                scope.Complete();
                return NewRecord.titleid;
            }
        }

        public bool Update(long tid, TitleMasterModel tentity)
        {
            var success = false;
            if (tentity != null && tid!=0)
            {
                using (var scope = new TransactionScope())
                {
                    var oldrecord = _unitOfWork.TitleMasterRepository.GetByID(tid);
                    if (oldrecord != null)
                    {
                        oldrecord.titlename = tentity.titlename;

                        _unitOfWork.TitleMasterRepository.Update(oldrecord);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool Delete(long tid)
        {
            var success = false;
            if (tid > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var result = _unitOfWork.TitleMasterRepository.GetByID(tid);
                    if (result != null)
                    {
                        _unitOfWork.TitleMasterRepository.Delete(result);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }





        public IEnumerable<TitleMasterModel> GetAllByOther(long otherid)
        {
            throw new NotImplementedException();
        }
    }
}
