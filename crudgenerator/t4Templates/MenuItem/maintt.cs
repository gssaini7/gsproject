  
  


using System; 
namespace R.BusinessEntities
{ 
    public class MenuItemModel : EntityCommonModel
    {
        #region properties

         public Guid MenuItemModelid { get; set; }
        public string ItemTitle { get; set; }
        public string ItemText { get; set; }
        public string ItemLink { get; set; }
        public string Classli { get; set; }
        public string Classa { get; set; }
        public Guid Parentid { get; set; }
        public Guid MenuModel_MenuModelid { get; set; }
        #endregion
    }
}

// Generated helper templates
// Generated items
// E:\b\Project\USS\Usofts\usofts angular\crudgenerator\t4Templates\MenuItem\Web_APP_Module_Route.cs
// E:\b\Project\USS\Usofts\usofts angular\crudgenerator\t4Templates\MenuItem\Web_APP_HTML.cs
// E:\b\Project\USS\Usofts\usofts angular\crudgenerator\t4Templates\MenuItem\Web_APP_Components.cs
// E:\b\Project\USS\Usofts\usofts angular\crudgenerator\t4Templates\MenuItem\Web_APP_Model.cs
// E:\b\Project\USS\Usofts\usofts angular\crudgenerator\t4Templates\MenuItem\Web_APIController.cs
// E:\b\Project\USS\Usofts\usofts angular\crudgenerator\t4Templates\MenuItem\BAL_Service.cs
// E:\b\Project\USS\Usofts\usofts angular\crudgenerator\t4Templates\MenuItem\BAL_interface.cs
// E:\b\Project\USS\Usofts\usofts angular\crudgenerator\t4Templates\MenuItem\DAL_ussdb.cs
// E:\b\Project\USS\Usofts\usofts angular\crudgenerator\t4Templates\MenuItem\DAL_Unit of work.cs



