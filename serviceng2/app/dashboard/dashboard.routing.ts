import { ModuleWithProviders } from '@angular/core';
import { RouterModule }        from '@angular/router';
import { AuthGuard } from '../auth.guard';

import { RootComponent } from './root/root.component';

import { HomeComponent }    from './home/home.component'; 
import { ProfileComponent } from './profile/profile.component';

import { MainDatabasesComponent } from './SuperAdmin/MainDatabases.component';

import { AdminManagerComponent } from './SupportAdmin/adminmanager.component';
import { RolesComponent } from './Roles/Roles.component';
//import { NotificationComponent } from './Notification/Notification.component';
import { NotificationScheduleComponent } from './NotificationSchedule/NotificationSchedule.component';
import { TCSSRelComponent } from './TeacherClassRel/TeacherClassRel.component';

import { ImageGalleryComponent } from './ImageGallery/ImageGallery.component';

import { AdminsComponent } from './Admins/Admins.component';
import { BlogTypeComponent } from './BlogType/BlogType.component';
import { BlogComponent } from './Blog/Blog.component';
import { PageComponent } from './Page/Page.component';
import { PageDetailComponent } from './PageDetail/PageDetail.component';
import { ListTypeComponent } from './CRUDType/ListType/ListType.component';
import { DetailTypeComponent } from './CRUDType/DetailType/DetailType.component';
import { FeedbackComponent } from './Feedback/Feedback.component';
import { SettingsComponent } from './Settings/Settings.component';


export const routing: ModuleWithProviders = RouterModule.forChild([
  {
      //path: 'dashboard',
        path: '',

      component: RootComponent,
      canActivate: [AuthGuard],

      children: [      
          { path: '', component: HomeComponent },
          { path: 'dbs', component: MainDatabasesComponent },

       { path: 'home', component: HomeComponent },
       { path: 'supportadmin', component: AdminManagerComponent },
       { path: 'profile', component: ProfileComponent },
       { path: 'roles', component: RolesComponent },
       { path: 'Imagegallery', component: ImageGalleryComponent },
       { path: 'admins', component: AdminsComponent },
       //{ path: 'notifications', component: NotificationComponent },
       { path: 'forward/:blogid', component: NotificationScheduleComponent },
       { path: 'blogtype', component: BlogTypeComponent },
       { path: 'blog/:type', component: BlogComponent },
       { path: 'blog', component: BlogComponent },

       { path: 'pages', component: PageComponent },
       { path: 'page/:id', component: PageDetailComponent },

       { path: 'list', component: ListTypeComponent },
       { path: 'detail/:type/:typeid/:id', component: DetailTypeComponent },
       { path: 'admindetail/:adminid', component: TCSSRelComponent },
       { path: 'feedback', component: FeedbackComponent },
       { path: 'settings/:type', component: SettingsComponent },

      ]       
    }  
]);

