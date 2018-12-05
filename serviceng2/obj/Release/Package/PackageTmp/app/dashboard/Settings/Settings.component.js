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
var SettingsComponent = /** @class */ (function () {
    function SettingsComponent(fb, _crudService, route, router) {
        this.fb = fb;
        this._crudService = _crudService;
        this.route = route;
        this.router = router;
        this.msgs = [];
        this.indLoading = false;
        this.isNewForm = false;
    }
    SettingsComponent.prototype.ngOnInit = function () {
        this.InitilizeFormItems();
        this.LoadData();
    };
    SettingsComponent.prototype.InitilizeFormItems = function () {
        var _this = this;
        this.route.params.subscribe(function (params) {
            _this.settingstype = params['type']; // --> Settings type Name
        });
    };
    SettingsComponent.prototype.InitilizeFormItemsAfterLoad = function () {
        if (this.mainFrm !== undefined)
            this.mainFrm.reset();
        if (this.settingstype == "SMS") {
            this.mainFrm = this.fb.group({
                username: [''],
                msgtoken: [''],
                senderid: [''],
                apiurl: [''],
            });
        }
        else if (this.settingstype == "OnlinePayment") {
            this.mainFrm = this.fb.group({
                api_key: [''],
                salt: [''],
                url: [''],
            });
        }
        else if (this.settingstype == "Mail") {
            this.mainFrm = this.fb.group({
                username: [''],
                password: [''],
                smtp: [''],
                port: [''],
            });
        }
    };
    SettingsComponent.prototype.LoadData = function () {
        var _this = this;
        if (this.settingstype === undefined) {
            this.settingstype = "Mail";
        }
        this.InitilizeFormItemsAfterLoad();
        this.indLoading = true;
        this.mainobj = null;
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "Settings/Get?type=" + this.settingstype)
            .subscribe(function (records) {
            _this.mainobj = records;
            _this.mainFrm.patchValue(JSON.parse(records.SettingsContent));
            _this.indLoading = false;
        }, function (error) { return _this.msg = error; });
    };
    SettingsComponent.prototype.onSubmit = function (formData) {
        var _this = this;
        this.msg = "";
        switch (this.dbops) {
            case enum_1.DBOperation.create:
                var curl = global_1.Global.BASE_USER_ENDPOINT + "Settings/Create";
                this._crudService.post(curl, formData).subscribe(function (data) {
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
                var curl = global_1.Global.BASE_USER_ENDPOINT + "Settings/Edit";
                this._crudService.post(curl, formData).subscribe(function (data) {
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
    //SetControlsState(isEnable: boolean) {
    //    isEnable ? this.mainFrm.enable() : this.mainFrm.disable();
    //}
    //showDialogToAdd() {
    //    this.isNewForm = true;
    //    this.mainFrm.reset();
    //    this.displayDialog = true;
    //    this.dbops = DBOperation.create;
    //}
    SettingsComponent.prototype.save = function (formData) {
        //this.SetControlsState(true);
        this.dbops = enum_1.DBOperation.update;
        var datatosend = { SettingsModelid: this.mainobj.SettingsModelid, SettingsType: this.settingstype, SettingsContent: JSON.stringify(formData._value) };
        this.onSubmit(datatosend);
    };
    //onRowSelect(event: any) {
    //    this.mainobj = event.data;
    //    this.mainFrm.patchValue(this.mainobj);
    //    this.displayDialog = true;
    //    this.dbops = DBOperation.update;
    //    this.isNewForm = false;
    //}
    //delete() {
    //    if (confirm("Are you sure to delete ")) {
    //        this.dbops = DBOperation.delete;
    //        this.onSubmit(this.mainFrm);
    //    }
    //}
    SettingsComponent.prototype.changetab = function (selectedtype) {
        this.settingstype = selectedtype;
        this.LoadData();
        this.router.navigate(['/settings', this.settingstype]);
        this.msg = "";
    };
    SettingsComponent.prototype.showError = function (errormsg) {
        this.msgs = [];
        this.msgs.push({ severity: 'error', summary: 'Error Message', detail: errormsg });
    };
    SettingsComponent = __decorate([
        core_1.Component({
            //selector: 'app-register-form',
            templateUrl: 'app/dashboard/Settings/Settings.component.html',
        }),
        __metadata("design:paramtypes", [forms_1.FormBuilder, authcrud_service_1.AuthcrudService, router_1.ActivatedRoute, router_1.Router])
    ], SettingsComponent);
    return SettingsComponent;
}());
exports.SettingsComponent = SettingsComponent;
//# sourceMappingURL=Settings.component.js.map