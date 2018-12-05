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
var PopupDialogComponent = /** @class */ (function () {
    function PopupDialogComponent(_crudService) {
        this._crudService = _crudService;
        this.indLoading = false;
        this.displayDialog = false;
        this.notify = new core_1.EventEmitter();
    }
    PopupDialogComponent.prototype.ngOnInit = function () {
        if (this.toShow === undefined)
            this.toShow = 'link';
    };
    PopupDialogComponent.prototype.sendTitle = function (str) {
        this.notify.emit(str);
    };
    PopupDialogComponent.prototype.showdialog = function () {
        this.displayDialog = true;
        this.LoadPages();
        this.LoadAlbums();
    };
    PopupDialogComponent.prototype.LoadPages = function () {
        var _this = this;
        this.indLoading = true;
        this.pagesobjlist = null;
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "page/GetAll")
            .subscribe(function (records) {
            _this.pagesobjlist = records.filter(function (s) { return s.isPublished; });
            _this.indLoading = false;
        }, function (error) { return _this.msg = error; });
    };
    PopupDialogComponent.prototype.LoadAlbums = function () {
        var _this = this;
        this.indLoading = true;
        this.albumobjlist = null;
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "Album/GetAll")
            .subscribe(function (records) {
            _this.albumobjlist = records.filter(function (s) { return s.isPublished; });
            _this.indLoading = false;
            if (_this.albumobjlist.length != 0) {
                var firstalbum = _this.albumobjlist[0];
                _this.selectedalbum = firstalbum;
                _this.LoadImages(firstalbum.AlbumModelid);
            }
        }, function (error) { return _this.msg = error; });
    };
    PopupDialogComponent.prototype.LoadImages = function (id) {
        var _this = this;
        this.msg = '';
        if (id == undefined)
            return;
        this.indLoading = true;
        this.imageobjlist = null;
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "ImageGallery/GetAll?id=" + id)
            .subscribe(function (records) {
            _this.imageobjlist = records;
            _this.indLoading = false;
        }, function (error) { return _this.msg = error; });
    };
    PopupDialogComponent.prototype.onpageselection = function () {
        this.sendTitle(this.selectedpage.pageurl);
        this.displayDialog = false;
    };
    PopupDialogComponent.prototype.onalbumselection = function () {
        //this.sendTitle(this.selectedpage.pageurl);
        //this.displayDialog = false;
        this.LoadImages(this.selectedalbum.AlbumModelid);
    };
    PopupDialogComponent.prototype.onimageselection = function (data) {
        this.sendTitle(data.ImageName);
        this.displayDialog = false;
    };
    __decorate([
        core_1.Input(),
        __metadata("design:type", String)
    ], PopupDialogComponent.prototype, "toShow", void 0);
    __decorate([
        core_1.Output(),
        __metadata("design:type", core_1.EventEmitter)
    ], PopupDialogComponent.prototype, "notify", void 0);
    PopupDialogComponent = __decorate([
        core_1.Component({
            selector: 'popup-dialog',
            templateUrl: 'app/dashboard/PopupDialog/PopupDialog.component.html',
        }),
        __metadata("design:paramtypes", [authcrud_service_1.AuthcrudService])
    ], PopupDialogComponent);
    return PopupDialogComponent;
}());
exports.PopupDialogComponent = PopupDialogComponent;
//# sourceMappingURL=PopupDialog.component.js.map