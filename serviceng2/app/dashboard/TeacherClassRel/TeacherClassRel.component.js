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
var TCSSRelComponent = /** @class */ (function () {
    function TCSSRelComponent(fb, _crudService, route) {
        this.fb = fb;
        this._crudService = _crudService;
        this.route = route;
        this.msgs = [];
        this.indLoading = false;
        this.isNewForm = false;
    }
    TCSSRelComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.InitilizeFormItems();
        this.route.params.subscribe(function (params) {
            _this.selecteduserid = params['adminid']; // --> Name must match wanted parameter
        });
        this.LoadData();
    };
    TCSSRelComponent.prototype.InitilizeFormItems = function () {
        this.mainFrm = this.fb.group({
            TCSSRelModelid: [''],
            Teacherid: [''],
            ClassModelid: [''],
            SectionModelid: [''],
            SubjectModelid: [''],
            isPublished: [''],
            remarks: [''],
            ForClass: ['', forms_1.Validators.required],
            ForSection: ['', forms_1.Validators.required],
            ForSubject: ['', forms_1.Validators.required],
        });
    };
    TCSSRelComponent.prototype.LoadData = function () {
        var _this = this;
        this.indLoading = true;
        this.mainobjlist = null;
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "TCSSRel/GetAll?id=" + this.selecteduserid)
            .subscribe(function (records) {
            _this.mainobjlist = records;
            _this.indLoading = false;
        }, function (error) { return _this.msg = error; });
    };
    TCSSRelComponent.prototype.LoadClasses = function () {
        var _this = this;
        this.indLoading = true;
        this.classes = null;
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "desk/GetAllClasses")
            .subscribe(function (records) {
            _this.classes = records;
            _this.indLoading = false;
        }, function (error) { return _this.msg = error; });
    };
    TCSSRelComponent.prototype.LoadSections = function () {
        var _this = this;
        this.indLoading = true;
        this.sections = null;
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "desk/GetAllSections")
            .subscribe(function (records) {
            _this.sections = records;
            _this.indLoading = false;
        }, function (error) { return _this.msg = error; });
    };
    TCSSRelComponent.prototype.LoadSubjects = function () {
        var _this = this;
        this.indLoading = true;
        this.subjects = null;
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "desk/GetAllSubjects")
            .subscribe(function (records) {
            _this.subjects = records;
            _this.indLoading = false;
        }, function (error) { return _this.msg = error; });
    };
    TCSSRelComponent.prototype.onSubmit = function (formData) {
        var _this = this;
        this.msg = "";
        switch (this.dbops) {
            case enum_1.DBOperation.create:
                if (formData._value.isPublished == null || formData._value.isPublished == '')
                    formData._value.isPublished = false;
                formData._value.Teacherid = this.selecteduserid;
                var curl = global_1.Global.BASE_USER_ENDPOINT + "TCSSRel/Create";
                this._crudService.post(curl, formData._value).subscribe(function (data) {
                    if (data == 1) {
                        _this.msg = "Data successfully added.";
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
            case enum_1.DBOperation.update:
                var curl = global_1.Global.BASE_USER_ENDPOINT + "TCSSRel/Edit";
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
                var curl = global_1.Global.BASE_USER_ENDPOINT + "TCSSRel/Delete";
                this._crudService.post(curl, formData._value).subscribe(function (data) {
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
    TCSSRelComponent.prototype.SetControlsState = function (isEnable) {
        isEnable ? this.mainFrm.enable() : this.mainFrm.disable();
    };
    TCSSRelComponent.prototype.showDialogToAdd = function () {
        this.isNewForm = true;
        this.mainFrm.reset();
        this.displayDialog = true;
        this.dbops = enum_1.DBOperation.create;
        this.LoadClasses();
        this.LoadSections();
        this.LoadSubjects();
    };
    TCSSRelComponent.prototype.save = function (formData) {
        this.SetControlsState(true);
        //formData._value.ClassModelid = formData._value.ClassModelid.ClassModelid;
        //formData._value.SectionModelid = formData._value.SectionModelid.SectionModelid;
        //formData._value.SubjectModelid = formData._value.SubjectModelid.SubjectModelid;
        this.onSubmit(formData);
    };
    TCSSRelComponent.prototype.onRowSelect = function (event) {
        this.mainobj = event.data;
        //this.mainobj.ClassModelid = { ClassModelid: event.data.ClassModelid };
        //this.mainobj.SectionModelid = { SectionModelid: event.data.SectionModelid };
        //this.mainobj.SubjectModelid = { SubjectModelid: event.data.SubjectModelid };
        this.mainFrm.patchValue(this.mainobj);
        this.displayDialog = true;
        this.dbops = enum_1.DBOperation.update;
        this.isNewForm = false;
        this.LoadClasses();
        this.LoadSections();
        this.LoadSubjects();
    };
    TCSSRelComponent.prototype.delete = function () {
        if (confirm("Are you sure to delete ")) {
            this.dbops = enum_1.DBOperation.delete;
            this.onSubmit(this.mainFrm);
        }
    };
    TCSSRelComponent.prototype.showError = function (errormsg) {
        this.msgs = [];
        this.msgs.push({ severity: 'error', summary: 'Error Message', detail: errormsg });
    };
    TCSSRelComponent = __decorate([
        core_1.Component({
            //selector: 'app-register-form',
            templateUrl: 'app/dashboard/TeacherClassRel/TeacherClassRel.component.html',
        }),
        __metadata("design:paramtypes", [forms_1.FormBuilder, authcrud_service_1.AuthcrudService, router_1.ActivatedRoute])
    ], TCSSRelComponent);
    return TCSSRelComponent;
}());
exports.TCSSRelComponent = TCSSRelComponent;
//# sourceMappingURL=TeacherClassRel.component.js.map