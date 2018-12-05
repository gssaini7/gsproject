"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var forms_1 = require("@angular/forms");
var dashboard_routing_1 = require("./dashboard.routing");
var root_component_1 = require("./root/root.component");
var header_component_1 = require("./header/header.component");
var home_component_1 = require("./home/home.component");
var authcrud_service_1 = require("../Services/authcrud.service");
var auth_guard_1 = require("../auth.guard");
var MainDatabases_component_1 = require("./SuperAdmin/MainDatabases.component");
var Admins_component_1 = require("./Admins/Admins.component");
var adminmanager_component_1 = require("./SupportAdmin/adminmanager.component");
var Roles_component_1 = require("./Roles/Roles.component");
var RoleAssign_component_1 = require("./RoleAssign/RoleAssign.component");
var TeacherClassRel_component_1 = require("./TeacherClassRel/TeacherClassRel.component");
var PopupDialog_component_1 = require("./PopupDialog/PopupDialog.component");
var Album_component_1 = require("./Album/Album.component");
var ImageGallery_component_1 = require("./ImageGallery/ImageGallery.component");
var BlogType_component_1 = require("./BlogType/BlogType.component");
var Blog_component_1 = require("./Blog/Blog.component");
var Settings_component_1 = require("./Settings/Settings.component");
var user_service_1 = require("../Services/user.service");
var NotificationSchedule_component_1 = require("./NotificationSchedule/NotificationSchedule.component");
var Page_component_1 = require("./Page/Page.component");
var PageDetail_component_1 = require("./PageDetail/PageDetail.component");
var ListType_component_1 = require("./CRUDType/ListType/ListType.component");
var DetailType_component_1 = require("./CRUDType/DetailType/DetailType.component");
var Feedback_component_1 = require("./Feedback/Feedback.component");
var profile_component_1 = require("./profile/profile.component");
var groupby_pipe_1 = require("../Shared/groupby.pipe");
var sortby_pipe_1 = require("../Shared/sortby.pipe");
var datatable_1 = require("primeng/datatable");
var panelmenu_1 = require("primeng/panelmenu");
var menubar_1 = require("primeng/menubar");
var button_1 = require("primeng/button");
var menu_1 = require("primeng/menu");
var dialog_1 = require("primeng/dialog");
var orderlist_1 = require("primeng/orderlist");
var checkbox_1 = require("primeng/checkbox");
var fileupload_1 = require("primeng/fileupload");
var dropdown_1 = require("primeng/dropdown");
var panel_1 = require("primeng/panel");
var growl_1 = require("primeng/growl");
var calendar_1 = require("primeng/calendar");
var contextmenu_1 = require("primeng/contextmenu");
var treetable_1 = require("primeng/treetable");
var tree_1 = require("primeng/tree");
var datagrid_1 = require("primeng/datagrid");
var organizationchart_1 = require("primeng/organizationchart");
var multiselect_1 = require("primeng/multiselect");
//import {
//    DataTableModule, PanelMenuModule, MenubarModule, ButtonModule, MenuModule, DialogModule, OrderListModule,
//    CheckboxModule, FileUploadModule, DropdownModule, PanelModule, GrowlModule, CalendarModule, ContextMenuModule, TreeTableModule, DataGridModule,
//} from 'primeng/primeng';
var ng2_ckeditor_1 = require("ng2-ckeditor");
var DashboardModule = /** @class */ (function () {
    function DashboardModule() {
    }
    DashboardModule = __decorate([
        core_1.NgModule({
            imports: [
                common_1.CommonModule,
                forms_1.FormsModule,
                dashboard_routing_1.routing,
                datatable_1.DataTableModule,
                panelmenu_1.PanelMenuModule,
                menubar_1.MenubarModule, menu_1.MenuModule,
                button_1.ButtonModule, treetable_1.TreeTableModule, tree_1.TreeModule, orderlist_1.OrderListModule, datagrid_1.DataGridModule, organizationchart_1.OrganizationChartModule, multiselect_1.MultiSelectModule,
                dialog_1.DialogModule, forms_1.ReactiveFormsModule, checkbox_1.CheckboxModule, fileupload_1.FileUploadModule, dropdown_1.DropdownModule, panel_1.PanelModule, growl_1.GrowlModule, calendar_1.CalendarModule, contextmenu_1.ContextMenuModule,
                ng2_ckeditor_1.CKEditorModule
                //NgxDatatableModule
            ],
            declarations: [root_component_1.RootComponent, header_component_1.HeaderComponent, home_component_1.HomeComponent, adminmanager_component_1.AdminManagerComponent, profile_component_1.ProfileComponent, Roles_component_1.RolesComponent,
                PopupDialog_component_1.PopupDialogComponent, Album_component_1.AlbumComponent, ImageGallery_component_1.ImageGalleryComponent, RoleAssign_component_1.RoleAssignComponent, Admins_component_1.AdminsComponent, NotificationSchedule_component_1.NotificationScheduleComponent,
                BlogType_component_1.BlogTypeComponent, Blog_component_1.BlogComponent, Page_component_1.PageComponent, PageDetail_component_1.PageDetailComponent, ListType_component_1.ListTypeComponent, DetailType_component_1.DetailTypeComponent, MainDatabases_component_1.MainDatabasesComponent,
                groupby_pipe_1.GroupByPipe, sortby_pipe_1.SortByPipe, TeacherClassRel_component_1.TCSSRelComponent, Feedback_component_1.FeedbackComponent, Settings_component_1.SettingsComponent
            ],
            exports: [],
            providers: [auth_guard_1.AuthGuard, authcrud_service_1.AuthcrudService, user_service_1.UserService]
        })
    ], DashboardModule);
    return DashboardModule;
}());
exports.DashboardModule = DashboardModule;
//# sourceMappingURL=dashboard.module.js.map