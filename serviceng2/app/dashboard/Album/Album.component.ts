import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AlbumModel } from '../../Models/credentials.interface';
import { AuthcrudService } from '../../Services/authcrud.service';
import { Global } from '../../Shared/global';
import { Message } from 'primeng/components/common/api';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DBOperation } from '../../Shared/enum';
import { MenuItem } from 'primeng/components/common/api';


@Component({
    selector: 'album-crud',
    templateUrl: 'app/dashboard/Album/Album.component.html',
    //styleUrls: ['./login-form.component.scss']
})

export class AlbumComponent implements OnInit {
    msgs: Message[] = [];
    msg: string;
    mainFrm: FormGroup;
    displayDialog: boolean;
    mainobjlist: AlbumModel[];
    mainobj: AlbumModel;
    indLoading: boolean = false;
    dbops: DBOperation;
    isNewForm: boolean = false;
    cmitems: MenuItem[];

    @Output()
    notify: EventEmitter<string> = new EventEmitter<string>();

    sendAlbum(): void {
        this.notify.emit(this.mainobj.AlbumModelid);
    }

    constructor(private fb: FormBuilder, private _crudService: AuthcrudService) { }

    ngOnInit(): void {
        this.InitilizeFormItems();
        this.LoadData();

        this.cmitems = [
            { label: 'View', icon: 'fa-search', command: (event) => this.onRowSelect(this.mainobj) },
            { label: 'Edit', icon: 'fa-edit', command: (event) => this.onRowEdit(this.mainobj) }
        ];
    }

    InitilizeFormItems() {
        this.mainFrm = this.fb.group({
            AlbumModelid: [''],
            AlbumName: ['', Validators.required],
            isPublished: [''],
            remarks: [''],
        });
    }

    LoadData(): void {
        this.indLoading = true;
        this.mainobjlist = null;
        this._crudService.get(Global.BASE_USER_ENDPOINT + "Album/GetAll")
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
                var curl = Global.BASE_USER_ENDPOINT + "Album/Create";
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
                        this.showError(this.msg);
                    }
                );
                break;
            case DBOperation.update:
                var curl = Global.BASE_USER_ENDPOINT + "Album/Edit";
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
            case DBOperation.delete:
                var curl = Global.BASE_USER_ENDPOINT + "Album/Delete";

                this._crudService.post(curl, formData._value).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "Data successfully deleted.";
                            this.LoadData();
                        }
                        else {
                            this.msg = "There is some issue in deleting records, please contact to system administrator!"
                        }
                        this.displayDialog = false;
                    },
                    error => {
                        this.msg = error;
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
        this.sendAlbum();
        //this.mainobj = event.data;
        //this.mainFrm.patchValue(this.mainobj);
        //this.displayDialog = true;
        //this.dbops = DBOperation.update;
        //this.isNewForm = false;

    }
    onRowEdit(event: any) {
        this.mainobj = event;
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

   

    showError(errormsg: any) {
        this.msgs = [];
        this.msgs.push({ severity: 'error', summary: 'Error Message', detail: errormsg });
    }
}


