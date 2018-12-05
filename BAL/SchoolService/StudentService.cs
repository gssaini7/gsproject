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
    public class StudentService : iStudentService
    {
        private readonly cUnitOfWork _unitOfWork;

        private commonclass clsobj;

        public StudentService(cUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this.clsobj = new commonclass(this._unitOfWork);
        }

        public IEnumerable<StudentModel> GetAll(string dbn)
        {
            clsobj.SetDataBase(dbn);
            //_unitOfWork.SetDatabase(dbn);
            var includes = new string[] { "OfClass", "OfSection", "OfBranch", "OfHouse", "VehicleDetail" };

            var results = _unitOfWork.StudentRepository.GetWithInclude2(includes);
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public IEnumerable<StudentModel> GetAllByMobile(string mobile, string dbn)
        {
            clsobj.SetDataBase(dbn);
            //_unitOfWork.SetDatabase(dbn);
            var includes = new string[] { "OfClass", "OfSection", "OfBranch", "OfHouse", "VehicleDetail" };

            var results = _unitOfWork.StudentRepository.GetWithInclude(m=>m.numMobileNoForSms==mobile, includes);
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public IEnumerable<string> GetAllClass(string dbn)
        {
            clsobj.SetDataBase(dbn);

            var includes = new string[] { "OfClass" };
            var results = _unitOfWork.StudentRepository.GetWithInclude2(includes).Select(c=>c.OfClass.ClassName).Distinct();
            if (results.Any())
            {
                return results;
            }
            return null;

            //var results = _unitOfWork.StudentRepository.GetAll().Select(c => c.).Distinct();
            //if (results.Any())
            //{
            //    return results;
            //}
            //return null;
            //throw new NotImplementedException();
            /////////////////////////////////////////////////////////////////////////////////////////
        }

        public StudentModel GetById(int tid, string dbn)
        {
            clsobj.SetDataBase(dbn);
            var includes = new string[] { "OfClass", "OfSection", "OfBranch", "OfHouse", "VehicleDetail" };
            var result = _unitOfWork.StudentRepository.GetWithInclude(t=>t.StudentModelID==tid, includes).FirstOrDefault();

            //var result = _unitOfWork.StudentRepository.GetByID(tid);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public IEnumerable<StudentModel> GetAllByTeacher(List<ClassSectionModel> classsection, string dbn)
        {

            clsobj.SetDataBase(dbn);
            var includes = new string[] { "OfClass", "OfSection", "OfBranch", "OfHouse", "VehicleDetail" };
            //var resultsd = _unitOfWork.StudentRepository.GetAll();
            //var results = _unitOfWork.StudentRepository.GetWithInclude(c => classsection.Any(a => (a.ClassModelid == c.ClassModelid)), includes);

            //var results = _unitOfWork.StudentRepository.GetWithInclude(c => classsection., includes);
            //var resultsn = _unitOfWork.StudentRepository.GetManyQueryable(c => classsection.Any(a => (a.ClassModelid == c.ClassModelid && a.SectionModelid == c.SectionModelid)));
            //var results = _unitOfWork.StudentRepository.GetWithInclude2(includes);
            var results = _unitOfWork.StudentRepository.GetWithInclude3(c => classsection.Any(a => (a.ClassModelid == c.ClassModelid && a.SectionModelid == c.SectionModelid)), includes);

            //var b = results.Where(x => hashSet.Contains(x.ClassModelid));

            //var r = results.Where(c => classsection.Any(a => (a.ClassModelid == c.ClassModelid)));
            //var results1 = _unitOfWork.StudentRepository.GetWithInclude(c => c.OfClass.s), includes);
            //results = results.Where(c => c.ClassModelid==classsection.);
            //var n=results.Where(s=> classsection.Contains(s.ClassModelid))
            if (results.Any())
            {
                return results;
            }
            return null;
            //throw new NotImplementedException();
            /////////////////////////////////////////////////////////////////////////////////////////
        }
    }


}
