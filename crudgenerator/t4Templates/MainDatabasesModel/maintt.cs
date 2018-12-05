  
  


using System; 
namespace R.BusinessEntities
{ 
    public class MainDatabasesModel : EntityCommonModel
    {
        #region properties

         public Guid MainDatabasesModelid { get; set; }
        public string IDataSource { get; set; }
        public string IInitialCatalog { get; set; }
        public string IUsername { get; set; }
        public string IPassword { get; set; }
        public string IName { get; set; }
        public string IUIDCode { get; set; }
        #endregion
    }
}

// Generated helper templates
// Generated items
// gsproject\crudgenerator\t4Templates\MainDatabasesModel\Web_APP_Module_Route.cs
// gsproject\crudgenerator\t4Templates\MainDatabasesModel\Web_APP_HTML.cs
// gsproject\crudgenerator\t4Templates\MainDatabasesModel\Web_APP_Components.cs
// gsproject\crudgenerator\t4Templates\MainDatabasesModel\Web_APP_Model.cs
// gsproject\crudgenerator\t4Templates\MainDatabasesModel\Web_APIController.cs
// gsproject\crudgenerator\t4Templates\MainDatabasesModel\BAL_Service.cs
// gsproject\crudgenerator\t4Templates\MainDatabasesModel\BAL_interface.cs
// gsproject\crudgenerator\t4Templates\MainDatabasesModel\DAL_ussdb.cs
// gsproject\crudgenerator\t4Templates\MainDatabasesModel\DAL_Unit of work.cs



