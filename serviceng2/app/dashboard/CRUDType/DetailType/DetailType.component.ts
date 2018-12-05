

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { BlogModel } from '../../../Models/credentials.interface';
import { AuthcrudService } from '../../../Services/authcrud.service';
import { Global } from '../../../Shared/global';
import { Message, SelectItem } from 'primeng/components/common/api';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DBOperation, MessageSeverity } from '../../../Shared/enum';

@Component({
    selector: 'detail-type-crud',
    templateUrl: 'app/dashboard/CRUDType/DetailType/DetailType.component.html',
    //styleUrls: ['./login-form.component.scss']
})

export class DetailTypeComponent implements OnInit {
    msgs: Message[] = [];
    msg: string;
    mainFrm: FormGroup;
    mainobjlist: BlogModel[];
    mainobj: BlogModel;
    indLoading: boolean = false;
    dbops: DBOperation;
    isNewForm: boolean = false;
    currentid: string;
    myBlogType: string;
    myBlogTypeId: string;

    dbcodeid: string;
    subjects: SelectItem[];


    
    
    constructor(private fb: FormBuilder, private _crudService: AuthcrudService, private route: ActivatedRoute, private router: Router) {

    }

    ckEditorConfig: {} = 
        {
                "toolbarGroups": [
                    { "name": "document", "groups": ["basicstyles"] },
                    //{ "name": "editing", "groups": ["find", "selection", "spellchecker", "editing"] },
                    //{ "name": "forms", "groups": ["forms"] }
                ],
                "removeButtons": "Source,Save,Templates,Find,Replace,Scayt,SelectAll",
                extraPlugins: 'divarea'
        }

    ngOnInit(): void {
        this.InitilizeFormItems();
        this.LoadBlogType();

        //this.LoadData();
       
    }

    InitilizeFormItems() {
        this.mainFrm = this.fb.group({
            BlogModelid: [''],
            title: ['', Validators.required],
            description: ['', Validators.required],
            imagepath: [''],
            isPublished: [''],
            remarks: [''],
            BlogTypeName: [''],
            filetype: [''],
            SubjectModelid: [''],

        });

        this.route.params.subscribe(params => {
            this.currentid = params['id']; // --> Blog ID
            this.myBlogType = params['type']; // --> BlogType Name
            this.myBlogTypeId = params['typeid'];// --> BlogType ID
        });

       

    }
    LoadBlogType(): void {
        this._crudService.get(Global.BASE_USER_ENDPOINT + "BlogType/Get?id=" + this.myBlogTypeId)
            .subscribe(records => {
                
                if (records.BlogTypeProperty === "Assignment") {
                    this.LoadSubjects();
                    this.mainFrm.controls['SubjectModelid'].validator = Validators.required;
                }
                else
                    this.LoadData();
               
            },
            error => this.msg = <any>error);
        //this.subjects = [];
        //let item: SelectItem = { label: "English", value: "English" };
        //this.subjects.push(item);
        //let item2: SelectItem = { label: "Computer", value: "Computer" };
        //this.subjects.push(item2);
    }


    LoadData(): void {
        

        if (this.currentid === "new") {
            //this.LoadBlogType();

            this.dbops = DBOperation.create;
            this.mainFrm.reset();
          
            return;
        }

        this.indLoading = true;
        this.mainobjlist = null;
        this._crudService.get(Global.BASE_USER_ENDPOINT + "blog/Get?id=" + this.currentid)
            .subscribe(records => {
               
                
                this.mainFrm.patchValue(records);

               
                this.dbops = DBOperation.update;
                //this.mainobjlist = records;
                this.dbcodeid = localStorage.getItem('dbcodeid');
                //this.indLoading = false;
               
                
            },
            error => {
                this.router.navigate(['/blog', this.myBlogType]);
                //this.msg = <any>error
            });
    }

    
    LoadSubjects() {
        this.indLoading = true;
        this.subjects = [];

        this._crudService.get(Global.BASE_USER_ENDPOINT + "TCSSRel/GetAllByLogedInUser")
            .subscribe(records => {
                let subjectsarray = this._crudService.unique(records, 'ForSubject');
                let subjectslocal = this._crudService.removeDuplicates(subjectsarray, 'SubjectModelid');
                subjectslocal.forEach((e: any) => {
                    let item: SelectItem = { label: e.SubjectName, value: e.SubjectModelid };
                    this.subjects.push(item);
                });
                this.LoadData();

            },
            error => {
                this.LoadData();
            });
    }


    onSubmit(formData: any) {
        this.msg = "";
        switch (this.dbops) {
            case DBOperation.create:
                if (formData._value.isPublished == null || formData._value.isPublished == '')
                    formData._value.isPublished = false;
                formData._value.BlogTypeModelid = this.myBlogTypeId;

                var curl = Global.BASE_USER_ENDPOINT + "blog/Create";
                this._crudService.postwithresponse(curl, formData._value).subscribe(

                    data => {
                        if (data.ok) //Success
                        {
                            this.msg = "Data successfully added. Please upload file.";
                            this.showMessage(MessageSeverity.success, this.msg);
                            let str = data._body.replace(/^"(.*)"$/, '$1');
                            this.dbops = DBOperation.update;

                            this.router.navigate(['/detail', this.myBlogType, this.myBlogTypeId, str]);
                            this.currentid = str;
                            this.LoadData();
                        }
                        else {
                            this.msg = "There is some issue in saving records, please contact to system administrator!"
                        }
                    },
                    error => {
                        this.msg = error;
                        this.showMessage(MessageSeverity.error, this.msg);

                    },
                   
                );
                break;
            case DBOperation.update:
                var curl = Global.BASE_USER_ENDPOINT + "blog/Edit";
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
                            this.showMessage(MessageSeverity.error, this.msg);

                        }
                    },
                    error => {
                        this.msg = error;
                        this.showMessage(MessageSeverity.error, this.msg);

                    }
                );
                break;
            case DBOperation.delete:
                var curl = Global.BASE_USER_ENDPOINT + "blog/Delete";

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
                    },
                    error => {
                        this.msg = error;
                    }
                );
                break;
            case DBOperation.deleteimage:
                var curl = Global.BASE_USER_ENDPOINT + "blog/Deleteimage";
                this._crudService.post(curl, formData._value).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "File successfully deleted.";
                            this.LoadData();
                        }
                        else {
                            this.msg = "There is some issue in deleting records, please contact to system administrator!"
                        }

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

    save(formData: any) {
        this.SetControlsState(true);
        this.onSubmit(formData);
    }

    //onRowSelect(event: any) {
    //    this.mainobj = event.data;
    //    this.mainFrm.patchValue(this.mainobj);
    //    console.log(event);
    //    this.displayDialog = true;
    //    this.dbops = DBOperation.update;
    //    this.isNewForm = false;

    //}

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


    //showError(errormsg: any) {
    //    this.msgs = [];
    //    this.msgs.push({ severity: 'error', summary: 'Error Message', detail: errormsg });
    //}

    deleteimage(formData: any) {
        if (confirm("Are you sure to delete?")) {
            this.dbops = DBOperation.deleteimage;
            this.onSubmit(formData);
        }
    }

    onUpload(event: any) {
        for (let file of event.files) {
            this.msg = "File saved.";
            this.LoadData();
        }
    }
    onBeforeSend(event: any, itemid: string) {
        //event.xhr.setRequestHeader(this._crudService.setheader());
        event.formData.append('BlogModelid', itemid);
        event.formData.append('dbcodeid', this.dbcodeid);
       

    }
}


