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
    public class CouponService : iCouponService
    {
        private readonly cUnitOfWork _unitOfWork; public CouponService(cUnitOfWork unitOfWork) { this._unitOfWork = unitOfWork; }
        public CouponEntity GetById(long tid)
        {
            var result = _unitOfWork.CouponRepository.GetByID(tid);
            if (result != null)
            {
                var resultModel = GsMapperConfig.CouponMapper(result);
                return resultModel;
            } 
            return null;
        }
        public IEnumerable<CouponEntity> GetAll()
        {
            var results = _unitOfWork.CouponRepository.GetAll().ToList();
            if (results.Any())
            {
                var resultsmodel = GsMapperConfig.CouponMapperList(results);
                return resultsmodel;
            } return null;
        }
        public long Create(CouponEntity tentity)
        {
            using (var scope = new TransactionScope())
            {
                var NewRecord = new TA_ussbk_Coupons
                {
                    couponcode = tentity.couponcode,
                    cmsgforuser = tentity.cmsgforuser,
                    cAdminRemarks = tentity.cAdminRemarks,
                    cblocked = tentity.cblocked,
                    camount=tentity.camount,
                }; _unitOfWork.CouponRepository.Insert(NewRecord); _unitOfWork.Save(); scope.Complete();
                return NewRecord.couponid;
            }
        }
        public bool Update(long tid, CouponEntity tentity)
        {
            var success = false;
            if (tentity != null && tid != 0)
            {
                using (var scope = new TransactionScope())
                {
                    var oldrecord = _unitOfWork.CouponRepository.GetByID(tid);
                    if (oldrecord != null)
                    {
                        oldrecord.couponcode = tentity.couponcode; 
                        oldrecord.cmsgforuser = tentity.cmsgforuser; 
                        oldrecord.cAdminRemarks = tentity.cAdminRemarks; 
                        oldrecord.cblocked = tentity.cblocked;
                        oldrecord.camount = tentity.camount;
                        _unitOfWork.CouponRepository.Update(oldrecord); 
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
            var success = false; if (tid > 0)
            {
                using (var scope = new TransactionScope()) { var result = _unitOfWork.CouponRepository.GetByID(tid); if (result != null) { _unitOfWork.CouponRepository.Delete(result); _unitOfWork.Save(); scope.Complete(); success = true; } }
            }
            return success;
        }
        public IEnumerable<CouponEntity> GetAllByOther(long otherid) { throw new NotImplementedException(); }

        public CouponEntity GetByCouponCode(string couponcode)
        {
            var result = _unitOfWork.CouponRepository.Get(c => (c.couponcode == couponcode && c.cblocked==false));
            if (result != null)
            {
                var resultModel = GsMapperConfig.CouponMapper(result);
                return resultModel;
            }
            return null;
        }
    }
} 
