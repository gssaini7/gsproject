"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Observable_1 = require("rxjs/Observable");
require("rxjs/add/observable/throw");
var BaseService = /** @class */ (function () {
    function BaseService() {
    }
    BaseService.prototype.handleError = function (error) {
        var applicationError = error.headers.get('Application-Error');
        // either applicationError in header or model error in body
        if (applicationError) {
            return Observable_1.Observable.throw(applicationError);
        }
        if (error.status === 401) {
            window.location.href = "/home/error";
        }
        var modelStateErrors = '';
        var serverError = error.json();
        if (!serverError.type) {
            for (var key in serverError) {
                if (serverError[key]) {
                    if (typeof serverError[key] === "string")
                        modelStateErrors += serverError[key] + '\n';
                    if (typeof serverError[key] === "object") {
                        var newobj = serverError[key];
                        for (var innerkey in newobj) {
                            modelStateErrors += newobj[innerkey] + '\n';
                        }
                    }
                }
            }
        }
        modelStateErrors = modelStateErrors = '' ? null : modelStateErrors;
        return Observable_1.Observable.throw(modelStateErrors || 'Server error');
    };
    return BaseService;
}());
exports.BaseService = BaseService;
//# sourceMappingURL=base.service.js.map