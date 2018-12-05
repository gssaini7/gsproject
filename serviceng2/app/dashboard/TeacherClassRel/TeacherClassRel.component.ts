

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { TCSSRelModel, ClassModel, SubjectModel, SectionModel } from '../../Models/credentials.interface';
import { AuthcrudService } from '../../Services/authcrud.service';
import { Global } from '../../Shared/global';
import { Message, SelectItem } from 'primeng/components/common/api';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DBOperation } from '../../Shared/enum';


@Component({
    //selector: 'app-register-form',
    templateUrl: 'app/dashboard/TeacherClassRel/TeacherClassRel.component.html',
    //styleUrls: ['./login-form.component.scss']
})

export class TCSSRelComponent implements OnInit {
    msgs: Message[] = [];
    msg: string;
    mainFrm: FormGroup;
    displayDialog: boolean;
    mainobjlist: TCSSRelModel[];
    mainobj: TCSSRelModel;
    indLoading: boolean = false;
    dbops: DBOperation;
    isNewForm: boolean = false;
    selecteduserid: string;
    classes: ClassModel[];
    sections: SectionModel[];
    subjects: SubjectModel[];


    constructor(private fb: FormBuilder, private _crudService: AuthcrudService, private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.InitilizeFormItems();
        this.route.params.subscribe(params => {
            this.selecteduserid = params['adminid']; // --> Name must match wanted parameter
        });
        this.LoadData();
    }

    InitilizeFormItems() {
        this.mainFrm = this.fb.group({
            TCSSRelModelid: [''],
            Teacherid: [''],
            ClassModelid: [''],
            SectionModelid: [''],
            SubjectModelid: [''],
            isPublished: [''],
            remarks: [''],
            ForClass: ['', Validators.required],
            ForSection: ['', Validators.required],
            ForSubject: ['', Validators.required],

        });
    }

    LoadData(): void {
        this.indLoading = true;
        this.mainobjlist = null;
        this._crudService.get(Global.BASE_USER_ENDPOINT + "TCSSRel/GetAll?id=" + this.selecteduserid)
            .subscribe(records => {
                this.mainobjlist = records;
                this.indLoading = false;
            },
            error => this.msg = <any>error);
    }

    LoadClasses(): void {
        this.indLoading = true;
        this.classes = null;
        this._crudService.get(Global.BASE_USER_ENDPOINT + "desk/GetAllClasses")
            .subscribe(records => {
                this.classes = records;
                this.indLoading = false;
            },
            error => this.msg = <any>error);
    }

    LoadSections(): void {
        this.indLoading = true;
        this.sections = null;
        this._crudService.get(Global.BASE_USER_ENDPOINT + "desk/GetAllSections")
            .subscribe(records => {
                this.sections = records;
                this.indLoading = false;
            },
            error => this.msg = <any>error);
    }
    LoadSubjects(): void {
        this.indLoading = true;
        this.subjects = null;
        this._crudService.get(Global.BASE_USER_ENDPOINT + "desk/GetAllSubjects")
            .subscribe(records => {
                this.subjects = records;
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
                formData._value.Teacherid = this.selecteduserid;
                var curl = Global.BASE_USER_ENDPOINT + "TCSSRel/Create";
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
                var curl = Global.BASE_USER_ENDPOINT + "TCSSRel/Edit";
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
                var curl = Global.BASE_USER_ENDPOINT + "TCSSRel/Delete";

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

        this.LoadClasses();
        this.LoadSections();
        this.LoadSubjects();
    }

    save(formData: any) {
        this.SetControlsState(true);
        //formData._value.ClassModelid = formData._value.ClassModelid.ClassModelid;
        //formData._value.SectionModelid = formData._value.SectionModelid.SectionModelid;
        //formData._value.SubjectModelid = formData._value.SubjectModelid.SubjectModelid;

        this.onSubmit(formData);
    }

    onRowSelect(event: any) {

        this.mainobj = event.data;

        //this.mainobj.ClassModelid = { ClassModelid: event.data.ClassModelid };
        //this.mainobj.SectionModelid = { SectionModelid: event.data.SectionModelid };
        //this.mainobj.SubjectModelid = { SubjectModelid: event.data.SubjectModelid };

        this.mainFrm.patchValue(this.mainobj);
        this.displayDialog = true;
        this.dbops = DBOperation.update;
        this.isNewForm = false;

        this.LoadClasses();
        this.LoadSections();
        this.LoadSubjects();
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


