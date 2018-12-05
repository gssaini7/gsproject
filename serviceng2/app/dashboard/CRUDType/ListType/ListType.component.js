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
var ListTypeComponent = /** @class */ (function () {
    //@Input() myBlogType: string="Blog";
    function ListTypeComponent(_crudService, router, route) {
        this._crudService = _crudService;
        this.router = router;
        this.route = route;
        this.indLoading = false;
        this.isAssignment = false;
    }
    ListTypeComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.LoadData();
        this.route.params.subscribe(function (params) {
            _this.currentblogtypename = params['type']; // --> Name must match wanted parameter
        });
        this.cmitems = [
            { label: 'Edit', icon: 'fa-edit', command: function (event) { return _this.onRowSelect(_this.mainobj); } },
            { label: 'Forward', icon: 'fa-share-square', command: function (event) { return _this.onForward(_this.mainobj); } },
        ];
        var ismainadmin = sessionStorage.getItem('isMainAdmin');
        if (ismainadmin === "true") {
            this.LoadAdmins();
        }
    };
    ListTypeComponent.prototype.LoadAdmins = function () {
        var _this = this;
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "Admin/Admins")
            .subscribe(function (records) {
            _this.listadmins = [];
            _this.loopthroughrecords(records);
        }, function (error) { return _this.msg = error; });
    };
    ListTypeComponent.prototype.loopthroughrecords = function (records) {
        var _this = this;
        records.forEach(function (item) {
            _this.listadmins.push(item);
            if (item.AdminChildren != null) {
                _this.loopthroughrecords(item.AdminChildren);
            }
        });
    };
    ListTypeComponent.prototype.LoadData = function () {
        var _this = this;
        this.indLoading = true;
        this.blogtypelist = null;
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "BlogType/GetAll")
            .subscribe(function (records) {
            _this.blogtypelist = records;
            if (_this.currentblogtypename === undefined) {
                _this.currentblogtype = records[0];
            }
            else {
                var blogstypes = records.filter(function (a) { return a.BlogTypeName === _this.currentblogtypename; })[0];
                if (blogstypes !== undefined)
                    _this.currentblogtype = blogstypes;
                else
                    _this.currentblogtype = records[0];
            }
            _this.LoadDataBlogs();
            _this.indLoading = false;
        }, function (error) { return _this.msg = error; });
    };
    ListTypeComponent.prototype.OnChangeAdmin = function () {
        this.LoadDataBlogs();
    };
    ListTypeComponent.prototype.LoadDataBlogs = function () {
        var _this = this;
        this.msg = "";
        this.indLoading = true;
        this.mainobjlist = null;
        this.isAssignment = false;
        //let newblogteacher: TeacherBlogModel = { BlogModelid: this.currentblogtype.BlogTypeModelid, TeacherId: null };
        //this.blogteacherrel = newblogteacher;
        var createrid = "";
        if (this.blogcreater !== undefined) {
            createrid = this.blogcreater.UserID;
        }
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "blog/GetAll?id=" + this.currentblogtype.BlogTypeModelid + "&createrid=" + createrid)
            .subscribe(function (records) {
            _this.mainobjlist = records;
            _this.indLoading = false;
            var checkisassignment = records.filter(function (a) { return a.SubjectModelid !== null; });
            if (checkisassignment.length !== 0)
                _this.isAssignment = true;
        }, function (error) { return _this.msg = error; });
        this.router.navigate(['/blog', this.currentblogtype.BlogTypeName]);
    };
    //LoadDataBlogs(): void {
    //    this.indLoading = true;
    //    this.mainobjlist = null;
    //    this._crudService.get(Global.BASE_USER_ENDPOINT + "blog/GetAll?id=" + this.currentblogtype.BlogTypeModelid)
    //        .subscribe(records => {
    //            this.mainobjlist = records;
    //            this.indLoading = false;
    //        },
    //        error => this.msg = <any>error);
    //    this.router.navigate(['/blog', this.currentblogtype.BlogTypeName]);
    //}
    ListTypeComponent.prototype.addNewPage = function () {
        this.router.navigate(['/detail', this.currentblogtype.BlogTypeName, this.currentblogtype.BlogTypeModelid, 'new']);
    };
    ListTypeComponent.prototype.onForward = function (event) {
        this.router.navigate(['/forward', event.BlogModelid]);
    };
    ListTypeComponent.prototype.onRowSelect = function (event) {
        this.router.navigate(['/detail', this.currentblogtype.BlogTypeName, this.currentblogtype.BlogTypeModelid, event.BlogModelid]);
    };
    ListTypeComponent.prototype.changetab = function (selectedblogtype) {
        this.currentblogtype = selectedblogtype;
        //this.currentblogtypename = selectedblogtype.BlogTypeName;
        this.LoadDataBlogs();
    };
    ListTypeComponent = __decorate([
        core_1.Component({
            selector: 'list-type-crud',
            templateUrl: 'app/dashboard/CRUDType/ListType/ListType.component.html',
        }),
        __metadata("design:paramtypes", [authcrud_service_1.AuthcrudService, router_1.Router, router_1.ActivatedRoute])
    ], ListTypeComponent);
    return ListTypeComponent;
}());
exports.ListTypeComponent = ListTypeComponent;
//# sourceMappingURL=ListType.component.js.map