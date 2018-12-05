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
    public class AttendanceService : iAttendanceService
    {
        private readonly cUnitOfWork _unitOfWork;

        private commonclass clsobj;

        public AttendanceService(cUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this.clsobj = new commonclass(this._unitOfWork);
        }

        public IEnumerable<AttendanceModel> GetAll(string dbn)
        {
            clsobj.SetDataBase(dbn);
            //_unitOfWork.SetDatabase(dbn);
            var results = _unitOfWork.AttendanceRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public IEnumerable<AttendanceModel> GetAllByStudent(int stuid, string dbn)
        {
            clsobj.SetDataBase(dbn);
            //_unitOfWork.SetDatabase(dbn);
            var results = _unitOfWork.AttendanceRepository.GetMany(s => s.StudentModelID == stuid);
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public AttendanceModel GetById(int tid, string dbn)
        {
            throw new NotImplementedException();
        }
    }


}
