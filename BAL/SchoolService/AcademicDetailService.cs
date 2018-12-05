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
    public class AcademicDetailService : iAcademicDetailService
    {
        private readonly cUnitOfWork _unitOfWork;

        private commonclass clsobj;

        public AcademicDetailService(cUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this.clsobj = new commonclass(this._unitOfWork);
        }

        public AcademicDetailModel GetById(int tid, string dbn)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AcademicDetailModel> GetAll(string dbn)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AcademicDetailModel> GetAllByConditions(AcademicDetailConditionsModel adcm, string dbn)
        {
            clsobj.SetDataBase(dbn);
            var includes = new string[] { "TypeOfAssesment" };
            var results = _unitOfWork.AcademicSummaryRepository.GetWithInclude(s => (s.StudentModelID == adcm.StudentModelID && s.SubjectModelid==adcm.SubjectModelid &&
            s.TermModelid==adcm.TermModelid), includes);
            if (results.Any())
            {
                return results;
            }
            return null;
        }
    }


}
