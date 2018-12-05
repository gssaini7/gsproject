using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using R.Resolver;
using System.ComponentModel.Composition;

namespace R.BAL
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            
            registerComponent.RegisterType<iAlbumService, AlbumService>();
            registerComponent.RegisterType<iImageGalleryService, ImageGalleryService>();
            registerComponent.RegisterType<iBlogTypeService, BlogTypeService>();

            registerComponent.RegisterType<iBlogService, BlogService>();

            registerComponent.RegisterType<iSchoolService, SchoolService>();
            registerComponent.RegisterType<iAttendanceService, AttendanceService>();
            registerComponent.RegisterType<iStudentService, StudentService>();
            registerComponent.RegisterType<iLedgerDetailService, LedgerDetailService>();
            registerComponent.RegisterType<iLedgerSumaryService, LedgerSumaryService>();
            registerComponent.RegisterType<iAcademicService, AcademicService>();
            //registerComponent.RegisterType<iPageService, PageService>();

            registerComponent.RegisterType<iMainDatabasesService, MainDatabasesService>();
            registerComponent.RegisterType<iClassService, ClassService>();

            registerComponent.RegisterType<iTCSSRelService, TCSSRelService>();
            registerComponent.RegisterType<iNotificationScheduleService, NotificationScheduleService>();
            registerComponent.RegisterType<iFeedbackService, FeedbackService>();
            registerComponent.RegisterType<iSettingsService, SettingsService>();
            registerComponent.RegisterType<iAcademicDetailService, AcademicDetailService>();










        }
    }
}
