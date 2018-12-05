import { NgModule } from '@angular/core';
import { APP_BASE_HREF, HashLocationStrategy, LocationStrategy } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
//import { Ng2Bs3ModalModule } from 'ng2-bs3-modal/ng2-bs3-modal';
import { HttpModule } from '@angular/http';
import { routing } from './app.routing';
//import { ManagerComponent } from './dashboard/manager/manager.component';
//import { HomeComponent } from './web/home/home.component';
//import { ManagerService } from './dashboard/manager/manager'
//import { AccountModule } from './account/account.module';
import { DashboardModule } from './dashboard/dashboard.module';
//import { WebModule } from './web/web.module';

//import { HeaderComponent } from './header/header.component';
//import { UserService } from './Services/user.service';
//import { RootComponent } from './web/root/root.component';




@NgModule({
    imports: [BrowserModule, BrowserAnimationsModule, ReactiveFormsModule, HttpModule, routing, DashboardModule, //WebModule
    ],
    declarations: [AppComponent,  //RootComponent //HeaderComponent //ManagerComponent
    ],
    providers: [{ provide: LocationStrategy, useClass: HashLocationStrategy }, { provide: APP_BASE_HREF, useValue: '/' }, //UserService,//ManagerService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
