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
var authcrud_service_1 = require("../../../Services/authcrud.service");
var global_1 = require("../../../Shared/global");
var forms_1 = require("@angular/forms");
var enum_1 = require("../../../Shared/enum");
var DetailTypeComponent = /** @class */ (function () {
    function DetailTypeComponent(fb, _crudService, route, router) {
        this.fb = fb;
        this._crudService = _crudService;
        this.route = route;
        this.router = router;
        this.msgs = [];
        this.indLoading = false;
        this.isNewForm = false;
        this.ckEditorConfig = {
            "toolbarGroups": [
                { "name": "document", "groups": ["basicstyles"] },
            ],
            "removeButtons": "Source,Save,Templates,Find,Replace,Scayt,SelectAll",
            extraPlugins: 'divarea'
        };
    }
    DetailTypeComponent.prototype.ngOnInit = function () {
        this.InitilizeFormItems();
        this.LoadBlogType();
        //this.LoadData();
    };
    DetailTypeComponent.prototype.InitilizeFormItems = function () {
        var _this = this;
        this.mainFrm = this.fb.group({
            BlogModelid: [''],
            title: ['', forms_1.Validators.required],
            description: ['', forms_1.Validators.required],
            imagepath: [''],
            isPublished: [''],
            remarks: [''],
            BlogTypeName: [''],
            filetype: [''],
            SubjectModelid: [''],
        });
        this.route.params.subscribe(function (params) {
            _this.currentid = params['id']; // --> Blog ID
            _this.myBlogType = params['type']; // --> BlogType Name
            _this.myBlogTypeId = params['typeid']; // --> BlogType ID
        });
    };
    DetailTypeComponent.prototype.LoadBlogType = function () {
        var _this = this;
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "BlogType/Get?id=" + this.myBlogTypeId)
            .subscribe(function (records) {
            if (records.BlogTypeProperty === "Assignment") {
                _this.LoadSubjects();
                _this.mainFrm.controls['SubjectModelid'].validator = forms_1.Validators.required;
            }
            else
                _this.LoadData();
        }, function (error) { return _this.msg = error; });
        //this.subjects = [];
        //let item: SelectItem = { label: "English", value: "English" };
        //this.subjects.push(item);
        //let item2: SelectItem = { label: "Computer", value: "Computer" };
        //this.subjects.push(item2);
    };
    DetailTypeComponent.prototype.LoadData = function () {
        var _this = this;
        if (this.currentid === "new") {
            //this.LoadBlogType();
            this.dbops = enum_1.DBOperation.create;
            this.mainFrm.reset();
            return;
        }
        this.indLoading = true;
        this.mainobjlist = null;
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "blog/Get?id=" + this.currentid)
            .subscribe(function (records) {
            _this.mainFrm.patchValue(records);
            _this.dbops = enum_1.DBOperation.update;
            //this.mainobjlist = records;
            _this.dbcodeid = localStorage.getItem('dbcodeid');
            //this.indLoading = false;
        }, function (error) {
            _this.router.navigate(['/blog', _this.myBlogType]);
            //this.msg = <any>error
        });
    };
    DetailTypeComponent.prototype.LoadSubjects = function () {
        var _this = this;
        this.indLoading = true;
        this.subjects = [];
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "TCSSRel/GetAllByLogedInUser")
            .subscribe(function (records) {
            var subjectsarray = _this._crudService.unique(records, 'ForSubject');
            var subjectslocal = _this._crudService.removeDuplicates(subjectsarray, 'SubjectModelid');
            subjectslocal.forEach(function (e) {
                var item = { label: e.SubjectName, value: e.SubjectModelid };
                _this.subjects.push(item);
            });
            _this.LoadData();
        }, function (error) {
            _this.LoadData();
        });
    };
    DetailTypeComponent.prototype.onSubmit = function (formData) {
        var _this = this;
        this.msg = "";
        switch (this.dbops) {
            case enum_1.DBOperation.create:
                if (formData._value.isPublished == null || formData._value.isPublished == '')
                    formData._value.isPublished = false;
                formData._value.BlogTypeModelid = this.myBlogTypeId;
                var curl = global_1.Global.BASE_USER_ENDPOINT + "blog/Create";
                this._crudService.postwithresponse(curl, formData._value).subscribe(function (data) {
                    if (data.ok) {
                        _this.msg = "Data successfully added. Please upload file.";
                        _this.showMessage(enum_1.MessageSeverity.success, _this.msg);
                        var str = data._body.replace(/^"(.*)"$/, '$1');
                        _this.dbops = enum_1.DBOperation.update;
                        _this.router.navigate(['/detail', _this.myBlogType, _this.myBlogTypeId, str]);
                        _this.currentid = str;
                        _this.LoadData();
                    }
                    else {
                        _this.msg = "There is some issue in saving records, please contact to system administrator!";
                    }
                }, function (error) {
                    _this.msg = error;
                    _this.showMessage(enum_1.MessageSeverity.error, _this.msg);
                });
                break;
            case enum_1.DBOperation.update:
                var curl = global_1.Global.BASE_USER_ENDPOINT + "blog/Edit";
                this._crudService.post(curl, formData._value).subscribe(function (data) {
                    if (data == 1) {
                        _this.msg = "Data successfully updated.";
                        _this.showMessage(enum_1.MessageSeverity.success, _this.msg);
                        _this.LoadData();
                    }
                    else {
                        _this.msg = "There is some issue in saving records, please contact to system administrator!";
                        _this.showMessage(enum_1.MessageSeverity.error, _this.msg);
                    }
                }, function (error) {
                    _this.msg = error;
                    _this.showMessage(enum_1.MessageSeverity.error, _this.msg);
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
                }, function (error) {
                    _this.msg = error;
                });
                break;
            case enum_1.DBOperation.deleteimage:
                var curl = global_1.Global.BASE_USER_ENDPOINT + "blog/Deleteimage";
                this._crudService.post(curl, formData._value).subscribe(function (data) {
                    if (data == 1) {
                        _this.msg = "File successfully deleted.";
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
    DetailTypeComponent.prototype.SetControlsState = function (isEnable) {
        isEnable ? this.mainFrm.enable() : this.mainFrm.disable();
    };
    //showDialogToAdd() {
    //    this.isNewForm = true;
    //    this.mainFrm.reset();
    //    this.displayDialog = true;
    //    this.dbops = DBOperation.create;
    //}
    DetailTypeComponent.prototype.save = function (formData) {
        this.SetControlsState(true);
        this.onSubmit(formData);
    };
    //onRowSelect(event: any) {
    //    this.mainobj = event.data;
    //    this.mainFrm.patchValue(this.mainobj);
    //    console.log(event);
    //    this.displayDialog = true;
    //    this.dbops = DBOperation.update;
    //    this.isNewForm = false;
    //}
    DetailTypeComponent.prototype.delete = function () {
        if (confirm("Are you sure to delete ")) {
            this.dbops = enum_1.DBOperation.delete;
            this.onSubmit(this.mainFrm);
        }
    };
    DetailTypeComponent.prototype.showMessage = function (severity, errormsg) {
        this.msgs = [];
        this.msgs.push({ severity: severity, summary: 'Message', detail: errormsg });
    };
    //showError(errormsg: any) {
    //    this.msgs = [];
    //    this.msgs.push({ severity: 'error', summary: 'Error Message', detail: errormsg });
    //}
    DetailTypeComponent.prototype.deleteimage = function (formData) {
        if (confirm("Are you sure to delete?")) {
            this.dbops = enum_1.DBOperation.deleteimage;
            this.onSubmit(formData);
        }
    };
    DetailTypeComponent.prototype.onUpload = function (event) {
        for (var _i = 0, _a = event.files; _i < _a.length; _i++) {
            var file = _a[_i];
            this.msg = "File saved.";
            this.LoadData();
        }
    };
    DetailTypeComponent.prototype.onBeforeSend = function (event, itemid) {
        //event.xhr.setRequestHeader(this._crudService.setheader());
        event.formData.append('BlogModelid', itemid);
        event.formData.append('dbcodeid', this.dbcodeid);
    };
    DetailTypeComponent = __decorate([
        core_1.Component({
            selector: 'detail-type-crud',
            templateUrl: 'app/dashboard/CRUDType/DetailType/DetailType.component.html',
        }),
        __metadata("design:paramtypes", [forms_1.FormBuilder, authcrud_service_1.AuthcrudService, router_1.ActivatedRoute, router_1.Router])
    ], DetailTypeComponent);
    return DetailTypeComponent;
}());
exports.DetailTypeComponent = DetailTypeComponent;
//# sourceMappingURL=DetailType.component.js.map