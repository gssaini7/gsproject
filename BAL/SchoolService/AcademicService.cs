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
    public class AcademicService : iAcademicService
    {
        private readonly cUnitOfWork _unitOfWork;

        private commonclass clsobj;

        public AcademicService(cUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this.clsobj = new commonclass(this._unitOfWork);
        }

        public IEnumerable<AcademicModel> GetAll(string dbn)
        {
            clsobj.SetDataBase(dbn);
            //_unitOfWork.SetDatabase(dbn);
            var results = _unitOfWork.AcademicRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public IEnumerable<AcademicModel> GetAllByStudent(int stuid, string dbn)
        {
            clsobj.SetDataBase(dbn);
            //_unitOfWork.SetDatabase(dbn);
            var includes = new string[] { "ForTerm", "ForSubject" };
            var results = _unitOfWork.AcademicRepository.GetWithInclude(s => s.StudentModelID == stuid, includes);

            //var results = _unitOfWork.AcademicRepository.GetMany(s => s.StudentModelID == stuid);
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public AcademicModel GetById(int tid, string dbn)
        {
            throw new NotImplementedException();
        }
    }


}
