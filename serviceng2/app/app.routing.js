"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
//import { ManagerComponent } from './dashboard/manager/manager.component';
//import { HomeComponent } from './web/home/home.component';
//import { HomeComponent } from './dashboard/home/home.component'; 
//import { DashboardModule } from './dashboard/dashboard.module';
var appRoutes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' }
    //, { path: 'home', component: HomeComponent }
    ,
    { path: 'home', loadChildren: 'app/dashboard/dashboard.module#DashboardModule' }
    //, { path: 'home', loadChildren: 'app/web/web.module#WebModule'}
    ////, { path: 'home', component: HomeComponent }
    //, { path: 'manager', component: ManagerComponent }
    //, { path: 'account', loadChildren: 'app/account/account.module#AccountModule' }
];
exports.routing = router_1.RouterModule.forRoot(appRoutes);
//# sourceMappingURL=app.routing.js.map