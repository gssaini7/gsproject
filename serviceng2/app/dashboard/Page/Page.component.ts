
import { Component, OnInit } from '@angular/core';
import { PageModel } from '../../Models/credentials.interface';
import { Router } from '@angular/router';
import { AuthcrudService } from '../../Services/authcrud.service';
import { Global } from '../../Shared/global';
import { DataTableModule } from 'primeng/components/datatable/datatable';
//import { Message } from 'primeng/components/common/api';
//import { FormBuilder, FormGroup, Validators } from '@angular/forms';
//import { DBOperation } from '../../Shared/enum';
//import { EqualsValidator } from '../../Services/equals.validators';
//import { SelectItem } from 'primeng/components/common/api';
@Component({
  //selector: 'app-register-form',
    templateUrl: 'app/dashboard/Page/Page.component.html',
  //styleUrls: ['./login-form.component.scss']
})

export class PageComponent implements OnInit {

    //msgs: Message[] = [];
    msg: string;
    //mainFrm: FormGroup;
    //displayDialog: boolean;
    mainobjlist: PageModel[];
    //mainobj: PageModel;
    indLoading: boolean = false;
    //dbops: DBOperation;
    //isNewForm: boolean = false;


    constructor(private _crudService: AuthcrudService, private router: Router) { }

    ngOnInit(): void {
        //this.InitilizeFormItems();
        this.LoadData();
        
    }

    //InitilizeFormItems() {
    //    this.mainFrm = this.fb.group({
    //        PageModelid: [''],
    //        PageTitle: ['', Validators.required],
    //        pageurl: ['', Validators.required],
    //        pagecontent: [''],
    //        isPublished: [''],
    //        remarks: [''],
    //    });
    //}

    LoadData(): void {
        this.indLoading = true;
        this.mainobjlist = null;
        this._crudService.get(Global.BASE_USER_ENDPOINT + "page/GetAll")
            .subscribe(records => {
                this.mainobjlist = records;
                this.indLoading = false;
            },
            error => this.msg = <any>error);
    }


    //onSubmit(formData: any) {
    //    this.msg = "";
    //    console.log(formData._value);
    //    switch (this.dbops) {
    //        case DBOperation.create:
                
    //            var curl = Global.BASE_USER_ENDPOINT + "page/Create";
    //            if (formData._value.isPublished == null)
    //                formData._value.isPublished = false;

    //            this._crudService.post(curl, formData._value).subscribe(
    //                data => {
    //                    if (data == 1) //Success
    //                    {
    //                        this.msg = "Data successfully added.";
    //                        this.LoadData();
    //                    }
    //                    else {
    //                        this.msg = "There is some issue in saving records, please contact to system administrator!"
    //                    }
    //                    this.displayDialog = false;
    //                },
    //                error => {
    //                    this.msg = error;
    //                    this.showError(this.msg);

    //                }
    //            );
    //            break;
    //        case DBOperation.update:

    //            var curl = Global.BASE_USER_ENDPOINT + "page/Edit";
    //            this._crudService.post(curl, formData._value).subscribe(
    //                data => {
    //                    if (data == 1) //Success
    //                    {
    //                        this.msg = "Data successfully updated.";
    //                        this.LoadData();
    //                    }
    //                    else {
    //                        this.msg = "There is some issue in saving records, please contact to system administrator!"
    //                    }
    //                    this.displayDialog = false;
    //                },
    //                error => {
    //                    this.msg = error;
    //                    this.showError(this.msg);
    //                }
    //            );
    //            break;
    //        //case DBOperation.delete:
    //        //    var curl = Global.BASE_USER_ENDPOINT + "item/Delete";

    //        //    this._crudService.post(curl, formData._value).subscribe(
    //        //        data => {
    //        //            if (data == 1) //Success
    //        //            {
    //        //                this.msg = "Data successfully deleted.";
    //        //                this.LoadData();
    //        //            }
    //        //            else {
    //        //                this.msg = "There is some issue in deleting records, please contact to system administrator!"
    //        //            }
    //        //            this.displayDialog = false;
    //        //        },
    //        //        error => {
    //        //            this.msg = error;
    //        //        }
    //        //    );
    //        //    break;

    //    }
    //}

    //SetControlsState(isEnable: boolean) {
    //    isEnable ? this.mainFrm.enable() : this.mainFrm.disable();
    //}

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

    addNewPage() {
        this.router.navigate(['/page/0']);
    }

    onRowSelect(event: any) {
        //this.mainobj = event.data;
        //this.mainFrm.patchValue(this.mainobj);
        //this.displayDialog = true;
        //this.dbops = DBOperation.update;
        //this.isNewForm = false;
            
        this.router.navigate(['/page', event.data.PageModelid]);

    }

    //delete() {
    //    if (confirm("Are you sure to delete ")) {
    //        this.dbops = DBOperation.delete;
    //        this.onSubmit(this.mainFrm);
    //    }
    //}

    

    //showError(errormsg:any) {
    //    this.msgs = [];
    //    this.msgs.push({ severity: 'error', summary: 'Error Message', detail: errormsg });
    //}

   
}


