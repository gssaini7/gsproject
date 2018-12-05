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
    public class ClassService : iClassService
    {
        private readonly cUnitOfWork _unitOfWork;

        private commonclass clsobj;

        public ClassService(cUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this.clsobj = new commonclass(this._unitOfWork);
        }

        public IEnumerable<ClassModel> GetAllClass(string dbn)
        {
            clsobj.SetDataBase(dbn);
            var results = _unitOfWork.ClassRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public IEnumerable<SectionModel> GetAllSection(string dbn)
        {
            clsobj.SetDataBase(dbn);
            var results = _unitOfWork.SectionRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public IEnumerable<SubjectModel> GetAllSubject(string dbn)
        {
            clsobj.SetDataBase(dbn);
            var results = _unitOfWork.SubjectRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }
    }


}
