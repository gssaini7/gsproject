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
    public class DescriptionModuleServiceG : iDescriptionModuleService
    {
        private readonly cUnitOfWork _unitOfWork;

        public DescriptionModuleServiceG(cUnitOfWork unitOfWork) 
        {
            this._unitOfWork = unitOfWork;
        }

        public DescriptionModuleModel GetById(long tid)
        {
            var result = _unitOfWork.DescriptionRepository.GetByID(tid);
            if (result != null)
            {
                var resultModel = GsMapperConfig.DescriptionModuleMapper(result);
                return resultModel;
            }
            return null;
        }

        public IEnumerable<DescriptionModuleModel> GetAll()
        {
            var results = _unitOfWork.DescriptionRepository.GetAll().ToList();
            if (results.Any())
            {
                var resultsmodel = GsMapperConfig.DescriptionModuleMapperList(results);
                return resultsmodel;
            }
            return null;
        }


        public long Create(DescriptionModuleModel tentity)
        {
            using (var scope = new TransactionScope())
            {
                var NewRecord = new ta_ussbk_Description
                {
                    descanydescription = tentity.descanydescription,
                    desccategory = tentity.desccategory,
                    desctitile = tentity.desctitile,
                    descvideolink = tentity.descvideolink,
                    websitemodule = tentity.websitemodule,
                };
                _unitOfWork.DescriptionRepository.Insert(NewRecord);
                _unitOfWork.Save();
                scope.Complete();
                return NewRecord.descriptionid;
            }
        }

        public bool Update(long tid, DescriptionModuleModel tentity)
        {
            var success = false;
            if (tentity != null && tid!=0)
            {
                using (var scope = new TransactionScope())
                {
                    var oldrecord = _unitOfWork.DescriptionRepository.GetByID(tid);
                    if (oldrecord != null)
                    {
                        oldrecord.descanydescription = tentity.descanydescription;
                        oldrecord.desccategory = tentity.desccategory;
                        oldrecord.desctitile = tentity.desctitile;
                        oldrecord.descvideolink = tentity.descvideolink;
                        oldrecord.websitemodule = tentity.websitemodule;

                        _unitOfWork.DescriptionRepository.Update(oldrecord);
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
                    var result = _unitOfWork.DescriptionRepository.GetByID(tid);
                    if (result != null)
                    {
                        _unitOfWork.DescriptionRepository.Delete(result);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }





        public IEnumerable<DescriptionModuleModel> GetAllByOther(long otherid)
        {
            throw new NotImplementedException();
        }
    }
}
