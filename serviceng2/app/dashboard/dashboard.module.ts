import { NgModule }           from '@angular/core';
import { CommonModule }       from '@angular/common';
import { FormsModule, ReactiveFormsModule }        from '@angular/forms';

import { routing }  from './dashboard.routing';
import { RootComponent } from './root/root.component';
import { HeaderComponent } from './header/header.component';

import { HomeComponent } from './home/home.component';
import { AuthcrudService } from '../Services/authcrud.service';

import { AuthGuard } from '../auth.guard';
import { MainDatabasesComponent } from './SuperAdmin/MainDatabases.component';

import { AdminsComponent } from './Admins/Admins.component';

import { AdminManagerComponent } from './SupportAdmin/adminmanager.component';
import { RolesComponent } from './Roles/Roles.component';
import { RoleAssignComponent } from './RoleAssign/RoleAssign.component';
import { TCSSRelComponent } from './TeacherClassRel/TeacherClassRel.component';


import { PopupDialogComponent } from './PopupDialog/PopupDialog.component';


import { AlbumComponent } from './Album/Album.component';
import { ImageGalleryComponent } from './ImageGallery/ImageGallery.component';

import { BlogTypeComponent } from './BlogType/BlogType.component';
import { BlogComponent } from './Blog/Blog.component';
import { SettingsComponent } from './Settings/Settings.component';



import { UserService } from '../Services/user.service';

import { NotificationScheduleComponent } from './NotificationSchedule/NotificationSchedule.component';

import { PageComponent } from './Page/Page.component';
import { PageDetailComponent } from './PageDetail/PageDetail.component';

import { ListTypeComponent } from './CRUDType/ListType/ListType.component';
import { DetailTypeComponent } from './CRUDType/DetailType/DetailType.component';
import { FeedbackComponent } from './Feedback/Feedback.component';


import { ProfileComponent } from './profile/profile.component';


import { GroupByPipe } from '../Shared/groupby.pipe';
import { SortByPipe } from '../Shared/sortby.pipe';

import { DataTableModule } from 'primeng/datatable';
import { PanelMenuModule } from 'primeng/panelmenu';
import { MenubarModule } from 'primeng/menubar';
import { ButtonModule } from 'primeng/button';
import { MenuModule } from 'primeng/menu';
import { DialogModule } from 'primeng/dialog';
import { OrderListModule } from 'primeng/orderlist';
import { CheckboxModule } from 'primeng/checkbox';
import { FileUploadModule } from 'primeng/fileupload';
import { DropdownModule } from 'primeng/dropdown';
import { PanelModule } from 'primeng/panel';
import { GrowlModule } from 'primeng/growl';
import { CalendarModule } from 'primeng/calendar';
import { ContextMenuModule } from 'primeng/contextmenu';
import { TreeTableModule } from 'primeng/treetable';
import { TreeModule } from 'primeng/tree';
import { DataGridModule } from 'primeng/datagrid';
import { OrganizationChartModule } from 'primeng/organizationchart';
import { MultiSelectModule } from 'primeng/multiselect';





//import {
//    DataTableModule, PanelMenuModule, MenubarModule, ButtonModule, MenuModule, DialogModule, OrderListModule,
//    CheckboxModule, FileUploadModule, DropdownModule, PanelModule, GrowlModule, CalendarModule, ContextMenuModule, TreeTableModule, DataGridModule,
//} from 'primeng/primeng';
import { CKEditorModule } from 'ng2-ckeditor';

@NgModule({
  imports:      [
    CommonModule, 
    FormsModule,
      routing,
      DataTableModule, 
      PanelMenuModule,
      MenubarModule,MenuModule,
      ButtonModule, TreeTableModule, TreeModule, OrderListModule, DataGridModule, OrganizationChartModule, MultiSelectModule,
      DialogModule, ReactiveFormsModule, CheckboxModule, FileUploadModule, DropdownModule, PanelModule, GrowlModule, CalendarModule, ContextMenuModule
      , CKEditorModule
      //NgxDatatableModule
    ],
  declarations: [RootComponent, HeaderComponent, HomeComponent, AdminManagerComponent, ProfileComponent,  RolesComponent,
      PopupDialogComponent, AlbumComponent, ImageGalleryComponent, RoleAssignComponent, AdminsComponent,  NotificationScheduleComponent,
      BlogTypeComponent, BlogComponent, PageComponent, PageDetailComponent, ListTypeComponent, DetailTypeComponent, MainDatabasesComponent,
      GroupByPipe, SortByPipe, TCSSRelComponent, FeedbackComponent, SettingsComponent
  ],
  exports:      [ ],
  providers: [AuthGuard,  AuthcrudService, UserService]
})
export class DashboardModule { }
