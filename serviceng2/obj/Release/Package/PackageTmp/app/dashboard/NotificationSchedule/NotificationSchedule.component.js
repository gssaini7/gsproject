"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var authcrud_service_1 = require("../../Services/authcrud.service");
var global_1 = require("../../Shared/global");
var forms_1 = require("@angular/forms");
var enum_1 = require("../../Shared/enum");
var sortby_pipe_1 = require("../../Shared/sortby.pipe");
var NotificationScheduleComponent = /** @class */ (function () {
    function NotificationScheduleComponent(fb, _crudService, route) {
        this.fb = fb;
        this._crudService = _crudService;
        this.route = route;
        this.msgs = [];
        this.indLoading = false;
        this.isNewForm = false;
        this.isSingle = false;
    }
    NotificationScheduleComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.subscribe(function (params) {
            _this.currentblogid = params['blogid']; // --> Name must match wanted parameter
            _this.selectedteacherid = params['teacherid']; // --> Name must match wanted parameter
        });
        this.InitilizeFormItems();
        this.LoadBlogData();
        this.LoadData();
        //this.LoadClasses();
        this.LoadStudents();
        this.cmitems = [
            { label: 'Delete', icon: 'fa-trash', command: function (event) { return _this.delete(); } },
        ];
        //this.TestFn();
    };
    //TestFn() {
    //    this._crudService.get(Global.BASE_USER_ENDPOINT + "appdetail/GetNotificationDetail")
    //        .subscribe(record => {
    //        },
    //        error => {
    //        });
    //}
    NotificationScheduleComponent.prototype.InitilizeFormItems = function () {
        this.mainFrm = this.fb.group({
            NotificationScheduleModelid: [''],
            notificationsentdate: [''],
            classname: [''],
            isPublished: [''],
            remarks: [''],
        });
    };
    NotificationScheduleComponent.prototype.LoadBlogData = function () {
        var _this = this;
        this.indLoading = true;
        this.blogmodel = null;
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "blog/Get?id=" + this.currentblogid)
            .subscribe(function (record) {
            _this.blogmodel = record;
            _this.dbcodeid = localStorage.getItem('dbcodeid');
            if (record.BlogType.BlogTypeProperty === "Assignment") {
                _this.isSingle = true;
                //this.LoadStudents();
            }
        }, function (error) {
        });
    };
    NotificationScheduleComponent.prototype.unique = function (arr, prop) {
        return arr.map(function (e) { return e[prop]; }).filter(function (e, i, a) {
            return i === a.indexOf(e);
        });
    };
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
    NotificationScheduleComponent.prototype.LoadStudents = function () {
        var _this = this;
        this.indLoading = true;
        this.students = null;
        this.classes = [];
        this.sections = [];
        this.classesfilter = [];
        this.sectionsfilter = [];
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "desk/GetStudentsByTeacher")
            .subscribe(function (records) {
            _this.students = records;
            var classesall = _this._crudService.unique(records, 'OfClass');
            var classeslocal = _this._crudService.removeDuplicates(classesall, 'ClassModelid');
            classeslocal.forEach(function (e) {
                var item = { label: e.ClassName, value: e.ClassModelid };
                _this.classes.push(item);
            });
            classeslocal.forEach(function (e) {
                var item = { label: e.ClassName, value: e.ClassName };
                _this.classesfilter.push(item);
            });
            var sectionsall = _this._crudService.unique(records, 'OfSection');
            var sectionslocal = _this._crudService.removeDuplicates(sectionsall, 'SectionModelid');
            sectionslocal.forEach(function (e) {
                var item = { label: e.SectionName, value: e.SectionModelid };
                _this.sections.push(item);
            });
            sectionslocal.forEach(function (e) {
                var item = { label: e.SectionName, value: e.SectionName };
                _this.sectionsfilter.push(item);
            });
            if (!_this.isSingle)
                _this.LoadAdvancedDialog();
        }, function (error) {
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
    };
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
    NotificationScheduleComponent.prototype.LoadData = function () {
        var _this = this;
        this.indLoading = true;
        this.mainobjlist = null;
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "NotificationSchedule/GetBlogSchedules?id=" + this.currentblogid)
            .subscribe(function (records) {
            _this.mainobjlist = records;
            var sortPipeFilter = new sortby_pipe_1.SortByPipe();
            sortPipeFilter.transform(records, 'notificationsentdate', true);
            _this.indLoading = false;
        }, function (error) { return _this.msg = error; });
    };
    NotificationScheduleComponent.prototype.onSubmit = function (formData) {
        var _this = this;
        this.msg = "";
        switch (this.dbops) {
            case enum_1.DBOperation.create:
                var curl = global_1.Global.BASE_USER_ENDPOINT + "NotificationSchedule/Create";
                this._crudService.post(curl, formData).subscribe(function (data) {
                    if (data == 1) {
                        _this.msg = "Data successfully added.";
                        _this.LoadData();
                        _this.selectedStudents = [];
                        if (_this.isSingle) {
                            _this.filterdsectionslist = [];
                            _this.selectedclass = null;
                        }
                        else
                            _this.selectedFiles2 = [];
                    }
                    else {
                        _this.msg = "There is some issue in saving records, please contact to system administrator!";
                    }
                    _this.displayDialog = false;
                }, function (error) {
                    _this.msg = error;
                    _this.showMessage(enum_1.MessageSeverity.error, _this.msg);
                });
                break;
            case enum_1.DBOperation.update:
                var curl = global_1.Global.BASE_USER_ENDPOINT + "NotificationSchedule/Edit";
                this._crudService.post(curl, formData._value).subscribe(function (data) {
                    if (data == 1) {
                        _this.msg = "Data successfully updated.";
                        _this.LoadData();
                    }
                    else {
                        _this.msg = "There is some issue in saving records, please contact to system administrator!";
                    }
                    _this.displayDialog = false;
                }, function (error) {
                    _this.msg = error;
                    _this.showMessage(enum_1.MessageSeverity.error, _this.msg);
                });
                break;
            case enum_1.DBOperation.delete:
                var curl = global_1.Global.BASE_USER_ENDPOINT + "NotificationSchedule/Delete";
                this._crudService.post(curl, formData).subscribe(function (data) {
                    if (data == 1) {
                        _this.msg = "Data successfully deleted.";
                        _this.LoadData();
                    }
                    else {
                        _this.msg = "There is some issue in deleting records, please contact to system administrator!";
                    }
                    _this.displayDialog = false;
                }, function (error) {
                    _this.msg = error;
                });
                break;
        }
    };
    NotificationScheduleComponent.prototype.SetControlsState = function (isEnable) {
        isEnable ? this.mainFrm.enable() : this.mainFrm.disable();
    };
    NotificationScheduleComponent.prototype.ShowAdvancedDialog = function () {
        this.displayDialog = true;
    };
    NotificationScheduleComponent.prototype.LoadAdvancedDialog = function () {
        var _this = this;
        var maintree = [];
        this.classes.forEach(function (item) {
            var node = _this.settreenode(item.label, item.value);
            var sectionsarray = _this._crudService.unique(_this.students.filter(function (a) { return a.OfClass.ClassModelid === item.value; }), 'OfSection');
            var sectionslocal = _this._crudService.removeDuplicates(sectionsarray, 'SectionModelid');
            sectionslocal.forEach(function (sectionitem) {
                var sectionnode = _this.settreenode(sectionitem.SectionName, sectionitem.SectionModelid);
                node.children.push(sectionnode);
            });
            maintree.push(node);
        });
        this.filesTree4 = maintree;
    };
    NotificationScheduleComponent.prototype.settreenode = function (ipdatalabel, ipdatavalue) {
        var opdata = {
            label: ipdatalabel,
            data: ipdatavalue,
            expanded: true,
            children: [],
        };
        return opdata;
    };
    NotificationScheduleComponent.prototype.CheckAdvancedRecords = function () {
        var _this = this;
        if (this.selectedFiles2 !== undefined && this.selectedFiles2.length !== 0) {
            var selectedsections = this.selectedFiles2.filter(function (a) { return a.parent !== undefined; });
            this.selectedStudents = [];
            var students = [];
            selectedsections.forEach(function (item) {
                students = _this.students.filter(function (a) { return a.OfClass.ClassModelid === item.parent.data && a.OfSection.SectionModelid === item.data; });
                _this.selectedStudents = _this.selectedStudents.concat(students);
            });
            this.displayDialog = false;
        }
    };
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
    NotificationScheduleComponent.prototype.onRowSelect = function (event) {
        this.mainobj = event.data;
        this.mainFrm.patchValue(this.mainobj);
        this.displayDialog = true;
        this.dbops = enum_1.DBOperation.update;
        this.isNewForm = false;
    };
    NotificationScheduleComponent.prototype.delete = function () {
        if (confirm("Are you sure to delete ")) {
            this.dbops = enum_1.DBOperation.delete;
            this.onSubmit(this.mainobj);
        }
    };
    NotificationScheduleComponent.prototype.showMessage = function (severity, errormsg) {
        this.msgs = [];
        this.msgs.push({ severity: severity, summary: 'Message', detail: errormsg });
    };
    NotificationScheduleComponent.prototype.checkrecordsclasses = function (event) {
        this.selectedStudents = this.students.filter(function (a) { return event.indexOf(a.ClassModelid) !== -1; });
    };
    NotificationScheduleComponent.prototype.SendNotification = function () {
        var _this = this;
        if (confirm("Are you sure want to send notification to selected students ")) {
            if (!this.isSingle) {
                //let individualstudentids = this.selectedStudents.map(a => a.StudentModelID).toString();
                var notificationto_1 = { typeofnotice: "Class", noticetoclasssection: [] };
                var selectedsections = this.selectedFiles2.filter(function (a) { return a.parent !== undefined; });
                console.log(selectedsections);
                selectedsections.forEach(function (item) {
                    var classsection = { ClassModelid: item.parent.data, SectionModelid: item.data, ClassModelName: item.parent.label, SectionModelName: item.label };
                    notificationto_1.noticetoclasssection.push(classsection);
                });
                var lobj = {
                    NotificationScheduleModelid: null, notificationsentdate: null, isPublished: true, remarks: null,
                    BlogModelid: this.currentblogid, classname: JSON.stringify(notificationto_1).toString()
                };
                this.mainobj = lobj;
                this.dbops = enum_1.DBOperation.create;
                this.onSubmit(this.mainobj);
            }
            else {
                var notificationto = { typeofnotice: "Class", noticetoclasssection: [] };
                var classsection = {
                    ClassModelid: this.selectedclass, SectionModelid: this.selectedsection,
                    ClassModelName: this.classes.filter(function (c) { return c.value == _this.selectedclass; })[0].label, SectionModelName: this.filterdsectionslist.filter(function (c) { return c.value == _this.selectedsection; })[0].label
                };
                notificationto.noticetoclasssection.push(classsection);
                var lobj = {
                    NotificationScheduleModelid: null, notificationsentdate: null, isPublished: true, remarks: null,
                    BlogModelid: this.currentblogid, classname: JSON.stringify(notificationto).toString()
                };
                this.mainobj = lobj;
                this.dbops = enum_1.DBOperation.create;
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
    };
    NotificationScheduleComponent.prototype.OnClassChange = function () {
        var _this = this;
        var classstudents = this.students.filter(function (a) { return a.ClassModelid === _this.selectedclass; });
        this.filterdsectionslist = [];
        this.selectedsection = null;
        var sectionsall = this._crudService.unique(classstudents, 'OfSection');
        var sectionslocal = this._crudService.removeDuplicates(sectionsall, 'SectionModelid');
        sectionslocal.forEach(function (e) {
            var item = { label: e.SectionName, value: e.SectionModelid };
            _this.filterdsectionslist.push(item);
        });
    };
    NotificationScheduleComponent.prototype.SelectSingleStudents = function () {
        var _this = this;
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "TCSSRel/GetAllByLogedInUser")
            .subscribe(function (records) {
            var isteacherrecord = records.filter(function (a) { return a.ClassModelid === _this.selectedclass && a.SectionModelid === _this.selectedsection
                && a.SubjectModelid === _this.blogmodel.SubjectModelid; });
            if (isteacherrecord.length === 0) {
                _this.msg = "You are not eligible to send notification to this class.";
                _this.showMessage(enum_1.MessageSeverity.error, _this.msg);
            }
            else {
                _this.selectedStudents = [];
                _this.selectedStudents = _this.students.filter(function (a) { return a.ClassModelid === _this.selectedclass && a.SectionModelid === _this.selectedsection; });
                _this.showMessage(enum_1.MessageSeverity.success, _this.selectedStudents.length + " student(s) selected.");
            }
        });
    };
    NotificationScheduleComponent.prototype.DeSelectSelectSingleStudents = function () {
        this.selectedStudents = [];
    };
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
    NotificationScheduleComponent.prototype.stringtojsondata = function (ipdata) {
        var opdatajson = JSON.parse(ipdata);
        var opdata = opdatajson.typeofnotice + " : ";
        opdatajson.noticetoclasssection.forEach(function (item) {
            opdata += item.ClassModelName + " - " + item.SectionModelName + ", ";
        });
        return opdata;
    };
    NotificationScheduleComponent = __decorate([
        core_1.Component({
            //selector: 'app-register-form',
            templateUrl: 'app/dashboard/NotificationSchedule/NotificationSchedule.component.html',
        }),
        __metadata("design:paramtypes", [forms_1.FormBuilder, authcrud_service_1.AuthcrudService, router_1.ActivatedRoute])
    ], NotificationScheduleComponent);
    return NotificationScheduleComponent;
}());
exports.NotificationScheduleComponent = NotificationScheduleComponent;
//# sourceMappingURL=NotificationSchedule.component.js.map