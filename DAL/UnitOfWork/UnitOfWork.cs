using DAL.GenericRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DAL;
using R.BusinessEntities;
using System.Threading.Tasks;

namespace R.DAL.UnitOfWork
{
    public class cUnitOfWork : IDisposable, iUnitOfWork
    {
        #region Private member variables...

        private usdbdatacontext _context = null;

      
        //private Genericrepository<Manager> _Ta_Manager;
        //private Genericrepository<ItemTypes> _Ta_ItemTypes;
        //private Genericrepository<Item> _Ta_Item;
        //private Genericrepository<ItemStock> _Ta_ItemStock;
        //private Genericrepository<OrderItem> _Ta_OrderItem;
        //private Genericrepository<Settings> _Ta_Settings;

        //private Genericrepository<ClientDetailModel> _Ta_ClientDetailModel;
        //private Genericrepository<PageModel> _Ta_PageModel;




        private Genericrepository<StudentModel> _Ta_StudentModel;
        private Genericrepository<AttendanceModel> _Ta_AttendanceModel;
        private Genericrepository<LedgerDetailModel> _Ta_LedgerDetailModel;
        private Genericrepository<LedgerSumaryModel> _Ta_LedgerSumaryModel;
        private Genericrepository<AcademicModel> _Ta_AcademicModel;
        private Genericrepository<SchoolModel> _Ta_SchoolModel;


        #endregion
        public cUnitOfWork()
        {
            _context = new usdbdatacontext();
        }
        //public cUnitOfWork()
        //{
        //    _context = new usdbdatacontext("ussdbng");
        //}

        //public cUnitOfWork()
        //{

        //    _context = new usdbdatacontext();
        //}

        #region Public Repository Creation properties...
        private Genericrepository<FeedbackModel> _Ta_FeedbackModel;
        public Genericrepository<FeedbackModel> FeedbackRepository
        {
            get
            {
                if (this._Ta_FeedbackModel == null) this._Ta_FeedbackModel = new Genericrepository<FeedbackModel>(_context);
                return _Ta_FeedbackModel;
            }
        }
        private Genericrepository<MainDatabasesModel> _Ta_MainDatabasesModel;
        public Genericrepository<MainDatabasesModel> MainDatabasesRepository
        {
            get
            {
                if (this._Ta_MainDatabasesModel == null) this._Ta_MainDatabasesModel = new Genericrepository<MainDatabasesModel>(_context);
                return _Ta_MainDatabasesModel;
            }
        }

      

    
        private Genericrepository<AlbumModel> _Ta_AlbumModel;
        public Genericrepository<AlbumModel> AlbumRepository
        {
            get
            {
                if (this._Ta_AlbumModel == null) this._Ta_AlbumModel = new Genericrepository<AlbumModel>(_context);
                return _Ta_AlbumModel;
            }
        }
        private Genericrepository<ImageGalleryModel> _Ta_ImageGalleryModel;
        public Genericrepository<ImageGalleryModel> ImageGalleryRepository
        {
            get
            {
                if (this._Ta_ImageGalleryModel == null) this._Ta_ImageGalleryModel = new Genericrepository<ImageGalleryModel>(_context);
                return _Ta_ImageGalleryModel;
            }
        }






        //private Genericrepository<NotificationModel> _Ta_NotificationModel;
        //public Genericrepository<NotificationModel> NotificationRepository
        //{
        //    get
        //    {
        //        if (this._Ta_NotificationModel == null) this._Ta_NotificationModel = new Genericrepository<NotificationModel>(_context);
        //        return _Ta_NotificationModel;
        //    }
        //}

        private Genericrepository<SettingsModel> _Ta_SettingsModel;
        public Genericrepository<SettingsModel> SettingsRepository
        {
            get
            {
                if (this._Ta_SettingsModel == null) this._Ta_SettingsModel = new Genericrepository<SettingsModel>(_context);
                return _Ta_SettingsModel;
            }
        }



        public Genericrepository<StudentModel> StudentRepository
        {
            get
            {
                if (this._Ta_StudentModel == null) this._Ta_StudentModel = new Genericrepository<StudentModel>(_context);
                return _Ta_StudentModel;
            }
        }
        public Genericrepository<AttendanceModel> AttendanceRepository
        {
            get
            {
                if (this._Ta_AttendanceModel == null) this._Ta_AttendanceModel = new Genericrepository<AttendanceModel>(_context);
                return _Ta_AttendanceModel;
            }
        }
        public Genericrepository<LedgerDetailModel> LedgerDetailRepository
        {
            get
            {
                if (this._Ta_LedgerDetailModel == null) this._Ta_LedgerDetailModel = new Genericrepository<LedgerDetailModel>(_context);
                return _Ta_LedgerDetailModel;
            }
        }
        public Genericrepository<LedgerSumaryModel> LedgerSumaryRepository
        {
            get
            {
                if (this._Ta_LedgerSumaryModel == null) this._Ta_LedgerSumaryModel = new Genericrepository<LedgerSumaryModel>(_context);
                return _Ta_LedgerSumaryModel;
            }
        }
        public Genericrepository<AcademicModel> AcademicRepository
        {
            get
            {
                if (this._Ta_AcademicModel == null) this._Ta_AcademicModel = new Genericrepository<AcademicModel>(_context);
                return _Ta_AcademicModel;
            }
        }
        public Genericrepository<SchoolModel> SchoolRepository
        {
            get
            {
                if (this._Ta_SchoolModel == null) this._Ta_SchoolModel = new Genericrepository<SchoolModel>(_context);
                return _Ta_SchoolModel;
            }
        }

       

        private Genericrepository<NotificationScheduleModel> _Ta_NotificationScheduleModel;
        public Genericrepository<NotificationScheduleModel> NotificationScheduleRepository
        {
            get
            {
                if (this._Ta_NotificationScheduleModel == null) this._Ta_NotificationScheduleModel = new Genericrepository<NotificationScheduleModel>(_context);
                return _Ta_NotificationScheduleModel;
            }
        }

        private Genericrepository<BlogTypeModel> _Ta_BlogTypeModel;
        public Genericrepository<BlogTypeModel> BlogTypeRepository
        {
            get
            {
                if (this._Ta_BlogTypeModel == null) this._Ta_BlogTypeModel = new Genericrepository<BlogTypeModel>(_context);
                return _Ta_BlogTypeModel;
            }
        }

        private Genericrepository<BlogModel> _Ta_BlogModel;
        public Genericrepository<BlogModel> BlogRepository
        {
            get
            {
                if (this._Ta_BlogModel == null) this._Ta_BlogModel = new Genericrepository<BlogModel>(_context);
                return _Ta_BlogModel;
            }
        }

        private Genericrepository<ClassModel> _Ta_ClassModel;
        public Genericrepository<ClassModel> ClassRepository
        {
            get
            {
                if (this._Ta_ClassModel == null) this._Ta_ClassModel = new Genericrepository<ClassModel>(_context);
                return _Ta_ClassModel;
            }
        }

        private Genericrepository<SubjectModel> _Ta_SubjectModel;
        public Genericrepository<SubjectModel> SubjectRepository
        {
            get
            {
                if (this._Ta_SubjectModel == null) this._Ta_SubjectModel = new Genericrepository<SubjectModel>(_context);
                return _Ta_SubjectModel;
            }
        }

        private Genericrepository<SectionModel> _Ta_SectionModel;
        public Genericrepository<SectionModel> SectionRepository
        {
            get
            {
                if (this._Ta_SectionModel == null) this._Ta_SectionModel = new Genericrepository<SectionModel>(_context);
                return _Ta_SectionModel;
            }
        }

        private Genericrepository<TCSSRelModel> _Ta_TCSSRelModel;
        public Genericrepository<TCSSRelModel> TCSSRelRepository
        {
            get
            {
                if (this._Ta_TCSSRelModel == null) this._Ta_TCSSRelModel = new Genericrepository<TCSSRelModel>(_context);
                return _Ta_TCSSRelModel;
            }
        }

        private Genericrepository<AcademicDetailModel> _Ta_AcademicDetailModel;
        public Genericrepository<AcademicDetailModel> AcademicSummaryRepository
        {
            get
            {
                if (this._Ta_AcademicDetailModel == null) this._Ta_AcademicDetailModel = new Genericrepository<AcademicDetailModel>(_context);
                return _Ta_AcademicDetailModel;
            }
        }

        private Genericrepository<TermModel> _Ta_TermModel;
        public Genericrepository<TermModel> TermRepository
        {
            get
            {
                if (this._Ta_TermModel == null) this._Ta_TermModel = new Genericrepository<TermModel>(_context);
                return _Ta_TermModel;
            }
        }

        private Genericrepository<AssesmentModel> _Ta_AssesmentModel;
        public Genericrepository<AssesmentModel> AssesmentRepository
        {
            get
            {
                if (this._Ta_AssesmentModel == null) this._Ta_AssesmentModel = new Genericrepository<AssesmentModel>(_context);
                return _Ta_AssesmentModel;
            }
        }
        private Genericrepository<HouseModel> _Ta_HouseModel;
        public Genericrepository<HouseModel> HouseRepository
        {
            get
            {
                if (this._Ta_HouseModel == null) this._Ta_HouseModel = new Genericrepository<HouseModel>(_context);
                return _Ta_HouseModel;
            }
        }
        private Genericrepository<VehicleModel> _Ta_VehicleModel;
        public Genericrepository<VehicleModel> VehicleRepository
        {
            get
            {
                if (this._Ta_VehicleModel == null) this._Ta_VehicleModel = new Genericrepository<VehicleModel>(_context);
                return _Ta_VehicleModel;
            }
        }
        #endregion

        #region Public member methods...
        /// <summary>
        /// Save method.
        /// </summary>
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                //System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }

        public async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                //System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }

        public void SetDatabase(MainDatabasesModel dbname=null) {
            try
            {
                if(dbname!=null)
                _context = new usdbdatacontext(dbname);
            }
            catch { }
        }

        #endregion

        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
