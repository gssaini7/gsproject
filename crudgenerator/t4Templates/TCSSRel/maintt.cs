  
  


using System; 
namespace R.BusinessEntities
{ 
    public class TCSSRelModel : EntityCommonModel
    {
        #region properties

         public Guid TCSSRelModelid { get; set; }
        public Guid Teacherid { get; set; }
        public string ClassModelid { get; set; }
        public string SectionModelid { get; set; }
        public string SubjectModelid { get; set; }
        #endregion
    }
}

// Generated helper templates
// Generated items
// gsproject\crudgenerator\t4Templates\TCSSRel\Web_APP_Module_Route.cs
// gsproject\crudgenerator\t4Templates\TCSSRel\Web_APP_HTML.cs
// gsproject\crudgenerator\t4Templates\TCSSRel\Web_APP_Components.cs
// gsproject\crudgenerator\t4Templates\TCSSRel\Web_APP_Model.cs
// gsproject\crudgenerator\t4Templates\TCSSRel\Web_APIController.cs
// gsproject\crudgenerator\t4Templates\TCSSRel\BAL_Service.cs
// gsproject\crudgenerator\t4Templates\TCSSRel\BAL_interface.cs
// gsproject\crudgenerator\t4Templates\TCSSRel\DAL_ussdb.cs
// gsproject\crudgenerator\t4Templates\TCSSRel\DAL_Unit of work.cs



