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
    public class CategoryMasterServiceG : iCategoryMasterService
    {
        private readonly cUnitOfWork _unitOfWork;

        public CategoryMasterServiceG(cUnitOfWork unitOfWork) 
        {
            this._unitOfWork = unitOfWork;
        }

        public CategoryMasterModel GetById(long tid)
        {
            var result = _unitOfWork.CategotyMasterRepository.GetByID(tid);
            if (result != null)
            {
                var resultModel = GsMapperConfig.CategoryMasterMapper(result);
                return resultModel;
            }
            return null;
        }

        public IEnumerable<CategoryMasterModel> GetAll()
        {
            var results = _unitOfWork.CategotyMasterRepository.GetAll().ToList();
            if (results.Any())
            {
                var resultsmodel = GsMapperConfig.CategoryMasterMapperList(results);
                return resultsmodel;
            }
            return null;
        }


        public long Create(CategoryMasterModel tentity)
        {
            using (var scope = new TransactionScope())
            {
                var NewRecord = new ta_ussbk_categoryMaster
                {
                    catname = tentity.catname,
                };
                _unitOfWork.CategotyMasterRepository.Insert(NewRecord);
                _unitOfWork.Save();
                scope.Complete();
                return NewRecord.catid;
            }
        }

        public bool Update(long tid, CategoryMasterModel tentity)
        {
            var success = false;
            if (tentity != null && tid!=0)
            {
                using (var scope = new TransactionScope())
                {
                    var oldrecord = _unitOfWork.CategotyMasterRepository.GetByID(tid);
                    if (oldrecord != null)
                    {
                        oldrecord.catname = tentity.catname;

                        _unitOfWork.CategotyMasterRepository.Update(oldrecord);
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
                    var result = _unitOfWork.CategotyMasterRepository.GetByID(tid);
                    if (result != null)
                    {
                        _unitOfWork.CategotyMasterRepository.Delete(result);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }





        public IEnumerable<CategoryMasterModel> GetAllByOther(long otherid)
        {
            throw new NotImplementedException();
        }
    }
}
