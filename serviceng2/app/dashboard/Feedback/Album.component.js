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
var AlbumComponent = /** @class */ (function () {
    function AlbumComponent(fb, _crudService) {
        this.fb = fb;
        this._crudService = _crudService;
        this.msgs = [];
        this.indLoading = false;
        this.isNewForm = false;
        this.notify = new core_1.EventEmitter();
    }
    AlbumComponent.prototype.sendAlbum = function () {
        this.notify.emit(this.mainobj.AlbumModelid);
    };
    AlbumComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.InitilizeFormItems();
        this.LoadData();
        this.cmitems = [
            { label: 'View', icon: 'fa-search', command: function (event) { return _this.onRowSelect(_this.mainobj); } },
            { label: 'Edit', icon: 'fa-edit', command: function (event) { return _this.onRowEdit(_this.mainobj); } }
        ];
    };
    AlbumComponent.prototype.InitilizeFormItems = function () {
        this.mainFrm = this.fb.group({
            AlbumModelid: [''],
            AlbumName: [''],
            isPublished: [''],
            remarks: [''],
        });
    };
    AlbumComponent.prototype.LoadData = function () {
        var _this = this;
        this.indLoading = true;
        this.mainobjlist = null;
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "Album/GetAll")
            .subscribe(function (records) {
            _this.mainobjlist = records;
            _this.indLoading = false;
        }, function (error) { return _this.msg = error; });
    };
    AlbumComponent.prototype.onSubmit = function (formData) {
        var _this = this;
        this.msg = "";
        switch (this.dbops) {
            case enum_1.DBOperation.create:
                if (formData._value.isPublished == null || formData._value.isPublished == '')
                    formData._value.isPublished = false;
                var curl = global_1.Global.BASE_USER_ENDPOINT + "Album/Create";
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
                var curl = global_1.Global.BASE_USER_ENDPOINT + "Album/Edit";
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
                var curl = global_1.Global.BASE_USER_ENDPOINT + "Album/Delete";
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
    AlbumComponent.prototype.SetControlsState = function (isEnable) {
        isEnable ? this.mainFrm.enable() : this.mainFrm.disable();
    };
    AlbumComponent.prototype.showDialogToAdd = function () {
        this.isNewForm = true;
        this.mainFrm.reset();
        this.displayDialog = true;
        this.dbops = enum_1.DBOperation.create;
    };
    AlbumComponent.prototype.save = function (formData) {
        this.SetControlsState(true);
        this.onSubmit(formData);
    };
    AlbumComponent.prototype.onRowSelect = function (event) {
        this.sendAlbum();
        //this.mainobj = event.data;
        //this.mainFrm.patchValue(this.mainobj);
        //this.displayDialog = true;
        //this.dbops = DBOperation.update;
        //this.isNewForm = false;
    };
    AlbumComponent.prototype.onRowEdit = function (event) {
        this.mainobj = event;
        this.mainFrm.patchValue(this.mainobj);
        this.displayDialog = true;
        this.dbops = enum_1.DBOperation.update;
        this.isNewForm = false;
    };
    AlbumComponent.prototype.delete = function () {
        if (confirm("Are you sure to delete ")) {
            this.dbops = enum_1.DBOperation.delete;
            this.onSubmit(this.mainFrm);
        }
    };
    AlbumComponent.prototype.showError = function (errormsg) {
        this.msgs = [];
        this.msgs.push({ severity: 'error', summary: 'Error Message', detail: errormsg });
    };
    __decorate([
        core_1.Output(),
        __metadata("design:type", core_1.EventEmitter)
    ], AlbumComponent.prototype, "notify", void 0);
    AlbumComponent = __decorate([
        core_1.Component({
            selector: 'album-crud',
            templateUrl: 'app/dashboard/Album/Album.component.html',
        }),
        __metadata("design:paramtypes", [forms_1.FormBuilder, authcrud_service_1.AuthcrudService])
    ], AlbumComponent);
    return AlbumComponent;
}());
exports.AlbumComponent = AlbumComponent;
//# sourceMappingURL=Album.component.js.map