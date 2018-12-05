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
var ProfileComponent = /** @class */ (function () {
    function ProfileComponent(_crudService, router, activatedRoute) {
        this._crudService = _crudService;
        this.router = router;
        this.activatedRoute = activatedRoute;
        this.submitted = false;
        this.credentials = { OldPassword: '', NewPassword: '', Confirmpassword: '' };
    }
    ProfileComponent.prototype.ngOnInit = function () {
        // subscribe to router event
    };
    ProfileComponent.prototype.ngOnDestroy = function () {
        // prevent memory leak by unsubscribing
    };
    ProfileComponent.prototype.changepassword = function (_a) {
        var _this = this;
        var value = _a.value, valid = _a.valid;
        this.submitted = true;
        this.isRequesting = true;
        this.msg = "";
        if (valid) {
            this._crudService.post(global_1.Global.BASE_USER_ENDPOINT + "webdetail/ChangePassword", value).subscribe(function (data) {
                if (data == 1) {
                    _this.msg = "Data successfully update.";
                }
                else {
                    _this.msg = "There is some issue in saving records, please contact to system administrator!";
                }
                _this.isRequesting = false;
            }, function (error) {
                _this.msg = error;
                _this.isRequesting = false;
            });
        }
    };
    ProfileComponent = __decorate([
        core_1.Component({
            //selector: 'app-register-form',
            templateUrl: 'app/dashboard/profile/profile.component.html',
        }),
        __metadata("design:paramtypes", [authcrud_service_1.AuthcrudService, router_1.Router, router_1.ActivatedRoute])
    ], ProfileComponent);
    return ProfileComponent;
}());
exports.ProfileComponent = ProfileComponent;
//# sourceMappingURL=profile.component.js.map