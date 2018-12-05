

import { Component, OnInit, Input } from '@angular/core';
import { ImageGalleryModel } from '../../Models/credentials.interface';
import { AuthcrudService } from '../../Services/authcrud.service';
import { Global } from '../../Shared/global';
import { Message } from 'primeng/components/common/api';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DBOperation } from '../../Shared/enum';


@Component({
    selector: 'image-gallery',
    templateUrl: 'app/dashboard/ImageGallery/ImageGallery.component.html',
    //styleUrls: ['./login-form.component.scss']
})

export class ImageGalleryComponent implements OnInit {
    msgs: Message[] = [];
    msg: string;
    mainFrm: FormGroup;
    displayDialog: boolean;
    mainobjlist: ImageGalleryModel[];
    mainobj: ImageGalleryModel;
    indLoading: boolean = false;
    dbops: DBOperation;
    isNewForm: boolean = false;
    currentalbumid: string;
    dbcodeid: string;

   

    constructor(private fb: FormBuilder, private _crudService: AuthcrudService) { }

    ngOnInit(): void {
        this.InitilizeFormItems();
        //this.LoadData();
    }

    InitilizeFormItems() {
        this.mainFrm = this.fb.group({
            ImageGalleryModelid: [''],
            ImageName: [''],
            isPublished: [''],
            remarks: [''],
            
        });

       
    }

    LoadData(id: string): void {
        this.dbcodeid = localStorage.getItem('dbcodeid');
        this.msg = '';
        if (id == undefined)
            return;
        this.currentalbumid = id;
        this.indLoading = true;
        this.mainobjlist = null;
        this._crudService.get(Global.BASE_USER_ENDPOINT + "ImageGallery/GetAll?id=" + this.currentalbumid)
            .subscribe(records => {
                this.mainobjlist = records;

                this.indLoading = false;
            },
            error => this.msg = <any>error);
    }


    onSubmit(formData: any) {
        this.msg = "";
        

        switch (this.dbops) {
            //case DBOperation.create:
            //    if (formData._value.isPublished == null || formData._value.isPublished == '')
            //        formData._value.isPublished = false;
            //    var curl = Global.BASE_USER_ENDPOINT + "ImageGallery/Create";
            //    formData._value.AlbumModelid = this.currentalbumid;

            //    this._crudService.post(curl, formData._value).subscribe(
            //        data => {
            //            if (data == 1) //Success
            //            {
            //                this.msg = "Data successfully added.";
            //                this.LoadData(this.currentalbumid);
            //            }
            //            else {
            //                this.msg = "There is some issue in saving records, please contact to system administrator!"
            //            }
            //            this.displayDialog = false;
            //        },
            //        error => {
            //            this.msg = error;
            //            this.showError(this.msg);
            //        }
            //    );
            //    break;
            //case DBOperation.update:
            //    var curl = Global.BASE_USER_ENDPOINT + "ImageGallery/Edit";
            //    this._crudService.post(curl, formData._value).subscribe(
            //        data => {
            //            if (data == 1) //Success
            //            {
            //                this.msg = "Data successfully updated.";
            //                this.LoadData(this.currentalbumid);
            //            }
            //            else {
            //                this.msg = "There is some issue in saving records, please contact to system administrator!"
            //            }
            //            this.displayDialog = false;
            //        },
            //        error => {
            //            this.msg = error;
            //            this.showError(this.msg);
            //        }
            //    );
            //    break;
            case DBOperation.delete:
                var curl = Global.BASE_USER_ENDPOINT + "ImageGallery/Delete";

                this._crudService.post(curl, formData).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "Data successfully deleted.";
                            this.LoadData(this.currentalbumid);
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

    //showDialogToAdd() {
    //    this.isNewForm = true;
    //    this.mainFrm.reset();
    //    this.displayDialog = true;
    //    this.dbops = DBOperation.create;
    //}

    //save(formData: any) {
    //    this.SetControlsState(true);
    //    this.onSubmit(formData);
    //}

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

    delete(event: any) {
        if (confirm("Are you sure to delete ")) {
          
            this.dbops = DBOperation.delete;
            this.onSubmit(event);
        }
    }

    showError(errormsg: any) {
        this.msgs = [];
        this.msgs.push({ severity: 'error', summary: 'Error Message', detail: errormsg });
    }

    getImageGalleryofAlbum(albumid: string): void {
        this.LoadData(albumid);
    }

    onUpload(event: any) {
        for (let file of event.files) {
            this.msg = "Image saved.";
            this.LoadData(this.currentalbumid);
        }
    }
    onBeforeSend(event: any) {
        let authToken = localStorage.getItem('auth_token');
        //event.xhr.setRequestHeader('Content-Type', 'application/json');
        event.xhr.setRequestHeader('Authorization', `Bearer ${authToken}`);

        event.formData.append('AlbumModelid', this.currentalbumid);
        event.formData.append('dbcodeid', this.dbcodeid);

    }

}


