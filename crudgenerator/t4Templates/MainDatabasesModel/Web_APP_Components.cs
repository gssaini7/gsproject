

import { Component, OnInit } from '@angular/core';
import { MainDatabasesModel } from '../../Models/credentials.interface';
import { AuthcrudService } from '../../Services/authcrud.service';
import { Global } from '../../Shared/global';
import { Message } from 'primeng/components/common/api';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DBOperation, MessageSeverity } from '../../Shared/enum';


@Component({
  //selector: 'app-register-form',
    templateUrl: 'app/dashboard/MainDatabases/MainDatabases.component.html',
  //styleUrls: ['./login-form.component.scss']
})

export class MainDatabasesComponent implements OnInit {
    msgs: Message[] = [];
    msg: string;
    mainFrm: FormGroup;
    displayDialog: boolean;
    mainobjlist: MainDatabasesModel[];
    mainobj: MainDatabasesModel;
    indLoading: boolean = false;
    dbops: DBOperation;
    isNewForm: boolean = false;
    
    constructor(private fb: FormBuilder, private _crudService: AuthcrudService) { }

    ngOnInit(): void {
        this.InitilizeFormItems();
        this.LoadData();
    }

    InitilizeFormItems() {
        this.mainFrm = this.fb.group({
		 	 MainDatabasesModelid: [''],
	 IDataSource: [''],
	 IInitialCatalog: [''],
	 IUsername: [''],
	 IPassword: [''],
	 IName: [''],
	 IUIDCode: [''],
 isPublished: [''],
    remarks: [''],
        });
    }

    LoadData(): void {
        this.indLoading = true;
        this.mainobjlist = null;
        this._crudService.get(Global.BASE_USER_ENDPOINT + "MainDatabases/GetAll")
            .subscribe(records => {
                this.mainobjlist = records;
                this.indLoading = false;
            },
            error => this.msg = <any>error);
    }


    onSubmit(formData: any) {
        this.msg = "";
        switch (this.dbops) {
            case DBOperation.create:
			 if (formData._value.isPublished == null || formData._value.isPublished == '')
                    formData._value.isPublished = false;
                var curl = Global.BASE_USER_ENDPOINT + "MainDatabases/Create";
                this._crudService.post(curl, formData._value).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "Data successfully added.";
                            this.showMessage(MessageSeverity.success, this.msg);

                            this.LoadData();
                        }
                        else {
                            this.msg = "There is some issue in saving records, please contact to system administrator!"
                        }
                        this.displayDialog = false;
                    },
                    error => {
                        this.msg = error;
                                                this.showMessage(MessageSeverity.error, this.msg);

                    }
                );
                break;
            case DBOperation.update:
                var curl = Global.BASE_USER_ENDPOINT + "MainDatabases/Edit";
                this._crudService.post(curl, formData._value).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "Data successfully updated.";
                            this.showMessage(MessageSeverity.success, this.msg);

                            this.LoadData();
                        }
                        else {
                            this.msg = "There is some issue in saving records, please contact to system administrator!"
                        }
                        this.displayDialog = false;
                    },
                    error => {
                        this.msg = error;
                                               this.showMessage(MessageSeverity.error, this.msg);

                    }
                );
                break;
            case DBOperation.delete:
                var curl = Global.BASE_USER_ENDPOINT + "MainDatabases/Delete";

                this._crudService.post(curl, formData._value).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "Data successfully deleted.";
                            this.showMessage(MessageSeverity.success, this.msg);

                            this.LoadData();
                        }
                        else {
                            this.msg = "There is some issue in deleting records, please contact to system administrator!"
                        }
                        this.displayDialog = false;
                    },
                    error => {
                        this.msg = error;
                        this.showMessage(MessageSeverity.error, this.msg);

                    }
                );
                break;

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

   showMessage(severity: any, errormsg: any) {
        this.msgs = [];
        this.msgs.push({ severity: severity, summary: 'Message', detail: errormsg });
    }
}


