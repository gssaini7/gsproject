
import { Component, OnInit } from '@angular/core';
import { newuser } from '../../Models/credentials.interface';
import { AuthcrudService } from '../../Services/authcrud.service';
import { Global } from '../../Shared/global';
import { DataTableModule } from 'primeng/components/datatable/datatable';
import { Message } from 'primeng/components/common/api';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DBOperation } from '../../Shared/enum';
import { EqualsValidator } from '../../Services/equals.validators';

@Component({
  //selector: 'app-register-form',
    templateUrl: 'app/dashboard/SupportAdmin/adminmanager.component.html',
  //styleUrls: ['./login-form.component.scss']
})

export class AdminManagerComponent implements OnInit {
    msgs: Message[] = [];
    msg: string;
    mainFrm: FormGroup;
    displayDialog: boolean;
    mainobjlist: newuser[];
    mainobj: newuser;
    indLoading: boolean = false;
    dbops: DBOperation;
    isNewForm: boolean = false;
    //filteritemtypes: SelectItem[];
    supportrolename: string = "Support";

    constructor(private fb: FormBuilder, private _crudService: AuthcrudService) { }

    ngOnInit(): void {
        this.InitilizeFormItems();
        this.LoadData();
    }

    InitilizeFormItems() {
        this.mainFrm = this.fb.group({
            UserID: [''],
            Mobile: ['', Validators.required],
            Email: ['', Validators.email],
            Password: [''],
            ConfirmPassword: [''],
            nameofuser: [''],
            remarks: [''],
        }, { validator: EqualsValidator.equals('Password', 'ConfirmPassword') });
    }

    LoadData(): void {
        this.indLoading = true;
        this.mainobjlist = null;
        this._crudService.get(Global.BASE_USER_ENDPOINT + "Admin/users?rolename=" + this.supportrolename)
            .subscribe(records => {
                this.mainobjlist = records;
                this.indLoading = false;
            },
            error => this.msg = <any>error);
    }


    onSubmit(formData: any) {
        this.msg = "";
        console.log(formData._value);
        switch (this.dbops) {
            case DBOperation.create:
                
                var curl = Global.BASE_USER_ENDPOINT + "Account/Register";
                formData._value.UserRole = this.supportrolename;
                this._crudService.post(curl, formData._value).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "Data successfully added.";
                            this.LoadData();
                        }
                        else {
                            this.msg = "There is some issue in saving records, please contact to system administrator!"
                        }
                        this.displayDialog = false;
                    },
                    error => {
                        this.msg = error;
                    }
                );
                break;
            case DBOperation.update:

                var curl = Global.BASE_USER_ENDPOINT + "Admin/UpdateUser";
                this._crudService.post(curl, formData._value).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "Data successfully updated.";
                            this.LoadData();
                        }
                        else {
                            this.msg = "There is some issue in saving records, please contact to system administrator!"
                        }
                        this.displayDialog = false;
                    },
                    error => {
                        this.msg = error;
                        this.showError(this.msg);
                    }
                );
                break;
            //case DBOperation.delete:
            //    var curl = Global.BASE_USER_ENDPOINT + "item/Delete";

            //    this._crudService.post(curl, formData._value).subscribe(
            //        data => {
            //            if (data == 1) //Success
            //            {
            //                this.msg = "Data successfully deleted.";
            //                this.LoadData();
            //            }
            //            else {
            //                this.msg = "There is some issue in deleting records, please contact to system administrator!"
            //            }
            //            this.displayDialog = false;
            //        },
            //        error => {
            //            this.msg = error;
            //        }
            //    );
            //    break;

        }
    }

    SetControlsState(isEnable: boolean) {
        isEnable ? this.mainFrm.enable() : this.mainFrm.disable();
    }

    showDialogToAdd() {
        this.isNewForm = true;
        this.mainFrm.reset();
        this.displayDialog = true;
        this.dbops = DBOperation.create;
    }

    save(formData: any) {
        this.SetControlsState(true);
        this.onSubmit(formData);
    }

    onRowSelect(event: any) {
        this.mainobj = event.data;
        this.mainFrm.patchValue(this.mainobj);
        this.displayDialog = true;
        this.dbops = DBOperation.update;
        this.isNewForm = false;

    }

    delete() {
        if (confirm("Are you sure to delete ")) {
            this.dbops = DBOperation.delete;
            this.onSubmit(this.mainFrm);
        }
    }

    showError(errormsg:any) {
        this.msgs = [];
        this.msgs.push({ severity: 'error', summary: 'Error Message', detail: errormsg });
    }
}


//export class AdminManagerComponent {
//  errors: string;
//  isRequesting: boolean;
//  credentials: newuser = { LoginUserName: '', Password: '', ConfirmPassword: '', Email: '', UserRole: ''  };

//  constructor(private userService: AuthcrudService) { }

//  createsupport({ value, valid }: { value: newuser, valid: boolean }) {
//    this.isRequesting = true;
//    this.errors = '';
//    if (valid) {
//        value.UserRole = "Support";
//        this.userService.post(Global.BASE_USER_ENDPOINT + "account/register", value)
//        .finally(() => this.isRequesting = false)
//        .subscribe(
//            result => {
//                if (result) { }
//            },
//            error => {
//            this.errors = error
//        });
//    }
//  }
//}
