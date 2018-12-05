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
var ClientDetailComponent = /** @class */ (function () {
    function ClientDetailComponent(fb, _crudService) {
        this.fb = fb;
        this._crudService = _crudService;
        this.msgs = [];
        this.indLoading = false;
        this.isNewForm = false;
    }
    ClientDetailComponent.prototype.ngOnInit = function () {
        this.InitilizeFormItems();
        this.LoadData();
    };
    ClientDetailComponent.prototype.InitilizeFormItems = function () {
        this.mainFrm = this.fb.group({
            ClientDetailModelid: [''],
            productname: ['', forms_1.Validators.required],
            productid: ['', forms_1.Validators.required],
            businessname: ['', forms_1.Validators.required],
            clientaddress: ['', forms_1.Validators.required],
            expirydate: ['', forms_1.Validators.required],
            Mobile: [''],
            Email: [''],
            isPublished: [''],
            remarks: [''],
        });
        this.currentDate = new Date();
        this.ExpiryItems = [];
        this.ExpiryItems.push({ label: 'All Records', value: null });
        this.ExpiryItems.push({ label: 'Expired', value: 'true' });
    };
    ClientDetailComponent.prototype.LoadData = function () {
        var _this = this;
        this.indLoading = true;
        this.mainobjlist = null;
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "client/GetAll")
            .subscribe(function (records) {
            _this.mainobjlist = records;
            _this.indLoading = false;
        }, function (error) { return _this.msg = error; });
    };
    ClientDetailComponent.prototype.onSubmit = function (formData) {
        var _this = this;
        this.msg = "";
        console.log(formData._value);
        switch (this.dbops) {
            case enum_1.DBOperation.create:
                var curl = global_1.Global.BASE_USER_ENDPOINT + "client/Create";
                if (formData._value.isPublished == null)
                    formData._value.isPublished = false;
                console.log(formData._value);
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
                var curl = global_1.Global.BASE_USER_ENDPOINT + "client/Edit";
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
        }
    };
    ClientDetailComponent.prototype.SetControlsState = function (isEnable) {
        isEnable ? this.mainFrm.enable() : this.mainFrm.disable();
    };
    ClientDetailComponent.prototype.showDialogToAdd = function () {
        this.isNewForm = true;
        this.mainFrm.reset();
        this.displayDialog = true;
        this.dbops = enum_1.DBOperation.create;
    };
    ClientDetailComponent.prototype.save = function (formData) {
        this.SetControlsState(true);
        this.onSubmit(formData);
    };
    ClientDetailComponent.prototype.onRowSelect = function (event) {
        this.mainobj = event.data.ClientDetailModel;
        //var d = new Date(this.mainobj.expirydate);
        //d = new Date(d.getTime() - d.getTimezoneOffset() * 60000)
        //this.mainobj.expirydate = d;
        this.mainobj.expirydate = this.StringToDate(this.mainobj.expirydate);
        this.mainFrm.patchValue(this.mainobj);
        this.displayDialog = true;
        this.dbops = enum_1.DBOperation.update;
        this.isNewForm = false;
    };
    ClientDetailComponent.prototype.delete = function () {
        if (confirm("Are you sure to delete ")) {
            this.dbops = enum_1.DBOperation.delete;
            this.onSubmit(this.mainFrm);
        }
    };
    ClientDetailComponent.prototype.StringToDate = function (strdate) {
        var d = new Date(strdate);
        d = new Date(d.getTime() - d.getTimezoneOffset() * 60000);
        return d;
    };
    ClientDetailComponent.prototype.showError = function (errormsg) {
        this.msgs = [];
        this.msgs.push({ severity: 'error', summary: 'Error Message', detail: errormsg });
    };
    ClientDetailComponent = __decorate([
        core_1.Component({
            //selector: 'app-register-form',
            templateUrl: 'app/dashboard/ClientDetail/ClientDetail.component.html',
        }),
        __metadata("design:paramtypes", [forms_1.FormBuilder, authcrud_service_1.AuthcrudService])
    ], ClientDetailComponent);
    return ClientDetailComponent;
}());
exports.ClientDetailComponent = ClientDetailComponent;
//# sourceMappingURL=ClientDetail.component.js.map