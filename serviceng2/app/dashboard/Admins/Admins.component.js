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
var AdminsComponent = /** @class */ (function () {
    function AdminsComponent(fb, _crudService, router) {
        this.fb = fb;
        this._crudService = _crudService;
        this.router = router;
        this.msgs = [];
        this.indLoading = false;
        this.isNewForm = false;
        this.isnewuser = false;
        this.selectedRoles = [];
    }
    AdminsComponent.prototype.ngOnInit = function () {
        this.InitilizeFormItems();
        this.LoadData();
    };
    AdminsComponent.prototype.InitilizeFormItems = function () {
        this.mainFrm = this.fb.group({
            nameofuser: ['', forms_1.Validators.required],
            Mobile: ['', forms_1.Validators.required],
            Email: ['', forms_1.Validators.required],
            remarks: [''],
            parent: [''],
            UserID: [''],
        });
        //this.itemsmi = [
        //    { label: 'Add Child', icon: 'fa-plus', command: (event) => this.showDialogToAdd() },
        //    { label: 'View', icon: 'fa-search', command: (event) => this.onRowSelect(this.selectedNode.data), disabled: this.isFirstUser },
        //    { label: 'Roles', icon: 'fa-user-plus', command: (event) => this.showDialogRoles(), disabled: this.isFirstUser },
        //    //{ label: 'Delete', icon: 'fa-close', command: (event) => ""},
        //];
    };
    AdminsComponent.prototype.LoadData = function () {
        var _this = this;
        this.indLoading = true;
        this.mainobjlist = null;
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "Admin/Admins")
            .subscribe(function (records) {
            _this.mainobjlist = records;
            _this.setformattreedata();
            _this.indLoading = false;
        }, function (error) { return _this.msg = error; });
    };
    //settreenode(ipdata: any) {
    //    var opdata: TreeNode = {
    //        label: ipdata.nameofuser,
    //        expanded: true,
    //        data: { 'email': ipdata.Email, 'mobile': ipdata.Mobile },
    //        children:[],
    //    };
    //    return opdata;
    //}
    AdminsComponent.prototype.settreenode = function (ipdata) {
        var opdata = {
            label: ipdata.nameofuser,
            //expanded: true,
            data: ipdata,
        };
        return opdata;
    };
    AdminsComponent.prototype.loopthroughrecords = function (records) {
        var _this = this;
        var maintree = [];
        records.forEach(function (item) {
            var node = _this.settreenode(item);
            if (item.AdminChildren != null) {
                var childnodes = _this.loopthroughrecords(item.AdminChildren);
                node.children = childnodes;
                node.expanded = true;
            }
            maintree.push(node);
        });
        //maintree.push(this.addtreenode());
        return maintree;
    };
    AdminsComponent.prototype.setformattreedata = function () {
        var records = this.mainobjlist;
        var bb = this.loopthroughrecords(records);
        this.data1 = bb;
    };
    AdminsComponent.prototype.onSubmit = function (formData) {
        var _this = this;
        this.msg = "";
        switch (this.dbops) {
            case enum_1.DBOperation.create:
                var curl = global_1.Global.BASE_USER_ENDPOINT + "Admin/Create";
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
                    _this.showError('error', _this.msg);
                });
                break;
            case enum_1.DBOperation.update:
                var curl = global_1.Global.BASE_USER_ENDPOINT + "Admin/UpdateUser";
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
                    _this.showError('error', _this.msg);
                });
                break;
            case enum_1.DBOperation.delete:
                var curl = global_1.Global.BASE_USER_ENDPOINT + "Admin/Delete";
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
                    _this.showError('error', _this.msg);
                });
                break;
        }
    };
    AdminsComponent.prototype.SetControlsState = function (isEnable) {
        isEnable ? this.mainFrm.enable() : this.mainFrm.disable();
    };
    AdminsComponent.prototype.showDialogToAdd = function () {
        this.isNewForm = true;
        this.mainFrm.reset();
        this.displayDialog = true;
        this.isnewuser = true;
        this.dbops = enum_1.DBOperation.create;
    };
    AdminsComponent.prototype.save = function (formData) {
        this.SetControlsState(true);
        formData._value.parentid = this.selectedNode.data.UserID;
        this.onSubmit(formData);
    };
    AdminsComponent.prototype.onRowSelect = function (event) {
        this.mainobj = event;
        this.mainFrm.patchValue(this.mainobj);
        this.displayDialog = true;
        this.isnewuser = true;
        this.dbops = enum_1.DBOperation.update;
        this.isNewForm = false;
    };
    AdminsComponent.prototype.nodeSelect = function (event) {
        //this.onRowSelect(event.node.data);
    };
    AdminsComponent.prototype.nodeRightClick = function (event) {
        var _this = this;
        var firstuser = this.mainobjlist[0];
        var firstdisabled = false;
        //this.isFirstUser = false;
        if (event.node.data.UserID === firstuser.UserID) {
            firstdisabled = true;
            var ismainadmin = sessionStorage.getItem('isLegitimate');
            if (ismainadmin === "true") {
                firstdisabled = false;
            }
        }
        this.itemsmi = [
            { label: 'Add Child', icon: 'fa-user-plus', command: function (event) { return _this.showDialogToAdd(); } },
            { label: 'View', icon: 'fa-search', command: function (event) { return _this.onRowSelect(_this.selectedNode.data); }, disabled: firstdisabled },
            { label: 'Roles', icon: 'fa-user-secret', command: function (event) { return _this.showDialogRoles(); }, disabled: firstdisabled },
            { label: 'Assign Class', icon: 'fa-address-card-o', command: function (event) { return _this.AdminDetail(_this.selectedNode.data); }, disabled: firstdisabled },
        ];
    };
    AdminsComponent.prototype.delete = function () {
        if (confirm("Are you sure to delete? All the child users also be deleted.")) {
            this.dbops = enum_1.DBOperation.delete;
            this.onSubmit(this.mainFrm);
        }
    };
    AdminsComponent.prototype.AdminDetail = function (data) {
        this.router.navigate(['/admindetail', data.UserID]);
    };
    AdminsComponent.prototype.showDialogRoles = function () {
        this.displayDialog = true;
        this.isnewuser = false;
        this.LoadDataRoles();
    };
    AdminsComponent.prototype.LoadDataRoles = function () {
        var _this = this;
        this.indLoading = true;
        this.roleslist = null;
        this.selectedRoles = null;
        if (this.selectedNode.data.parentid !== "00000000-0000-0000-0000-000000000000") {
            this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "Admin/GetRolesByID?id=" + this.selectedNode.data.parentid)
                .subscribe(function (records) {
                _this.roleslist = records;
                _this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "Admin/GetRolesByID?id=" + _this.selectedNode.data.UserID)
                    .subscribe(function (records) {
                    _this.selectedRoles = records;
                    _this.indLoading = false;
                }, function (error) { return _this.msg = error; });
                _this.indLoading = false;
            }, function (error) { return _this.msg = error; });
        }
        else {
            this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "Admin/GetRoles")
                .subscribe(function (records) {
                _this.roleslist = records.map(function (a) { return a.Name; });
                _this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "Admin/GetRolesByID?id=" + _this.selectedNode.data.UserID)
                    .subscribe(function (records) {
                    _this.selectedRoles = records;
                    _this.indLoading = false;
                }, function (error) { return _this.msg = error; });
                _this.indLoading = false;
            }, function (error) { return _this.msg = error; });
        }
    };
    AdminsComponent.prototype.updateroles = function () {
        var _this = this;
        var formData = {
            UserID: this.selectedNode.data.UserID,
            Rolenames: this.selectedRoles
        };
        var curl = global_1.Global.BASE_USER_ENDPOINT + "Admin/AssignRole";
        this._crudService.post(curl, formData).subscribe(function (data) {
            if (data == 1) {
                _this.msg = "Data successfully added.";
            }
            else {
                _this.msg = "There is some issue in saving records, please contact to system administrator!";
            }
            _this.displayDialog = false;
        }, function (error) {
            _this.msg = error;
            _this.showError('error', _this.msg);
        });
    };
    AdminsComponent.prototype.showError = function (severity, errormsg) {
        this.msgs = [];
        this.msgs.push({ severity: severity, summary: 'Message', detail: errormsg });
    };
    AdminsComponent = __decorate([
        core_1.Component({
            //selector: 'app-register-form',
            templateUrl: 'app/dashboard/Admins/Admins.component.html',
        }),
        __metadata("design:paramtypes", [forms_1.FormBuilder, authcrud_service_1.AuthcrudService, router_1.Router])
    ], AdminsComponent);
    return AdminsComponent;
}());
exports.AdminsComponent = AdminsComponent;
//# sourceMappingURL=Admins.component.js.map