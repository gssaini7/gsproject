  
  


using System; 
namespace R.BusinessEntities
{ 
    public class NotificationScheduleModel : EntityCommonModel
    {
        #region properties

         public Guid NotificationScheduleModelid { get; set; }
        public DateTime notificationsentdate { get; set; }
        public string classname { get; set; }
        #endregion
    }
}

// Generated helper templates
// Generated items
// gsproject\crudgenerator\t4Templates\NotificationSchedule\Web_APP_Module_Route.cs
// gsproject\crudgenerator\t4Templates\NotificationSchedule\Web_APP_HTML.cs
// gsproject\crudgenerator\t4Templates\NotificationSchedule\Web_APP_Components.cs
// gsproject\crudgenerator\t4Templates\NotificationSchedule\Web_APP_Model.cs
// gsproject\crudgenerator\t4Templates\NotificationSchedule\Web_APIController.cs
// gsproject\crudgenerator\t4Templates\NotificationSchedule\BAL_Service.cs
// gsproject\crudgenerator\t4Templates\NotificationSchedule\BAL_interface.cs
// gsproject\crudgenerator\t4Templates\NotificationSchedule\DAL_ussdb.cs
// gsproject\crudgenerator\t4Templates\NotificationSchedule\DAL_Unit of work.cs



