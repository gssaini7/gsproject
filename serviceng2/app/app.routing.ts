import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

//import { ManagerComponent } from './dashboard/manager/manager.component';
//import { HomeComponent } from './web/home/home.component';
//import { HomeComponent } from './dashboard/home/home.component'; 
//import { DashboardModule } from './dashboard/dashboard.module';


const appRoutes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' }
    //, { path: 'home', component: HomeComponent }
    , { path: 'home', loadChildren: 'app/dashboard/dashboard.module#DashboardModule' }

    //, { path: 'home', loadChildren: 'app/web/web.module#WebModule'}

    ////, { path: 'home', component: HomeComponent }
    //, { path: 'manager', component: ManagerComponent }
    //, { path: 'account', loadChildren: 'app/account/account.module#AccountModule' }
];

export const routing: ModuleWithProviders =
    RouterModule.forRoot(appRoutes);