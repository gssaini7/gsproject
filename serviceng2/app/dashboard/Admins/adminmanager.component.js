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
var equals_validators_1 = require("../../Services/equals.validators");
var AdminManagerComponent = /** @class */ (function () {
    function AdminManagerComponent(fb, _crudService) {
        this.fb = fb;
        this._crudService = _crudService;
        this.msgs = [];
        this.indLoading = false;
        this.isNewForm = false;
        //filteritemtypes: SelectItem[];
        this.supportrolename = "Support";
    }
    AdminManagerComponent.prototype.ngOnInit = function () {
        this.InitilizeFormItems();
        this.LoadData();
    };
    AdminManagerComponent.prototype.InitilizeFormItems = function () {
        this.mainFrm = this.fb.group({
            UserID: [''],
            Mobile: ['', forms_1.Validators.required],
            Email: ['', forms_1.Validators.email],
            Password: [''],
            ConfirmPassword: [''],
            nameofuser: [''],
            remarks: [''],
        }, { validator: equals_validators_1.EqualsValidator.equals('Password', 'ConfirmPassword') });
    };
    AdminManagerComponent.prototype.LoadData = function () {
        var _this = this;
        this.indLoading = true;
        this.mainobjlist = null;
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "account/users?rolename=" + this.supportrolename)
            .subscribe(function (records) {
            _this.mainobjlist = records;
            _this.indLoading = false;
        }, function (error) { return _this.msg = error; });
    };
    AdminManagerComponent.prototype.onSubmit = function (formData) {
        var _this = this;
        this.msg = "";
        console.log(formData._value);
        switch (this.dbops) {
            case enum_1.DBOperation.create:
                var curl = global_1.Global.BASE_USER_ENDPOINT + "Account/Register";
                formData._value.UserRole = this.supportrolename;
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
                });
                break;
            case enum_1.DBOperation.update:
                var curl = global_1.Global.BASE_USER_ENDPOINT + "Account/UpdateUser";
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
                var curl = global_1.Global.BASE_USER_ENDPOINT + "item/Delete";
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
    AdminManagerComponent.prototype.SetControlsState = function (isEnable) {
        isEnable ? this.mainFrm.enable() : this.mainFrm.disable();
    };
    AdminManagerComponent.prototype.showDialogToAdd = function () {
        this.isNewForm = true;
        this.mainFrm.reset();
        this.displayDialog = true;
        this.dbops = enum_1.DBOperation.create;
    };
    AdminManagerComponent.prototype.save = function (formData) {
        this.SetControlsState(true);
        this.onSubmit(formData);
    };
    AdminManagerComponent.prototype.onRowSelect = function (event) {
        this.mainobj = event.data;
        this.mainFrm.patchValue(this.mainobj);
        this.displayDialog = true;
        this.dbops = enum_1.DBOperation.update;
        this.isNewForm = false;
    };
    AdminManagerComponent.prototype.delete = function () {
        if (confirm("Are you sure to delete ")) {
            this.dbops = enum_1.DBOperation.delete;
            this.onSubmit(this.mainFrm);
        }
    };
    AdminManagerComponent.prototype.showError = function (errormsg) {
        this.msgs = [];
        this.msgs.push({ severity: 'error', summary: 'Error Message', detail: errormsg });
    };
    AdminManagerComponent = __decorate([
        core_1.Component({
            //selector: 'app-register-form',
            templateUrl: 'app/dashboard/SupportAdmin/adminmanager.component.html',
        }),
        __metadata("design:paramtypes", [forms_1.FormBuilder, authcrud_service_1.AuthcrudService])
    ], AdminManagerComponent);
    return AdminManagerComponent;
}());
exports.AdminManagerComponent = AdminManagerComponent;
//export class AdminManagerComponent {
//  errors: string;
//  isRequesting: boolean;
//  credentials: newuser = { LoginUserName: '', Password: '', ConfirmPassword: '', Email: '', UserRole: ''  };
//  constructor(private userService: AuthcrudService) { }
//  createsupport({ value, valid }: { value: newuser, valid: boolean }) {
//    this.isRequesting = true;
//    this.errors = '';
//    if (valid) {
//        value.UserRole = "Support";
//        this.userService.post(Global.BASE_USER_ENDPOINT + "account/register", value)
//        .finally(() => this.isRequesting = false)
//        .subscribe(
//            result => {
//                if (result) { }
//            },
//            error => {
//            this.errors = error
//        });
//    }
//  }
//}
//# sourceMappingURL=adminmanager.component.js.map