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
    public class NotificationScheduleService : iNotificationScheduleService
    {
        private readonly cUnitOfWork _unitOfWork;

        private commonclass clsobj;

        public NotificationScheduleService(cUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this.clsobj = new commonclass(this._unitOfWork);
        }

        public Guid Create(NotificationScheduleModel tentity, string dbn)
        {
            using (var scope = new TransactionScope())
            {
                clsobj.SetDataBase(dbn);
                //_unitOfWork.SetDatabase(dbn);

                _unitOfWork.NotificationScheduleRepository.Insert(tentity);
                _unitOfWork.Save();
                scope.Complete();
                return tentity.NotificationScheduleModelid;
            }
        }

        public bool Delete(NotificationScheduleModel tentity, string dbn)
        {
            var success = false;

            using (var scope = new TransactionScope())
            {
                _unitOfWork.NotificationScheduleRepository.Delete(tentity);
                _unitOfWork.Save();
                scope.Complete();
                success = true;
            }
            return success;
        }

        public bool Delete(Guid tid, string dbn)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NotificationScheduleModel> GetAll(string dbn)
        {
            clsobj.SetDataBase(dbn);
            //_unitOfWork.SetDatabase(dbn);

            var results = _unitOfWork.NotificationScheduleRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public NotificationScheduleModel GetById(Guid tid, string dbn)
        {
            clsobj.SetDataBase(dbn);
            //_unitOfWork.SetDatabase(dbn);

            var result = _unitOfWork.NotificationScheduleRepository.GetByID(tid);
            if (result != null)
            {
                return result;
            }
            return null;
        }


        public bool Update(NotificationScheduleModel tentity, string dbn)
        {
            var success = false;
            if (tentity != null)
            {
                using (var scope = new TransactionScope())
                {
                    clsobj.SetDataBase(dbn);
                    //_unitOfWork.SetDatabase(dbn);

                    _unitOfWork.NotificationScheduleRepository.Update(tentity);
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

        public bool Update(Guid tid, NotificationScheduleModel tentity, string dbn)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NotificationScheduleModel> GetAll(Guid blogid, string dbn)
        {
            clsobj.SetDataBase(dbn);
            var results = _unitOfWork.NotificationScheduleRepository.GetMany(b=>b.BlogModelid==blogid);
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public IEnumerable<NotificationScheduleModel> GetForStudentByClassSection(ClassSectionModel classsection, string type, string dbn)
        {
            clsobj.SetDataBase(dbn);

            string strtosearch = "{\"ClassModelid\":" + classsection.ClassModelid + ",\"SectionModelid\":" + classsection.SectionModelid+",";
            string[] includes = new string[] { "BlogDetail", "BlogDetail.BlogType", "BlogDetail.ForSubject" };
            var results = _unitOfWork.NotificationScheduleRepository.GetWithInclude(b => (b.classname.Contains(strtosearch) && b.BlogDetail.BlogType.BlogTypeName==type), includes);
            if (results.Any())
            {
                return results;
            }
            return null;
        }
    }



}
