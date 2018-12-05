  
  


using System; 
namespace R.BusinessEntities
{ 
    public class NotificationModel : EntityCommonModel
    {
        #region properties

         public Guid Notificationid { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string imagepath { get; set; }
        #endregion
    }
}

// Generated helper templates
// Generated items
// gsproject\crudgenerator\t4Templates\Notification\Web_APP_Module_Route.cs
// gsproject\crudgenerator\t4Templates\Notification\Web_APP_HTML.cs
// gsproject\crudgenerator\t4Templates\Notification\Web_APP_Components.cs
// gsproject\crudgenerator\t4Templates\Notification\Web_APP_Model.cs
// gsproject\crudgenerator\t4Templates\Notification\Web_APIController.cs
// gsproject\crudgenerator\t4Templates\Notification\BAL_Service.cs
// gsproject\crudgenerator\t4Templates\Notification\BAL_interface.cs
// gsproject\crudgenerator\t4Templates\Notification\DAL_ussdb.cs
// gsproject\crudgenerator\t4Templates\Notification\DAL_Unit of work.cs



