using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using R.BusinessEntities;
using System.Threading.Tasks;

namespace R.BAL
{
    //public interface iCommon<T> where T : class
    //{
    //    T GetById(Guid tid);
    //    IEnumerable<T> GetAll();
    //    Guid Create(T tentity);
    //    bool Update(Guid tid, T tentity);
    //    bool Update(T tentity);
    //    bool Delete(Guid tid);
    //}
    public interface iCommon<T> where T : class
    {
        T GetById(Guid tid, string dbn);
        IEnumerable<T> GetAll(string dbn);
        Guid Create(T tentity, string dbn);
        bool Update(Guid tid, T tentity, string dbn);
        bool Update(T tentity, string dbn);
        bool Delete(Guid tid, string dbn);
        bool Delete(T tentity, string dbn);

    }

   

    public interface iCommonApp<T> where T : class
    {
        T GetById(int tid, string dbn);
        IEnumerable<T> GetAll(string dbn);
    }

    public interface iMainDatabasesService : iCommon<MainDatabasesModel>
    {
        MainDatabasesModel GetByName(string name, string dbn);
    }

    public interface iAlbumService : iCommon<AlbumModel>
    {
    }
    public interface iImageGalleryService : iCommon<ImageGalleryModel>
    {
        IEnumerable<ImageGalleryModel> GetAllByAlbumId(Guid albumid, string dbn);

    }

    public interface iBlogTypeService : iCommon<BlogTypeModel>
    {
        BlogTypeModel GetAllByName(string name, string dbn);

    }

    public interface iBlogService : iCommon<BlogModel>
    {
        IEnumerable<BlogModel> GetAllByType(string blogtypeid, string dbn);

        IEnumerable<BlogModel> GetAllByType_CreatedBy(string blogtypeid, Guid author, string dbn);
    }
    //public interface iPageService : iCommon<PageModel>
    //{
    //    PageModel GetByURL(string pageurl, string dbn);

    //}


    //public interface iNotificationService : iCommon<NotificationModel>
    //{
    //    IEnumerable<NotificationModel> GetAllByBlogType(string blogtypename, string dbn);

    //}
    public interface iNotificationScheduleService : iCommon<NotificationScheduleModel>
    {
        IEnumerable<NotificationScheduleModel> GetAll(Guid blogid,string dbn);
        IEnumerable<NotificationScheduleModel> GetForStudentByClassSection(ClassSectionModel classsection, string type, string dbn);
    }

    public interface iStudentService : iCommonApp<StudentModel>
    {
        IEnumerable<StudentModel> GetAllByMobile(string mobile,string dbn);
        IEnumerable<string> GetAllClass(string dbn);

        IEnumerable<StudentModel> GetAllByTeacher(List<ClassSectionModel> classsection, string dbn);

    }

    public interface iAttendanceService : iCommonApp<AttendanceModel>
    {
        IEnumerable<AttendanceModel> GetAllByStudent(int stuid, string dbn);

    }

    public interface iLedgerDetailService : iCommonApp<LedgerDetailModel>
    {
        IEnumerable<LedgerDetailModel> GetAllByStudent(int stuid, string dbn);

    }
    public interface iLedgerSumaryService : iCommonApp<LedgerSumaryModel>
    {
    }
    public interface iAcademicService : iCommonApp<AcademicModel>
    {
        IEnumerable<AcademicModel> GetAllByStudent(int stuid, string dbn);

    }

    public interface iAcademicDetailService : iCommonApp<AcademicDetailModel>
    {
        IEnumerable<AcademicDetailModel> GetAllByConditions(AcademicDetailConditionsModel adcm, string dbn);
    }

    public interface iSchoolService : iCommonApp<SchoolModel>
    {
    }

    public interface iClassService 
    {
        IEnumerable<ClassModel> GetAllClass(string dbn);
        IEnumerable<SectionModel> GetAllSection(string dbn);
        IEnumerable<SubjectModel> GetAllSubject(string dbn);

        
    }

    public interface iTCSSRelService : iCommon<TCSSRelModel>
    {
        IEnumerable<TCSSRelModel> GetAllByAdmin(Guid teacherid, string dbn);
        

    }

    public interface iFeedbackService : iCommon<FeedbackModel>
    {
    }

    public interface iSettingsService : iCommon<SettingsModel>
    {
        SettingsModel GetSettingByType(string type,string dbn);

    }

}
