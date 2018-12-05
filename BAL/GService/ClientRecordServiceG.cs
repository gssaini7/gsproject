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
    public class ClientRecordServiceG : iClientRecordService
    {
        private readonly cUnitOfWork _unitOfWork;
        const string salt = "MEDHSK@634834";

        public ClientRecordServiceG(cUnitOfWork unitOfWork) 
        {
            this._unitOfWork = unitOfWork;
        }

        public ClientRecordModel GetById(long tid)
        {
            var result = _unitOfWork.ClientRecordRepository.GetByID(tid);
            if (result != null)
            {
                var resultModel = GsMapperConfig.ClientRecordMapper(result);
                return resultModel;
            }
            return null;
        }

        public IEnumerable<ClientRecordModel> GetAll()
        {
            var results = _unitOfWork.ClientRecordRepository.GetAll().ToList();
            if (results.Any())
            {
                var resultsmodel = GsMapperConfig.ClientRecordMapperList(results);
                return resultsmodel;
            }
            return null;
        }


        public long Create(ClientRecordModel tentity)
        {
            using (var scope = new TransactionScope())
            {
                var NewRecord = new ta_ussbk_clientrecord
                {
                    businessname = tentity.businessname,
                    clientaddress = tentity.clientaddress,
                    clientemail = tentity.clientemail,
                    clientname = tentity.clientname,
                    contactmobile = tentity.contactmobile,
                    formodule = tentity.formodule,
                    productid = tentity.productid,
                    productname = tentity.productname,
                    userrole = tentity.userrole,
                    userblocked = tentity.userblocked,
                    expirydate=tentity.expirydate,
                    remarks=tentity.remarks,
                };
                _unitOfWork.ClientRecordRepository.Insert(NewRecord);
                _unitOfWork.Save();
                scope.Complete();
                return NewRecord.recordid;
            }
        }

        public bool Update(long tid, ClientRecordModel tentity)
        {
            var success = false;
            if (tentity != null && tid!=0)
            {
                using (var scope = new TransactionScope())
                {
                    var oldrecord = _unitOfWork.ClientRecordRepository.GetByID(tid);
                    if (oldrecord != null)
                    {
                        oldrecord.businessname = tentity.businessname;
                        oldrecord.clientaddress = tentity.clientaddress;
                        oldrecord.clientemail = tentity.clientemail;
                        oldrecord.clientname = tentity.clientname;
                        oldrecord.contactmobile = tentity.contactmobile;
                        oldrecord.formodule = tentity.formodule;
                        oldrecord.productid = tentity.productid;
                        oldrecord.productname = tentity.productname;
                        oldrecord.userrole = tentity.userrole;
                        oldrecord.userblocked = tentity.userblocked;
                        oldrecord.expirydate = tentity.expirydate;
                        oldrecord.upassword = tentity.upassword+salt;
                        oldrecord.remarks = tentity.remarks;

                        _unitOfWork.ClientRecordRepository.Update(oldrecord);
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
                    var result = _unitOfWork.ClientRecordRepository.GetByID(tid);
                    if (result != null)
                    {
                        _unitOfWork.ClientRecordRepository.Delete(result);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }





        public IEnumerable<ClientRecordModel> GetAllByOther(long otherid)
        {
            throw new NotImplementedException();
        }

        public ClientRecordModel GetByMobile(string mobileno)
        {
            var result = _unitOfWork.ClientRecordRepository.Get(m => m.contactmobile == mobileno);
            if (result != null)
            {
                var resultModel = GsMapperConfig.ClientRecordMapper(result);
                return resultModel;
            }
            return null;
        }


        public ClientRecordModel GetByMobilePassword(string mobileno, string usrpassword)
        {
            var result = _unitOfWork.ClientRecordRepository.Get(m => (m.contactmobile == mobileno && m.upassword==usrpassword+salt));
            if (result != null)
            {
                var resultModel = GsMapperConfig.ClientRecordMapper(result);
                return resultModel;
            }
            return null;
        }

       
    }
}
