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
var base_service_1 = require("./base.service");
require("rxjs/add/operator/map");
require("rxjs/add/operator/do");
require("rxjs/add/operator/catch");
var AuthcrudService = /** @class */ (function (_super) {
    __extends(AuthcrudService, _super);
    function AuthcrudService(_http) {
        var _this = _super.call(this) || this;
        _this._http = _http;
        return _this;
    }
    AuthcrudService.prototype.get = function (url) {
        //let headers = new Headers();
        //headers.append('Content-Type', 'application/json');
        //let authToken = localStorage.getItem('auth_token');
        //headers.append('Authorization', `Bearer ${authToken}`);
        return this._http.get(url, { headers: this.setheader() })
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    AuthcrudService.prototype.post = function (url, model) {
        var body = JSON.stringify(model);
        //let headers = new Headers({ 'Content-Type': 'application/json' });
        //let authToken = localStorage.getItem('auth_token');
        //headers.append('Authorization', `Bearer ${authToken}`);
        var options = new http_1.RequestOptions({ headers: this.setheader() });
        return this._http.post(url, body, options)
            .map(function (res) { return true; })
            .catch(this.handleError);
    };
    AuthcrudService.prototype.postwithresponse = function (url, model) {
        var body = JSON.stringify(model);
        var options = new http_1.RequestOptions({ headers: this.setheader() });
        return this._http.post(url, body, options)
            .map(function (response) { return response; })
            .catch(this.handleError);
    };
    AuthcrudService.prototype.put = function (url, model) {
        var body = JSON.stringify(model);
        //let headers = new Headers({ 'Content-Type': 'application/json' });
        //let authToken = localStorage.getItem('auth_token');
        //headers.append('Authorization', `Bearer ${authToken}`);
        var options = new http_1.RequestOptions({ headers: this.setheader() });
        return this._http.put(url, body, options)
            .map(function (res) { return true; })
            .catch(this.handleError);
    };
    AuthcrudService.prototype.delete = function (url, id) {
        //let headers = new Headers({ 'Content-Type': 'application/json' });
        //let authToken = localStorage.getItem('auth_token');
        //headers.append('Authorization', `Bearer ${authToken}`);
        var options = new http_1.RequestOptions({ headers: this.setheader() });
        return this._http.delete(url, options)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    AuthcrudService.prototype.setheader = function () {
        var headers = new http_1.Headers({ 'Content-Type': 'application/json' });
        var authToken = localStorage.getItem('auth_token');
        var dbcodeid = localStorage.getItem('dbcodeid');
        headers.append('Authorization', "Bearer " + authToken);
        headers.append('dbcodeid', dbcodeid);
        return headers;
    };
    //private handleError(error: Response) {
    //    console.error(error);
    //    return Observable.throw(error.json().error || 'Server error');
    //}
    AuthcrudService.prototype.unique = function (arr, prop) {
        return arr.map(function (e) { return e[prop]; }).filter(function (e, i, a) {
            return i === a.indexOf(e);
        });
    };
    AuthcrudService.prototype.removeDuplicates = function (arr, prop) {
        var newArray = [];
        var lookupObject = {};
        for (var i in arr) {
            lookupObject[arr[i][prop]] = arr[i];
        }
        for (i in lookupObject) {
            newArray.push(lookupObject[i]);
        }
        return newArray;
    };
    AuthcrudService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.Http])
    ], AuthcrudService);
    return AuthcrudService;
}(base_service_1.BaseService));
exports.AuthcrudService = AuthcrudService;
//# sourceMappingURL=authcrud.service.js.map