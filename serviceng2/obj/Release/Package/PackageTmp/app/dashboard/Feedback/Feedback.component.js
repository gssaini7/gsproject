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
var authcrud_service_1 = require("../../Services/authcrud.service");
var global_1 = require("../../Shared/global");
var forms_1 = require("@angular/forms");
var enum_1 = require("../../Shared/enum");
var sortby_pipe_1 = require("../../Shared/sortby.pipe");
var FeedbackComponent = /** @class */ (function () {
    function FeedbackComponent(fb, _crudService) {
        this.fb = fb;
        this._crudService = _crudService;
        this.msgs = [];
        this.indLoading = false;
        this.isNewForm = false;
    }
    FeedbackComponent.prototype.ngOnInit = function () {
        //this.InitilizeFormItems();
        this.LoadData();
    };
    //InitilizeFormItems() {
    //    this.mainFrm = this.fb.group({
    //        FeedbackModelid: [''],
    //        FeedbackType: [''],
    //        StudentModelID: [''],
    //        FeedbackDate: [''],
    //        FeedbackMessage: [''],
    //        isPublished: [''],
    //        remarks: [''],
    //    });
    //}
    FeedbackComponent.prototype.LoadData = function () {
        var _this = this;
        this.indLoading = true;
        this.mainobjlist = null;
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "Feedback/GetAll")
            .subscribe(function (records) {
            _this.mainobjlist = records;
            var sortPipeFilter = new sortby_pipe_1.SortByPipe();
            sortPipeFilter.transform(records, 'FeedbackDate', true);
            _this.indLoading = false;
        }, function (error) { return _this.msg = error; });
    };
    FeedbackComponent.prototype.onSubmit = function (formData) {
        var _this = this;
        this.msg = "";
        switch (this.dbops) {
            case enum_1.DBOperation.create:
                if (formData._value.isPublished == null || formData._value.isPublished == '')
                    formData._value.isPublished = false;
                var curl = global_1.Global.BASE_USER_ENDPOINT + "Feedback/Create";
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
                var curl = global_1.Global.BASE_USER_ENDPOINT + "Feedback/Edit";
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
                var curl = global_1.Global.BASE_USER_ENDPOINT + "Feedback/Delete";
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
    FeedbackComponent.prototype.SetControlsState = function (isEnable) {
        isEnable ? this.mainFrm.enable() : this.mainFrm.disable();
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
    //onRowSelect(event: any) {
    //    this.mainobj = event.data;
    //    this.mainFrm.patchValue(this.mainobj);
    //    this.displayDialog = true;
    //    this.dbops = DBOperation.update;
    //    this.isNewForm = false;
    //}
    FeedbackComponent.prototype.delete = function () {
        if (confirm("Are you sure to delete ")) {
            this.dbops = enum_1.DBOperation.delete;
            this.onSubmit(this.mainFrm);
        }
    };
    FeedbackComponent.prototype.showError = function (errormsg) {
        this.msgs = [];
        this.msgs.push({ severity: 'error', summary: 'Error Message', detail: errormsg });
    };
    FeedbackComponent = __decorate([
        core_1.Component({
            //selector: 'app-register-form',
            templateUrl: 'app/dashboard/Feedback/Feedback.component.html',
        }),
        __metadata("design:paramtypes", [forms_1.FormBuilder, authcrud_service_1.AuthcrudService])
    ], FeedbackComponent);
    return FeedbackComponent;
}());
exports.FeedbackComponent = FeedbackComponent;
//# sourceMappingURL=Feedback.component.js.map