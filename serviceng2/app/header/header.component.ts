import { Component, OnInit, OnDestroy } from '@angular/core';
import {Subscription} from 'rxjs/Subscription';
//import { Observable } from 'rxjs/Rx';
import { UserService } from '../Services/user.service';

@Component({
    selector: 'app-header',
    templateUrl: 'app/header/header.component.html',
    //providers: [UserService],

    //styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit, OnDestroy {

    status: boolean;
    private subscription: Subscription;
   

   
    constructor(private userService: UserService) {
       
    }

    logout() {
        this.userService.logout();
    }

    ngOnInit() {
        this.subscription = this.userService.authNavStatus$.subscribe(status => this.status = status);
       
    }

    ngOnDestroy() {
        // prevent memory leak when component is destroyed
        this.subscription.unsubscribe();
    }
}
