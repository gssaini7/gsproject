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
    public class BlogService : iBlogService
    {
        private readonly cUnitOfWork _unitOfWork;

        private commonclass clsobj;

        public BlogService(cUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this.clsobj = new commonclass(this._unitOfWork);
        }

        public Guid Create(BlogModel tentity, string dbn)
        {
            using (var scope = new TransactionScope())
            {
                clsobj.SetDataBase(dbn);
                //_unitOfWork.SetDatabase(dbn);

                _unitOfWork.BlogRepository.Insert(tentity);
                _unitOfWork.Save();
                scope.Complete();
                return tentity.BlogModelid;
            }
        }

        public bool Delete(BlogModel tentity, string dbn)
        {
            var success = false;

            using (var scope = new TransactionScope())
            {
                _unitOfWork.BlogRepository.Delete(tentity);
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

        public IEnumerable<BlogModel> GetAll(string dbn)
        {
            clsobj.SetDataBase(dbn);
            //_unitOfWork.SetDatabase(dbn);

            var results = _unitOfWork.BlogRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public BlogModel GetById(Guid tid, string dbn)
        {
            clsobj.SetDataBase(dbn);
            //_unitOfWork.SetDatabase(dbn);

            //var result = _unitOfWork.BlogRepository.GetByID(tid);

            var includes = new string[] { "BlogType" };
            var result = _unitOfWork.BlogRepository.GetWithInclude(a => a.BlogModelid == tid, includes).FirstOrDefault();

            if (result != null)
            {
                return result;
            }
            return null;
        }


        public bool Update(BlogModel tentity, string dbn)
        {
            var success = false;
            if (tentity != null)
            {
                using (var scope = new TransactionScope())
                {
                    //_unitOfWork.SetDatabase(dbn);

                    _unitOfWork.BlogRepository.Update(tentity);
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

        public bool Update(Guid tid, BlogModel tentity, string dbn)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BlogModel> GetAllByType(string blogtypeid, string dbn)
        {
            clsobj.SetDataBase(dbn);
            var results = _unitOfWork.BlogRepository.GetMany(bt=>bt.BlogTypeModelid==new Guid(blogtypeid));
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public IEnumerable<BlogModel> GetAllByType_CreatedBy(string blogtypeid, Guid author, string dbn)
        {
            clsobj.SetDataBase(dbn);
            var includes = new string[] { "ForSubject" };

            var results = _unitOfWork.BlogRepository.GetWithInclude(bt => (bt.BlogTypeModelid == new Guid(blogtypeid) && bt.createdBy==author), includes);
            if (results.Any())
            {
                return results;
            }
            return null;
        }
    }



}
