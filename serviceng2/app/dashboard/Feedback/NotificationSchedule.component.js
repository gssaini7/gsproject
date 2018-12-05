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
        }, function (error) {
        });
    };
    NotificationScheduleComponent.prototype.unique = function (arr, prop) {
        return arr.map(function (e) { return e[prop]; }).filter(function (e, i, a) {
            return i === a.indexOf(e);
        });
    };
    NotificationScheduleComponent.prototype.LoadStudents = function () {
        var _this = this;
        this.indLoading = true;
        this.students = null;
        this.classes = [];
        this.sections = [];
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "desk/GetStudentsByTeacher")
            .subscribe(function (records) {
            _this.students = records;
            var classeslocal = _this.unique(records, 'strClass');
            classeslocal.forEach(function (e) {
                var item = { label: e, value: e };
                _this.classes.push(item);
            });
            var sectionslocal = _this.unique(records, 'SectionModelid');
            sectionslocal.forEach(function (e) {
                var item = { label: e, value: e };
                _this.sections.push(item);
            });
            _this.LoadAdvancedDialog();
        }, function (error) {
        });
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
                        _this.selectedStudents = null;
                        _this.selectedFiles2 = [];
                    }
                    else {
                        _this.msg = "There is some issue in saving records, please contact to system administrator!";
                    }
                    _this.displayDialog = false;
                }, function (error) {
                    _this.msg = error;
                    _this.showError(_this.msg);
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
                    _this.showError(_this.msg);
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
            var node = _this.settreenode(item.value);
            var sectionslocal = _this.unique(_this.students.filter(function (a) { return a.strClass === item.value; }), 'SectionModelid');
            sectionslocal.forEach(function (sectionitem) {
                var sectionnode = _this.settreenode(sectionitem);
                node.children.push(sectionnode);
            });
            maintree.push(node);
        });
        this.filesTree4 = maintree;
    };
    NotificationScheduleComponent.prototype.settreenode = function (ipdata) {
        var opdata = {
            label: ipdata,
            data: ipdata,
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
                students = _this.students.filter(function (a) { return a.strClass === item.parent.label && a.SectionModelid === item.label; });
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
    NotificationScheduleComponent.prototype.showError = function (errormsg) {
        this.msgs = [];
        this.msgs.push({ severity: 'error', summary: 'Error Message', detail: errormsg });
    };
    NotificationScheduleComponent.prototype.checkrecordsclasses = function (event) {
        this.selectedStudents = this.students.filter(function (a) { return event.indexOf(a.strClass) !== -1; });
    };
    NotificationScheduleComponent.prototype.SendNotification = function () {
        if (confirm("Are you sure want to send notification to selected students ")) {
            this.dbops = enum_1.DBOperation.create;
            var individualstudentids = this.selectedStudents.map(function (a) { return a.StudentModelID; }).toString();
            var notificationto_1 = { typeofnotice: "Class", noticetoclasssection: [] };
            var selectedsections = this.selectedFiles2.filter(function (a) { return a.parent !== undefined; });
            selectedsections.forEach(function (item) {
                var classsection = { ClassModelid: item.parent.label, SectionModelid: item.label };
                notificationto_1.noticetoclasssection.push(classsection);
            });
            var lobj = {
                NotificationScheduleModelid: null, notificationsentdate: null, isPublished: true, remarks: null,
                BlogModelid: this.currentblogid, classname: JSON.stringify(notificationto_1).toString()
            };
            this.mainobj = lobj;
            this.onSubmit(this.mainobj);
        }
    };
    NotificationScheduleComponent.prototype.stringtojsondata = function (ipdata) {
        var opdatajson = JSON.parse(ipdata);
        var opdata = opdatajson.typeofnotice + " : ";
        opdatajson.noticetoclasssection.forEach(function (item) {
            opdata += item.ClassModelid + " - " + item.SectionModelid + ", ";
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