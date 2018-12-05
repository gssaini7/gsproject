
import { Subscription } from 'rxjs';
import { Component, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { ChangepasswordModel } from '../../Models/credentials.interface';
import { AuthcrudService } from '../../Services/authcrud.service';
import { Global } from '../../Shared/global';


@Component({
  //selector: 'app-register-form',
  templateUrl: 'app/dashboard/profile/profile.component.html',
  //styleUrls: ['./login-form.component.scss']
})
export class ProfileComponent implements OnDestroy {
    msg: string;
  brandNew: boolean;
  isRequesting: boolean;
  submitted: boolean = false;
  credentials: ChangepasswordModel = { OldPassword: '', NewPassword: '', Confirmpassword: '' };

  constructor(private _crudService: AuthcrudService, private router: Router,private activatedRoute: ActivatedRoute) { }
      
    ngOnInit() {

    // subscribe to router event
       
  }

   ngOnDestroy() {
    // prevent memory leak by unsubscribing
  }

   changepassword({ value, valid }: { value: ChangepasswordModel, valid: boolean }) {
    this.submitted = true;
    this.isRequesting = true;
    this.msg = "";

    if (valid) {
        this._crudService.post(Global.BASE_USER_ENDPOINT + "webdetail/ChangePassword", value).subscribe(
            data => {
                if (data == 1) //Success
                {
                    this.msg = "Data successfully update.";
                }
                else {
                    this.msg = "There is some issue in saving records, please contact to system administrator!"
                }
                this.isRequesting = false;

            },
            error => {
                this.msg = error;
                this.isRequesting = false;

            }
        );

       
    }
  }
}
