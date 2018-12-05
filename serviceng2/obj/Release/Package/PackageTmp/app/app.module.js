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
var platform_browser_1 = require("@angular/platform-browser");
var animations_1 = require("@angular/platform-browser/animations");
var forms_1 = require("@angular/forms");
var app_component_1 = require("./app.component");
//import { Ng2Bs3ModalModule } from 'ng2-bs3-modal/ng2-bs3-modal';
var http_1 = require("@angular/http");
var app_routing_1 = require("./app.routing");
//import { ManagerComponent } from './dashboard/manager/manager.component';
//import { HomeComponent } from './web/home/home.component';
//import { ManagerService } from './dashboard/manager/manager'
//import { AccountModule } from './account/account.module';
var dashboard_module_1 = require("./dashboard/dashboard.module");
//import { WebModule } from './web/web.module';
//import { HeaderComponent } from './header/header.component';
//import { UserService } from './Services/user.service';
//import { RootComponent } from './web/root/root.component';
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_1.NgModule({
            imports: [platform_browser_1.BrowserModule, animations_1.BrowserAnimationsModule, forms_1.ReactiveFormsModule, http_1.HttpModule, app_routing_1.routing, dashboard_module_1.DashboardModule,
            ],
            declarations: [app_component_1.AppComponent,
            ],
            providers: [{ provide: common_1.LocationStrategy, useClass: common_1.HashLocationStrategy }, { provide: common_1.APP_BASE_HREF, useValue: '/' },
            ],
            bootstrap: [app_component_1.AppComponent]
        })
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map