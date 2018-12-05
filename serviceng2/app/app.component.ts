import { Component, ViewEncapsulation } from '@angular/core';

@Component({
    //selector: 'app-root',
    selector: "user-app",
    encapsulation: ViewEncapsulation.None,
    templateUrl: './app/app.component.html',
    //styleUrls: ['./app.component.scss']
})
export class AppComponent {
    title = 'app works!';
}



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