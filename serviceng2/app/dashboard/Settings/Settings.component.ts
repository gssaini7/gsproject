

import { Component, OnInit } from '@angular/core';
import { Router,ActivatedRoute } from '@angular/router';

import { AuthcrudService } from '../../Services/authcrud.service';
import { Global } from '../../Shared/global';
import { Message } from 'primeng/components/common/api';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DBOperation } from '../../Shared/enum';


@Component({
    //selector: 'app-register-form',
    templateUrl: 'app/dashboard/Settings/Settings.component.html',
    //styleUrls: ['./login-form.component.scss']
})

export class SettingsComponent implements OnInit {
    msgs: Message[] = [];
    msg: string;
    mainFrm: FormGroup;
    mainFrmsms: FormGroup;
    mainFrmmail: FormGroup;
    mainFrmpayment: FormGroup;

    displayDialog: boolean;
    mainobj: any;
    indLoading: boolean = false;
    dbops: DBOperation;
    isNewForm: boolean = false;

    settingstype: string;

    //SettingsModel: {
    //    SettingsType: string,
    //    SettingsContent: string,
    //}

    SMSModel: { username: string, msgtoken: string, senderid: string, apiurl: string }
    MailModel: { username: string, password: string, smtp: string, port: string }
    OnlinePaymentModel: { api_key: string, salt: string, url: string }
  
    constructor(private fb: FormBuilder, private _crudService: AuthcrudService, private route: ActivatedRoute,private router: Router) {
    }

    ngOnInit(): void {
        this.InitilizeFormItems();
        this.LoadData();
    }

    InitilizeFormItems() {
        this.route.params.subscribe(params => {
            this.settingstype = params['type']; // --> Settings type Name
        });
    }

    InitilizeFormItemsAfterLoad() {
        if (this.mainFrm !== undefined)
            this.mainFrm.reset();
        if (this.settingstype == "SMS") {
            this.mainFrm = this.fb.group({
                username: [''],
                msgtoken: [''],
                senderid: [''],
                apiurl: [''],
            });
        }
        else if (this.settingstype == "OnlinePayment") {
            this.mainFrm = this.fb.group({
                api_key: [''],
                salt: [''],
                url: [''],
            });
        }
        else if (this.settingstype == "Mail") {
            this.mainFrm = this.fb.group({
                username: [''],
                password: [''],
                smtp: [''],
                port: [''],
            });
        }
       
    }

    LoadData(): void {
        if (this.settingstype === undefined)
        {
            this.settingstype = "Mail";
        }

        this.InitilizeFormItemsAfterLoad();

        this.indLoading = true;
        this.mainobj = null;
        this._crudService.get(Global.BASE_USER_ENDPOINT + "Settings/Get?type=" + this.settingstype)
            .subscribe(records => {
                this.mainobj = records;
                this.mainFrm.patchValue(JSON.parse(records.SettingsContent));
                this.indLoading = false;
            },
            error => this.msg = <any>error);
    }


    onSubmit(formData: any) {
        this.msg = "";
        switch (this.dbops) {
            case DBOperation.create:
                var curl = Global.BASE_USER_ENDPOINT + "Settings/Create";
                this._crudService.post(curl, formData).subscribe(
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
                        this.showError(this.msg);
                    }
                );
                break;
            case DBOperation.update:
                var curl = Global.BASE_USER_ENDPOINT + "Settings/Edit";
                this._crudService.post(curl, formData).subscribe(
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
            //    var curl = Global.BASE_USER_ENDPOINT + "Settings/Delete";

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

    //SetControlsState(isEnable: boolean) {
    //    isEnable ? this.mainFrm.enable() : this.mainFrm.disable();
    //}

    //showDialogToAdd() {
    //    this.isNewForm = true;
    //    this.mainFrm.reset();
    //    this.displayDialog = true;
    //    this.dbops = DBOperation.create;
    //}

    save(formData: any) {
        //this.SetControlsState(true);
        this.dbops = DBOperation.update;
        let datatosend = { SettingsModelid: this.mainobj.SettingsModelid, SettingsType: this.settingstype, SettingsContent: JSON.stringify(formData._value) };
        this.onSubmit(datatosend);
    }

    //onRowSelect(event: any) {
    //    this.mainobj = event.data;
    //    this.mainFrm.patchValue(this.mainobj);
    //    this.displayDialog = true;
    //    this.dbops = DBOperation.update;
    //    this.isNewForm = false;

    //}

    //delete() {
    //    if (confirm("Are you sure to delete ")) {
    //        this.dbops = DBOperation.delete;
    //        this.onSubmit(this.mainFrm);
    //    }
    //}
    changetab(selectedtype: string) {
        this.settingstype = selectedtype;
        this.LoadData();
        this.router.navigate(['/settings', this.settingstype]);
        this.msg = "";
    }
    showError(errormsg: any) {
        this.msgs = [];
        this.msgs.push({ severity: 'error', summary: 'Error Message', detail: errormsg });
    }
}


