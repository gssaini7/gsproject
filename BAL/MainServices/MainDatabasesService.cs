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
    public class MainDatabasesService : iMainDatabasesService
    {
        private readonly cUnitOfWork _unitOfWork;

        private commonclass clsobj;

        public MainDatabasesService(cUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this.clsobj = new commonclass(this._unitOfWork);
        }

        public Guid Create(MainDatabasesModel tentity, string dbn)
        {
            using (var scope = new TransactionScope())
            {
                //_unitOfWork.SetDatabase(dbn);

                _unitOfWork.MainDatabasesRepository.Insert(tentity);
                _unitOfWork.Save();
                scope.Complete();
                return tentity.MainDatabasesModelid;
            }
        }

        public bool Delete(MainDatabasesModel tentity, string dbn)
        {
            var success = false;

            using (var scope = new TransactionScope())
            {
                _unitOfWork.MainDatabasesRepository.Delete(tentity);
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

        public IEnumerable<MainDatabasesModel> GetAll(string dbn)
        {
            clsobj.SetDataBase(dbn);
            //_unitOfWork.SetDatabase(dbn);

            var results = _unitOfWork.MainDatabasesRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public MainDatabasesModel GetById(Guid tid, string dbn)
        {
            //clsobj.SetDataBase(dbn);
            //_unitOfWork.SetDatabase(dbn);

            var result = _unitOfWork.MainDatabasesRepository.GetByID(tid);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public MainDatabasesModel GetByName(string name, string dbn)
        {
            //clsobj.SetDataBase(dbn);
            //_unitOfWork.SetDatabase(dbn);
            var result = _unitOfWork.MainDatabasesRepository.Get(n=>n.IUIDCode==name);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public bool Update(MainDatabasesModel tentity, string dbn)
        {
            var success = false;
            if (tentity != null)
            {
                using (var scope = new TransactionScope())
                {
                    //_unitOfWork.SetDatabase(dbn);

                    _unitOfWork.MainDatabasesRepository.Update(tentity);
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

        public bool Update(Guid tid, MainDatabasesModel tentity, string dbn)
        {
            throw new NotImplementedException();
        }
    }


}
