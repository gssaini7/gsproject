"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
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
var http_1 = require("@angular/http");
//import { ConfigService } from '../Shared/config.service';
var base_service_1 = require("./base.service");
var global_1 = require("../Shared/global");
var BehaviorSubject_1 = require("rxjs/BehaviorSubject");
//import * as _ from 'lodash';
// Add the RxJS Observable operators we need in this app.
//import '../../rxjs-operators';
var UserService = /** @class */ (function (_super) {
    __extends(UserService, _super);
    //constructor(private http: Http, private configService: ConfigService) {
    function UserService(http) {
        var _this = _super.call(this) || this;
        _this.http = http;
        _this.baseUrl = '';
        _this.loggedIn = _this.gs_hasToken();
        //// Observable navItem source
        _this._authNavStatusSource = new BehaviorSubject_1.BehaviorSubject(_this.loggedIn);
        //// Observable navItem stream
        _this.authNavStatus$ = _this._authNavStatusSource.asObservable();
        //this.loggedIn = !!localStorage.getItem('auth_token');
        // ?? not sure if this the best way to broadcast the status but seems to resolve issue on page refresh where auth status is lost in
        // header component resulting in authed user nav links disappearing despite the fact user is still logged in
        //this._authNavStatusSource.next(this.loggedIn);
        //this.baseUrl = configService.getApiURI();
        _this.baseUrl = global_1.Global.BASE_USER_ENDPOINT;
        return _this;
    }
    UserService.prototype.register = function (email, password, ConfirmPassword) {
        var body = JSON.stringify({ email: email, password: password, ConfirmPassword: ConfirmPassword });
        var headers = new http_1.Headers({ 'Content-Type': 'application/json' });
        var options = new http_1.RequestOptions({ headers: headers });
        return this.http.post("api/account/Register", body, options)
            .map(function (res) { return true; })
            .catch(this.handleError);
    };
    UserService.prototype.login = function (userName, password) {
        var _this = this;
        var headers = new http_1.Headers();
        headers.append('Content-Type', 'application/x-www-form-urlencoded');
        var bodyrequest = "userName=" + encodeURIComponent(userName) +
            "&password=" + encodeURIComponent(password) +
            "&grant_type=password";
        return this.http
            .post(
        //this.baseUrl + 'Account/login',
        'token', bodyrequest, { headers: headers })
            .map(function (res) { return res.json(); })
            .map(function (res) {
            localStorage.setItem('auth_token', res.access_token);
            _this.loggedIn = true;
            _this._authNavStatusSource.next(true);
            //this.gs_isLoginSubject.next(true);
            return true;
        })
            .catch(this.handleError);
    };
    UserService.prototype.logout = function () {
        localStorage.removeItem('auth_token');
        localStorage.removeItem('dbcodeid');
        sessionStorage.removeItem('isMainAdmin');
        sessionStorage.removeItem('isLegitimate');
        this.loggedIn = false;
        this._authNavStatusSource.next(false);
        //this.gs_isLoginSubject.next(false);
    };
    UserService.prototype.isLoggedIn = function () {
        return this.loggedIn;
    };
    UserService.prototype.gs_hasToken = function () {
        return !!localStorage.getItem('auth_token');
    };
    UserService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.Http])
    ], UserService);
    return UserService;
}(base_service_1.BaseService));
exports.UserService = UserService;
//# sourceMappingURL=user.service.js.map