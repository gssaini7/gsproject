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
    public class ProjectDetailService : iProjectDetailService
    {
        private readonly cUnitOfWork _unitOfWork;
        public ProjectDetailService(cUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public ProjectDetailEntity GetById(long tid)
        {
            var result = _unitOfWork.ProjectDetailRepository.GetByID(tid);
            if (result != null)
            {
                var resultModel = GsMapperConfig.ProjectDetailMapper(result);
                return resultModel;
            }
            return null;
        }
        public IEnumerable<ProjectDetailEntity> GetAll()
        {
            var results = _unitOfWork.ProjectDetailRepository.GetAll().ToList();
            if (results.Any())
            {
                var resultsmodel = GsMapperConfig.ProjectDetailMapperList(results);
                return resultsmodel;
            }
            return null;
        }
        public long Create(ProjectDetailEntity tentity)
        {
            using (var scope = new TransactionScope())
            {
                var NewRecord = new ta_ussbk_ProjectDetail
                {
                    projectdetail = tentity.projectdetail,

                };
                _unitOfWork.ProjectDetailRepository.Insert(NewRecord);
                _unitOfWork.Save();
                scope.Complete();
                return NewRecord.amtdtid;
            }
        }
        public bool Update(long tid, ProjectDetailEntity tentity)
        {
            var success = false;
            if (tentity != null && tid != 0)
            {
                using (var scope = new TransactionScope())
                {
                    var oldrecord = _unitOfWork.ProjectDetailRepository.GetByID(tid);
                    if (oldrecord != null)
                    {
                        oldrecord.projectdetail = tentity.projectdetail;
                        _unitOfWork.ProjectDetailRepository.Update(oldrecord);
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
                    var result = _unitOfWork.ProjectDetailRepository.GetByID(tid);
                    if (result != null)
                    {
                        _unitOfWork.ProjectDetailRepository.Delete(result);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }
        public IEnumerable<ProjectDetailEntity> GetAllByOther(long otherid)
        {
            throw new NotImplementedException();
        }
    }
} 
