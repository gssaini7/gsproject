
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace R.BusinessEntities
{
    // Models used as parameters to Admins by GS.

    public class BlogTypeModel : EntityCommonModel
    {
        #region properties

        public Guid BlogTypeModelid { get; set; }
        public string BlogTypeName { get; set; }
        public string BlogTypeDisplayName { get; set; }
        public string BlogTypeProperty { get; set; }


        #endregion
    }

    public class BlogModel : EntityCommonModel
    {
        #region properties

        public Guid BlogModelid { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string imagepath { get; set; }
        public string filetype { get; set; }

        //public string BlogSubType { get; set; }

        public int? SubjectModelid { get; set; }
        [ForeignKey("SubjectModelid")]
        public SubjectModel ForSubject { get; set; }


        public Guid BlogTypeModelid { get; set; }
        [ForeignKey("BlogTypeModelid")]
        public BlogTypeModel BlogType { get; set; }

        #endregion
    }

    public class NotificationModel : EntityCommonModel
    {
        #region properties

        public Guid NotificationModelid { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string imagepath { get; set; }
        public string BlogTypeName { get; set; }

      

        #endregion
    }

    public class NotificationScheduleModel : EntityCommonModel
    {
        #region properties

        public Guid NotificationScheduleModelid { get; set; }
        public DateTime notificationsentdate { get; set; }
        public string classname { get; set; }

        public Guid BlogModelid { get; set; }
        [ForeignKey("BlogModelid")]
        public BlogModel BlogDetail { get; set; }

        #endregion
    }

    public class TeacherBlogModel 
    {
        public Guid BlogModelid { get; set; }
        public Guid TeacherId { get; set; }
    }
}
