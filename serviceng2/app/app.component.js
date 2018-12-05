"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var AppComponent = /** @class */ (function () {
    function AppComponent() {
        this.title = 'app works!';
    }
    AppComponent = __decorate([
        core_1.Component({
            //selector: 'app-root',
            selector: "user-app",
            encapsulation: core_1.ViewEncapsulation.None,
            templateUrl: './app/app.component.html',
        })
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;
//import { Component } from "@angular/core"
//@Component({
//    selector: "user-app",
//    template: `
//                <div>
//                    <nav class='navbar navbar-inverse'>
//                        <div class='container-fluid'>
//                            <ul class='nav navbar-nav'>
//                                <li><a [routerLink]="['home']">Home</a></li>
//                                <li><a [routerLink]="['login']">Login</a></li>
//                                <li><a [routerLink]="['register']">Register</a></li>
//                            </ul>
//                        </div>
//                    </nav>
//                    <div class='container'>
//                        <router-outlet></router-outlet>
//                    </div>
//                 </div>
//                `
//})
//export class AppComponent {
//} 
//# sourceMappingURL=app.component.js.map