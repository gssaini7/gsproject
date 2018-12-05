

import { Component, OnInit } from '@angular/core';
import { NotificationScheduleModel, BlogModel, TCSSRelModel, StudentModel, NotificationToClassSection, ClassSectionRel } from '../../Models/credentials.interface';
import {  ActivatedRoute } from '@angular/router';
import { AuthcrudService } from '../../Services/authcrud.service';
import { Global } from '../../Shared/global';
import { Message, SelectItem, TreeNode, MenuItem } from 'primeng/components/common/api';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DBOperation, MessageSeverity} from '../../Shared/enum';
import { SortByPipe } from '../../Shared/sortby.pipe';


@Component({
    //selector: 'app-register-form',
    templateUrl: 'app/dashboard/NotificationSchedule/NotificationSchedule.component.html',
    //styleUrls: ['./login-form.component.scss']
})

export class NotificationScheduleComponent implements OnInit {
    msgs: Message[] = [];
    msg: string;
    mainFrm: FormGroup;
    displayDialog: boolean;
    mainobjlist: NotificationScheduleModel[];
    mainobj: NotificationScheduleModel;

    blogmodel: BlogModel;

    indLoading: boolean = false;
    dbops: DBOperation;
    isNewForm: boolean = false;
    currentblogid: string;
    selectedteacherid: string;

    dbcodeid: string;

    NotificationTypeToStudent: false;

    classes: SelectItem[];
    classesfilter: SelectItem[];

    sections: SelectItem[];
    sectionsfilter: SelectItem[];

    subjects: SelectItem[];
    students: StudentModel[];

    selectedStudents: StudentModel[];

    filesTree4: TreeNode[];
    selectedFiles2: TreeNode[];
    cmitems: MenuItem[];
    isSingle: boolean = false;

    selectedclass: string;
    filterdsectionslist: SelectItem[];
    selectedsection: string;



    constructor(private fb: FormBuilder, private _crudService: AuthcrudService, private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.route.params.subscribe(params => {
            this.currentblogid = params['blogid']; // --> Name must match wanted parameter
            this.selectedteacherid = params['teacherid']; // --> Name must match wanted parameter
        });


        this.InitilizeFormItems();
        this.LoadBlogData();
        this.LoadData();
        //this.LoadClasses();
        this.LoadStudents();

        this.cmitems = [
            { label: 'Delete', icon: 'fa-trash', command: (event) => this.delete() },
        ];

        //this.TestFn();
    }

    //TestFn() {
    //    this._crudService.get(Global.BASE_USER_ENDPOINT + "appdetail/GetNotificationDetail")
    //        .subscribe(record => {
              
    //        },
    //        error => {
    //        });
    //}

    InitilizeFormItems() {
        this.mainFrm = this.fb.group({
            NotificationScheduleModelid: [''],
            notificationsentdate: [''],
            classname: [''],
            isPublished: [''],
            remarks: [''],
        });
    }

    LoadBlogData(): void {
        this.indLoading = true;
        this.blogmodel = null;
        this._crudService.get(Global.BASE_USER_ENDPOINT + "blog/Get?id=" + this.currentblogid)
            .subscribe(record => {
                this.blogmodel = record;
                this.dbcodeid = localStorage.getItem('dbcodeid');
                if (record.BlogType.BlogTypeProperty === "Assignment") {
                    this.isSingle = true;
                    //this.LoadStudents();
                }
            },
            error => {
            });
    }

    unique(arr: any, prop: any) {

        return arr.map(function (e: any) {  return e[prop]; }).filter(function (e: any, i: any, a: any) {
            return i === a.indexOf(e);
        });
    }

    //removeDuplicates(arr: any, prop: any) {
    //    var newArray = [];
    //    var lookupObject = {};

    //    for (var i in arr) {
    //        lookupObject[arr[i][prop]] = arr[i];
    //    }

    //    for (i in lookupObject) {
    //        newArray.push(lookupObject[i]);
    //    }
    //    return newArray;
    //}



    LoadStudents() {
        this.indLoading = true;
        this.students = null;
        this.classes = [];
        this.sections = [];

        this.classesfilter = [];
        this.sectionsfilter = [];

        this._crudService.get(Global.BASE_USER_ENDPOINT + "desk/GetStudentsByTeacher")
            .subscribe(records => {
                
                this.students = records;

                let classesall = this._crudService.unique(records, 'OfClass');
                let classeslocal = this._crudService.removeDuplicates(classesall,'ClassModelid');

                classeslocal.forEach((e: any) => {
                    let item: SelectItem = { label: e.ClassName, value: e.ClassModelid };
                    this.classes.push(item);
                });

                classeslocal.forEach((e: any) => {
                    let item: SelectItem = { label: e.ClassName, value: e.ClassName };
                    this.classesfilter.push(item);
                });
               

                let sectionsall = this._crudService.unique(records, 'OfSection');
                let sectionslocal = this._crudService.removeDuplicates(sectionsall, 'SectionModelid');

              

                sectionslocal.forEach((e: any) => {
                    let item: SelectItem = { label: e.SectionName, value: e.SectionModelid };
                    this.sections.push(item);
                });

                sectionslocal.forEach((e: any) => {
                    let item: SelectItem = { label: e.SectionName, value: e.SectionName };
                    this.sectionsfilter.push(item);
                });

                if (!this.isSingle)
                    this.LoadAdvancedDialog();
            },
            error => {
            });
        
        //if (this.isSingle) {
        //    let finalstudents: StudentModel[] = [];
        //    this._crudService.get(Global.BASE_USER_ENDPOINT + "TCSSRel/GetAllByLogedInUser")
        //        .subscribe(records => {
        //            records = records.filter((a: any) => a.SubjectModelid === this.blogmodel.BlogSubType);

        //            records.forEach((e: any) => {
        //                let nstudents = this.students.filter((a: any) => a.strClass === e.ClassModelid && a.SectionModelid === e.SectionModelid);
        //                finalstudents = finalstudents.concat(nstudents);
        //            });
        //            this.students = finalstudents;
        //        });
        //}
    }

    //LoadClasses() {
    //    this.indLoading = true;
    //    this.classes = [];
    //    this._crudService.get(Global.BASE_USER_ENDPOINT + "TCSSRel/GetAllByLogedInUser")
    //        .subscribe(record => {
    //            let classeslocal = this.unique(record, 'ClassModelid');
    //            classeslocal.forEach((e: any) => {
    //                let item: SelectItem = { label: e, value: e };
    //                this.classes.push(item);
    //            });
    //        },
    //        error => {
    //        });
    //}



    LoadData(): void {
        this.indLoading = true;
        this.mainobjlist = null;
        this._crudService.get(Global.BASE_USER_ENDPOINT + "NotificationSchedule/GetBlogSchedules?id=" + this.currentblogid)
            .subscribe(records => {
                this.mainobjlist = records;

                let sortPipeFilter = new SortByPipe();
                sortPipeFilter.transform(records, 'notificationsentdate', true)
                this.indLoading = false;
            },
            error => this.msg = <any>error);
    }


    onSubmit(formData: any) {
        this.msg = "";
        switch (this.dbops) {
            case DBOperation.create:

                var curl = Global.BASE_USER_ENDPOINT + "NotificationSchedule/Create";
                this._crudService.post(curl, formData).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "Data successfully added.";
                            this.LoadData();
                            this.selectedStudents = [];
                            if (this.isSingle) {
                                this.filterdsectionslist = [];
                                this.selectedclass = null;
                            }
                            else
                                this.selectedFiles2 = [];
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
                var curl = Global.BASE_USER_ENDPOINT + "NotificationSchedule/Edit";
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
                        this.showMessage(MessageSeverity.error,this.msg);
                    }
                );
                break;
            case DBOperation.delete:
                var curl = Global.BASE_USER_ENDPOINT + "NotificationSchedule/Delete";

                this._crudService.post(curl, formData).subscribe(
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

    ShowAdvancedDialog() {

        this.displayDialog = true;

    }

    LoadAdvancedDialog() {
        var maintree: TreeNode[] = [];
        this.classes.forEach((item: any) => {
            var node: TreeNode = this.settreenode(item.label, item.value);
            let sectionsarray = this._crudService.unique(this.students.filter((a: StudentModel) => a.OfClass.ClassModelid === item.value), 'OfSection');
            let sectionslocal = this._crudService.removeDuplicates(sectionsarray, 'SectionModelid');
            
            sectionslocal.forEach((sectionitem: any) => {
                var sectionnode = this.settreenode(sectionitem.SectionName, sectionitem.SectionModelid);
                node.children.push(sectionnode);
            });

            maintree.push(node);
        });
        this.filesTree4 = maintree;
    }

    settreenode(ipdatalabel: any, ipdatavalue: any) {
        var opdata: TreeNode = {
            label: ipdatalabel,
            data: ipdatavalue,
            expanded: true,
            children: [],
        };
        return opdata;
    }
    CheckAdvancedRecords() {
        if (this.selectedFiles2 !== undefined && this.selectedFiles2.length !== 0) {


            var selectedsections = this.selectedFiles2.filter((a: TreeNode) => a.parent !== undefined);
            this.selectedStudents = [];
            var students: StudentModel[] = [];
            selectedsections.forEach((item: TreeNode) => {

                students = this.students.filter((a: StudentModel) => a.OfClass.ClassModelid === item.parent.data && a.OfSection.SectionModelid === item.data);
                this.selectedStudents = this.selectedStudents.concat(students);
            });
            this.displayDialog = false;
        }
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
         
            this.onSubmit(this.mainobj);
        }
    }

    showMessage(severity: any, errormsg: any) {
        this.msgs = [];
        this.msgs.push({ severity: severity, summary: 'Message', detail: errormsg });
    }

    

    checkrecordsclasses(event: any) {
        this.selectedStudents = this.students.filter(a => event.indexOf(a.ClassModelid) !== -1);
    }

    SendNotification() {

        

        if (confirm("Are you sure want to send notification to selected students ")) {

            if (!this.isSingle) {
                
                

                //let individualstudentids = this.selectedStudents.map(a => a.StudentModelID).toString();

                let notificationto: NotificationToClassSection = { typeofnotice: "Class", noticetoclasssection: [] };
                var selectedsections = this.selectedFiles2.filter((a: TreeNode) => a.parent !== undefined);
                console.log(selectedsections);
                selectedsections.forEach((item: TreeNode) => {
                    let classsection: ClassSectionRel = { ClassModelid: item.parent.data, SectionModelid: item.data, ClassModelName: item.parent.label, SectionModelName: item.label };
                    notificationto.noticetoclasssection.push(classsection);
                });

                let lobj: NotificationScheduleModel = {
                    NotificationScheduleModelid: null, notificationsentdate: null, isPublished: true, remarks: null,
                    BlogModelid: this.currentblogid, classname: JSON.stringify(notificationto).toString()
                };
                this.mainobj = lobj;
                this.dbops = DBOperation.create;
                this.onSubmit(this.mainobj);
            }
            else {
                

                let notificationto: NotificationToClassSection = { typeofnotice: "Class", noticetoclasssection: [] };
                let classsection: ClassSectionRel = {
                    ClassModelid: this.selectedclass, SectionModelid: this.selectedsection,
                    ClassModelName: this.classes.filter(c => c.value == this.selectedclass)[0].label, SectionModelName: this.filterdsectionslist.filter(c => c.value == this.selectedsection)[0].label
                };
                notificationto.noticetoclasssection.push(classsection);

                let lobj: NotificationScheduleModel = {
                    NotificationScheduleModelid: null, notificationsentdate: null, isPublished: true, remarks: null,
                    BlogModelid: this.currentblogid, classname: JSON.stringify(notificationto).toString()
                };
                this.mainobj = lobj;
                this.dbops = DBOperation.create;
                this.onSubmit(this.mainobj);
                this.selectedsection = undefined;
                //this._crudService.get(Global.BASE_USER_ENDPOINT + "TCSSRel/GetAllByLogedInUser")
                //    .subscribe(records => {
                //        var isteacherrecord = records.filter((a: any) => a.ClassModelid === this.selectedclass && a.SectionModelid === this.selectedsection
                //            && a.SubjectModelid === this.blogmodel.BlogSubType);
                //        console.log(isteacherrecord);
                //        if (isteacherrecord === null) {
                //            this.msg = "You are not eligible to send notification to class " + this.selectedclass + " - "
                //                + this.selectedsection + " of subject: " + this.blogmodel.BlogSubType;
                //            this.showError(this.msg);
                //            return;

                //        }
                //        else {


                //            let notificationto: NotificationToClassSection = { typeofnotice: "Class", noticetoclasssection: [] };
                //            let classsection: ClassSectionRel = { ClassModelid: this.selectedclass, SectionModelid: this.selectedsection };
                //            notificationto.noticetoclasssection.push(classsection);

                //            let lobj: NotificationScheduleModel = {
                //                NotificationScheduleModelid: null, notificationsentdate: null, isPublished: true, remarks: null,
                //                BlogModelid: this.currentblogid, classname: JSON.stringify(notificationto).toString()
                //            };
                //            this.mainobj = lobj;
                //            this.dbops = DBOperation.create;
                //            this.onSubmit(this.mainobj);
                //        }
                //    });

            }

            
        }

    }

    OnClassChange() {
       
        var classstudents = this.students.filter(a => a.ClassModelid === this.selectedclass);
        this.filterdsectionslist = [];
        this.selectedsection = null;
       
        let sectionsall = this._crudService.unique(classstudents, 'OfSection');
        let sectionslocal = this._crudService.removeDuplicates(sectionsall, 'SectionModelid');
        sectionslocal.forEach((e: any) => {
            let item: SelectItem = { label: e.SectionName, value: e.SectionModelid };
            this.filterdsectionslist.push(item);
        });
    }

    SelectSingleStudents() {
        

        this._crudService.get(Global.BASE_USER_ENDPOINT + "TCSSRel/GetAllByLogedInUser")
            .subscribe(records => {

                var isteacherrecord = records.filter((a: any) => a.ClassModelid === this.selectedclass && a.SectionModelid === this.selectedsection
                    && a.SubjectModelid === this.blogmodel.SubjectModelid);

                if (isteacherrecord.length === 0) {
                    this.msg = "You are not eligible to send notification to this class.";
                    this.showMessage(MessageSeverity.error, this.msg);

                }
                else {
                    this.selectedStudents = [];
                    this.selectedStudents = this.students.filter(a => a.ClassModelid === this.selectedclass && a.SectionModelid === this.selectedsection);

                    this.showMessage(MessageSeverity.success, this.selectedStudents.length+" student(s) selected.");

                }
            });
    }
    DeSelectSelectSingleStudents() {
        this.selectedStudents = [];
    }

    //CheckTeacherEligibilty() {
    //    this._crudService.get(Global.BASE_USER_ENDPOINT + "TCSSRel/GetAllByLogedInUser")
    //        .subscribe(records => {
    //            var isteacherrecord = records.filter((a: any) => a.ClassModelid === this.selectedclass && a.SectionModelid === this.selectedsection
    //                && a.SubjectModelid === this.blogmodel.BlogSubType);
    //            console.log(records);
    //            console.log(isteacherrecord);

    //            if (isteacherrecord !== null)
    //                return true;
    //            return false;
    //        });
        
    //}

   

    stringtojsondata(ipdata: any) {
        let opdatajson = JSON.parse(ipdata);
        let opdata = opdatajson.typeofnotice + " : ";
        opdatajson.noticetoclasssection.forEach((item: any) => {
            opdata += item.ClassModelName + " - " + item.SectionModelName+", ";
        });
        return opdata;

    }

   

}



