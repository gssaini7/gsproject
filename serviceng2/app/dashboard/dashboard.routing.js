"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
var auth_guard_1 = require("../auth.guard");
var root_component_1 = require("./root/root.component");
var home_component_1 = require("./home/home.component");
var profile_component_1 = require("./profile/profile.component");
var MainDatabases_component_1 = require("./SuperAdmin/MainDatabases.component");
var adminmanager_component_1 = require("./SupportAdmin/adminmanager.component");
var Roles_component_1 = require("./Roles/Roles.component");
//import { NotificationComponent } from './Notification/Notification.component';
var NotificationSchedule_component_1 = require("./NotificationSchedule/NotificationSchedule.component");
var TeacherClassRel_component_1 = require("./TeacherClassRel/TeacherClassRel.component");
var ImageGallery_component_1 = require("./ImageGallery/ImageGallery.component");
var Admins_component_1 = require("./Admins/Admins.component");
var BlogType_component_1 = require("./BlogType/BlogType.component");
var Blog_component_1 = require("./Blog/Blog.component");
var Page_component_1 = require("./Page/Page.component");
var PageDetail_component_1 = require("./PageDetail/PageDetail.component");
var ListType_component_1 = require("./CRUDType/ListType/ListType.component");
var DetailType_component_1 = require("./CRUDType/DetailType/DetailType.component");
var Feedback_component_1 = require("./Feedback/Feedback.component");
var Settings_component_1 = require("./Settings/Settings.component");
exports.routing = router_1.RouterModule.forChild([
    {
        //path: 'dashboard',
        path: '',
        component: root_component_1.RootComponent,
        canActivate: [auth_guard_1.AuthGuard],
        children: [
            { path: '', component: home_component_1.HomeComponent },
            { path: 'dbs', component: MainDatabases_component_1.MainDatabasesComponent },
            { path: 'home', component: home_component_1.HomeComponent },
            { path: 'supportadmin', component: adminmanager_component_1.AdminManagerComponent },
            { path: 'profile', component: profile_component_1.ProfileComponent },
            { path: 'roles', component: Roles_component_1.RolesComponent },
            { path: 'Imagegallery', component: ImageGallery_component_1.ImageGalleryComponent },
            { path: 'admins', component: Admins_component_1.AdminsComponent },
            //{ path: 'notifications', component: NotificationComponent },
            { path: 'forward/:blogid', component: NotificationSchedule_component_1.NotificationScheduleComponent },
            { path: 'blogtype', component: BlogType_component_1.BlogTypeComponent },
            { path: 'blog/:type', component: Blog_component_1.BlogComponent },
            { path: 'blog', component: Blog_component_1.BlogComponent },
            { path: 'pages', component: Page_component_1.PageComponent },
            { path: 'page/:id', component: PageDetail_component_1.PageDetailComponent },
            { path: 'list', component: ListType_component_1.ListTypeComponent },
            { path: 'detail/:type/:typeid/:id', component: DetailType_component_1.DetailTypeComponent },
            { path: 'admindetail/:adminid', component: TeacherClassRel_component_1.TCSSRelComponent },
            { path: 'feedback', component: Feedback_component_1.FeedbackComponent },
            { path: 'settings/:type', component: Settings_component_1.SettingsComponent },
        ]
    }
]);
//# sourceMappingURL=dashboard.routing.js.map