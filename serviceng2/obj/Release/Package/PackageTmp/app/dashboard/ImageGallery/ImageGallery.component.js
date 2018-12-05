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
var ImageGalleryComponent = /** @class */ (function () {
    function ImageGalleryComponent(fb, _crudService) {
        this.fb = fb;
        this._crudService = _crudService;
        this.msgs = [];
        this.indLoading = false;
        this.isNewForm = false;
    }
    ImageGalleryComponent.prototype.ngOnInit = function () {
        this.InitilizeFormItems();
        //this.LoadData();
    };
    ImageGalleryComponent.prototype.InitilizeFormItems = function () {
        this.mainFrm = this.fb.group({
            ImageGalleryModelid: [''],
            ImageName: [''],
            isPublished: [''],
            remarks: [''],
        });
    };
    ImageGalleryComponent.prototype.LoadData = function (id) {
        var _this = this;
        this.dbcodeid = localStorage.getItem('dbcodeid');
        this.msg = '';
        if (id == undefined)
            return;
        this.currentalbumid = id;
        this.indLoading = true;
        this.mainobjlist = null;
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "ImageGallery/GetAll?id=" + this.currentalbumid)
            .subscribe(function (records) {
            _this.mainobjlist = records;
            _this.indLoading = false;
        }, function (error) { return _this.msg = error; });
    };
    ImageGalleryComponent.prototype.onSubmit = function (formData) {
        var _this = this;
        this.msg = "";
        switch (this.dbops) {
            //case DBOperation.create:
            //    if (formData._value.isPublished == null || formData._value.isPublished == '')
            //        formData._value.isPublished = false;
            //    var curl = Global.BASE_USER_ENDPOINT + "ImageGallery/Create";
            //    formData._value.AlbumModelid = this.currentalbumid;
            //    this._crudService.post(curl, formData._value).subscribe(
            //        data => {
            //            if (data == 1) //Success
            //            {
            //                this.msg = "Data successfully added.";
            //                this.LoadData(this.currentalbumid);
            //            }
            //            else {
            //                this.msg = "There is some issue in saving records, please contact to system administrator!"
            //            }
            //            this.displayDialog = false;
            //        },
            //        error => {
            //            this.msg = error;
            //            this.showError(this.msg);
            //        }
            //    );
            //    break;
            //case DBOperation.update:
            //    var curl = Global.BASE_USER_ENDPOINT + "ImageGallery/Edit";
            //    this._crudService.post(curl, formData._value).subscribe(
            //        data => {
            //            if (data == 1) //Success
            //            {
            //                this.msg = "Data successfully updated.";
            //                this.LoadData(this.currentalbumid);
            //            }
            //            else {
            //                this.msg = "There is some issue in saving records, please contact to system administrator!"
            //            }
            //            this.displayDialog = false;
            //        },
            //        error => {
            //            this.msg = error;
            //            this.showError(this.msg);
            //        }
            //    );
            //    break;
            case enum_1.DBOperation.delete:
                var curl = global_1.Global.BASE_USER_ENDPOINT + "ImageGallery/Delete";
                this._crudService.post(curl, formData).subscribe(function (data) {
                    if (data == 1) {
                        _this.msg = "Data successfully deleted.";
                        _this.LoadData(_this.currentalbumid);
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
    ImageGalleryComponent.prototype.SetControlsState = function (isEnable) {
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
    //delete() {
    //    if (confirm("Are you sure to delete ")) {
    //        this.dbops = DBOperation.delete;
    //        this.onSubmit(this.mainFrm);
    //    }
    //}
    ImageGalleryComponent.prototype.delete = function (event) {
        if (confirm("Are you sure to delete ")) {
            this.dbops = enum_1.DBOperation.delete;
            this.onSubmit(event);
        }
    };
    ImageGalleryComponent.prototype.showError = function (errormsg) {
        this.msgs = [];
        this.msgs.push({ severity: 'error', summary: 'Error Message', detail: errormsg });
    };
    ImageGalleryComponent.prototype.getImageGalleryofAlbum = function (albumid) {
        this.LoadData(albumid);
    };
    ImageGalleryComponent.prototype.onUpload = function (event) {
        for (var _i = 0, _a = event.files; _i < _a.length; _i++) {
            var file = _a[_i];
            this.msg = "Image saved.";
            this.LoadData(this.currentalbumid);
        }
    };
    ImageGalleryComponent.prototype.onBeforeSend = function (event) {
        var authToken = localStorage.getItem('auth_token');
        //event.xhr.setRequestHeader('Content-Type', 'application/json');
        event.xhr.setRequestHeader('Authorization', "Bearer " + authToken);
        event.formData.append('AlbumModelid', this.currentalbumid);
        event.formData.append('dbcodeid', this.dbcodeid);
    };
    ImageGalleryComponent = __decorate([
        core_1.Component({
            selector: 'image-gallery',
            templateUrl: 'app/dashboard/ImageGallery/ImageGallery.component.html',
        }),
        __metadata("design:paramtypes", [forms_1.FormBuilder, authcrud_service_1.AuthcrudService])
    ], ImageGalleryComponent);
    return ImageGalleryComponent;
}());
exports.ImageGalleryComponent = ImageGalleryComponent;
//# sourceMappingURL=ImageGallery.component.js.map