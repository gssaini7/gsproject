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
var BlogComponent = /** @class */ (function () {
    function BlogComponent(fb, _crudService) {
        this.fb = fb;
        this._crudService = _crudService;
        this.msgs = [];
        this.indLoading = false;
        this.isNewForm = false;
        this.myBlogType = "Blog";
        this.ckEditorConfig = {
            "toolbarGroups": [
                { "name": "document", "groups": ["basicstyles"] },
            ],
            "removeButtons": "Source,Save,Templates,Find,Replace,Scayt,SelectAll"
        };
    }
    BlogComponent.prototype.ngOnInit = function () {
        this.InitilizeFormItems();
        this.LoadData();
    };
    BlogComponent.prototype.InitilizeFormItems = function () {
        this.mainFrm = this.fb.group({
            NotificationModelid: [''],
            title: [''],
            description: [''],
            imagepath: [''],
            isPublished: [''],
            remarks: [''],
            BlogTypeName: [''],
        });
    };
    BlogComponent.prototype.LoadData = function () {
        var _this = this;
        this.indLoading = true;
        this.mainobjlist = null;
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "blog/GetAll?id=" + this.myBlogType)
            .subscribe(function (records) {
            _this.mainobjlist = records;
            _this.dbcodeid = localStorage.getItem('dbcodeid');
            _this.indLoading = false;
        }, function (error) { return _this.msg = error; });
    };
    BlogComponent.prototype.onSubmit = function (formData) {
        var _this = this;
        this.msg = "";
        switch (this.dbops) {
            case enum_1.DBOperation.create:
                if (formData._value.isPublished == null || formData._value.isPublished == '')
                    formData._value.isPublished = false;
                formData._value.BlogTypeName = this.myBlogType;
                var curl = global_1.Global.BASE_USER_ENDPOINT + "blog/Create";
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
                var curl = global_1.Global.BASE_USER_ENDPOINT + "blog/Edit";
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
                var curl = global_1.Global.BASE_USER_ENDPOINT + "blog/Delete";
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
            case enum_1.DBOperation.deleteimage:
                var curl = global_1.Global.BASE_USER_ENDPOINT + "blog/Deleteimage";
                this._crudService.post(curl, formData).subscribe(function (data) {
                    if (data == 1) {
                        _this.msg = "Image successfully deleted.";
                        _this.LoadData();
                    }
                    else {
                        _this.msg = "There is some issue in deleting records, please contact to system administrator!";
                    }
                }, function (error) {
                    _this.msg = error;
                });
                break;
        }
    };
    BlogComponent.prototype.SetControlsState = function (isEnable) {
        isEnable ? this.mainFrm.enable() : this.mainFrm.disable();
    };
    BlogComponent.prototype.showDialogToAdd = function () {
        this.isNewForm = true;
        this.mainFrm.reset();
        this.displayDialog = true;
        this.dbops = enum_1.DBOperation.create;
    };
    BlogComponent.prototype.save = function (formData) {
        this.SetControlsState(true);
        this.onSubmit(formData);
    };
    BlogComponent.prototype.onRowSelect = function (event) {
        this.mainobj = event.data;
        this.mainFrm.patchValue(this.mainobj);
        console.log(event);
        this.displayDialog = true;
        this.dbops = enum_1.DBOperation.update;
        this.isNewForm = false;
    };
    BlogComponent.prototype.delete = function () {
        if (confirm("Are you sure to delete ")) {
            this.dbops = enum_1.DBOperation.delete;
            this.onSubmit(this.mainFrm);
        }
    };
    BlogComponent.prototype.showError = function (errormsg) {
        this.msgs = [];
        this.msgs.push({ severity: 'error', summary: 'Error Message', detail: errormsg });
    };
    BlogComponent.prototype.deleteimage = function (formData) {
        if (confirm("Are you sure to delete?")) {
            this.dbops = enum_1.DBOperation.deleteimage;
            this.onSubmit(formData);
        }
    };
    BlogComponent.prototype.onUpload = function (event) {
        for (var _i = 0, _a = event.files; _i < _a.length; _i++) {
            var file = _a[_i];
            this.msg = "Image saved.";
            this.LoadData();
        }
    };
    BlogComponent.prototype.onBeforeSend = function (event, itemid) {
        //event.xhr.setRequestHeader(this._crudService.setheader());
        event.formData.append('NotificationModelid', itemid);
        event.formData.append('dbcodeid', this.dbcodeid);
    };
    __decorate([
        core_1.Input(),
        __metadata("design:type", String)
    ], BlogComponent.prototype, "myBlogType", void 0);
    BlogComponent = __decorate([
        core_1.Component({
            selector: 'app-blog-crud',
            templateUrl: 'app/dashboard/Blog/Blog.component.html',
        }),
        __metadata("design:paramtypes", [forms_1.FormBuilder, authcrud_service_1.AuthcrudService])
    ], BlogComponent);
    return BlogComponent;
}());
exports.BlogComponent = BlogComponent;
//# sourceMappingURL=Blog.component.js.map