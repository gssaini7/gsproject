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
    public class AlbumService : iAlbumService
    {
        private readonly cUnitOfWork _unitOfWork;

        private commonclass clsobj;

        public AlbumService(cUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this.clsobj = new commonclass(this._unitOfWork);
        }

        public Guid Create(AlbumModel tentity, string dbn)
        {
            using (var scope = new TransactionScope())
            {
                clsobj.SetDataBase(dbn);
                //_unitOfWork.SetDatabase(dbn);

                _unitOfWork.AlbumRepository.Insert(tentity);
                _unitOfWork.Save();
                scope.Complete();
                return tentity.AlbumModelid;
            }
        }

        public bool Delete(AlbumModel tentity, string dbn)
        {
            var success = false;

            using (var scope = new TransactionScope())
            {
                _unitOfWork.AlbumRepository.Delete(tentity);
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

        public IEnumerable<AlbumModel> GetAll(string dbn)
        {
            clsobj.SetDataBase(dbn);
            //_unitOfWork.SetDatabase(dbn);

            var results = _unitOfWork.AlbumRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public AlbumModel GetById(Guid tid, string dbn)
        {
            clsobj.SetDataBase(dbn);
            //_unitOfWork.SetDatabase(dbn);

            var result = _unitOfWork.AlbumRepository.GetByID(tid);
            if (result != null)
            {
                return result;
            }
            return null;
        }


        public bool Update(AlbumModel tentity, string dbn)
        {
            var success = false;
            if (tentity != null)
            {
                using (var scope = new TransactionScope())
                {
                    //_unitOfWork.SetDatabase(dbn);

                    _unitOfWork.AlbumRepository.Update(tentity);
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

        public bool Update(Guid tid, AlbumModel tentity, string dbn)
        {
            throw new NotImplementedException();
        }

        //private void SetDataBase(string dbid)
        //{
        //    if (dbid != null)
        //    {
        //        var maindbresult = _unitOfWork.MainDatabasesRepository.GetByID(dbid);
        //        _unitOfWork.SetDatabase(maindbresult);
        //    }
        //}

    }


}
