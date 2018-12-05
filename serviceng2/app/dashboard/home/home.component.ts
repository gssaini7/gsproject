import { Component, OnInit } from '@angular/core';

import { AuthcrudService } from '../../Services/authcrud.service';


@Component({
    selector: 'app-home',
    //templateUrl: '/home.component.html',
    templateUrl: 'app/dashboard/home/home.component.html',

    //styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

    constructor( private _crudService: AuthcrudService) { }

    ngOnInit() {
       

    }

   

}
