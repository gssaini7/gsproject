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
    public class BlogTypeService : iBlogTypeService
    {
        private readonly cUnitOfWork _unitOfWork;

        private commonclass clsobj;

        public BlogTypeService(cUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this.clsobj = new commonclass(this._unitOfWork);
        }

        public Guid Create(BlogTypeModel tentity, string dbn)
        {
            using (var scope = new TransactionScope())
            {
                clsobj.SetDataBase(dbn);
                //_unitOfWork.SetDatabase(dbn);

                _unitOfWork.BlogTypeRepository.Insert(tentity);
                _unitOfWork.Save();
                scope.Complete();
                return tentity.BlogTypeModelid;
            }
        }

       
        public bool Delete(BlogTypeModel tentity, string dbn)
        {
            var success = false;

            using (var scope = new TransactionScope())
            {
                _unitOfWork.BlogTypeRepository.Delete(tentity);
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

        

        public IEnumerable<BlogTypeModel> GetAll(string dbn)
        {
            clsobj.SetDataBase(dbn);

            //_unitOfWork.SetDatabase(dbn);

            var results = _unitOfWork.BlogTypeRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public BlogTypeModel GetAllByName(string name, string dbn)
        {
            clsobj.SetDataBase(dbn);

            //_unitOfWork.SetDatabase(dbn);
            var result = _unitOfWork.BlogTypeRepository.Get(b=>b.BlogTypeName==name);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public BlogTypeModel GetById(Guid tid, string dbn)
        {
            clsobj.SetDataBase(dbn);

            //_unitOfWork.SetDatabase(dbn);

            var result = _unitOfWork.BlogTypeRepository.GetByID(tid);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        
        public bool Update(BlogTypeModel tentity, string dbn)
        {
            var success = false;
            if (tentity != null)
            {
                using (var scope = new TransactionScope())
                {
                    _unitOfWork.BlogTypeRepository.Update(tentity);
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

        public bool Update(Guid tid, BlogTypeModel tentity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid tid, BlogTypeModel tentity, string dbn)
        {
            throw new NotImplementedException();
        }
    }




}
