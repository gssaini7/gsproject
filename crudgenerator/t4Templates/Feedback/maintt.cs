  
  


using System; 
namespace R.BusinessEntities
{ 
    public class FeedbackModel : EntityCommonModel
    {
        #region properties

         public Guid FeedbackModelid { get; set; }
        public string FeedbackType { get; set; }
        public int StudentModelID { get; set; }
        public DateTime FeedbackDate { get; set; }
        public string FeedbackMessage { get; set; }
        #endregion
    }
}

// Generated helper templates
// Generated items
// gsproject\crudgenerator\t4Templates\Feedback\Web_APP_Module_Route.cs
// gsproject\crudgenerator\t4Templates\Feedback\Web_APP_HTML.cs
// gsproject\crudgenerator\t4Templates\Feedback\Web_APP_Components.cs
// gsproject\crudgenerator\t4Templates\Feedback\Web_APP_Model.cs
// gsproject\crudgenerator\t4Templates\Feedback\Web_APIController.cs
// gsproject\crudgenerator\t4Templates\Feedback\BAL_Service.cs
// gsproject\crudgenerator\t4Templates\Feedback\BAL_interface.cs
// gsproject\crudgenerator\t4Templates\Feedback\DAL_ussdb.cs
// gsproject\crudgenerator\t4Templates\Feedback\DAL_Unit of work.cs



