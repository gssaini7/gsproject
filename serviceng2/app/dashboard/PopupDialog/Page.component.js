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
//import { Message } from 'primeng/components/common/api';
//import { FormBuilder, FormGroup, Validators } from '@angular/forms';
//import { DBOperation } from '../../Shared/enum';
//import { EqualsValidator } from '../../Services/equals.validators';
//import { SelectItem } from 'primeng/components/common/api';
var PageComponent = /** @class */ (function () {
    //dbops: DBOperation;
    //isNewForm: boolean = false;
    function PageComponent(_crudService, router) {
        this._crudService = _crudService;
        this.router = router;
        //mainobj: PageModel;
        this.indLoading = false;
    }
    PageComponent.prototype.ngOnInit = function () {
        //this.InitilizeFormItems();
        this.LoadData();
    };
    //InitilizeFormItems() {
    //    this.mainFrm = this.fb.group({
    //        PageModelid: [''],
    //        PageTitle: ['', Validators.required],
    //        pageurl: ['', Validators.required],
    //        pagecontent: [''],
    //        isPublished: [''],
    //        remarks: [''],
    //    });
    //}
    PageComponent.prototype.LoadData = function () {
        var _this = this;
        this.indLoading = true;
        this.mainobjlist = null;
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "page/GetAll")
            .subscribe(function (records) {
            _this.mainobjlist = records;
            _this.indLoading = false;
        }, function (error) { return _this.msg = error; });
    };
    //onSubmit(formData: any) {
    //    this.msg = "";
    //    console.log(formData._value);
    //    switch (this.dbops) {
    //        case DBOperation.create:
    //            var curl = Global.BASE_USER_ENDPOINT + "page/Create";
    //            if (formData._value.isPublished == null)
    //                formData._value.isPublished = false;
    //            this._crudService.post(curl, formData._value).subscribe(
    //                data => {
    //                    if (data == 1) //Success
    //                    {
    //                        this.msg = "Data successfully added.";
    //                        this.LoadData();
    //                    }
    //                    else {
    //                        this.msg = "There is some issue in saving records, please contact to system administrator!"
    //                    }
    //                    this.displayDialog = false;
    //                },
    //                error => {
    //                    this.msg = error;
    //                    this.showError(this.msg);
    //                }
    //            );
    //            break;
    //        case DBOperation.update:
    //            var curl = Global.BASE_USER_ENDPOINT + "page/Edit";
    //            this._crudService.post(curl, formData._value).subscribe(
    //                data => {
    //                    if (data == 1) //Success
    //                    {
    //                        this.msg = "Data successfully updated.";
    //                        this.LoadData();
    //                    }
    //                    else {
    //                        this.msg = "There is some issue in saving records, please contact to system administrator!"
    //                    }
    //                    this.displayDialog = false;
    //                },
    //                error => {
    //                    this.msg = error;
    //                    this.showError(this.msg);
    //                }
    //            );
    //            break;
    //        //case DBOperation.delete:
    //        //    var curl = Global.BASE_USER_ENDPOINT + "item/Delete";
    //        //    this._crudService.post(curl, formData._value).subscribe(
    //        //        data => {
    //        //            if (data == 1) //Success
    //        //            {
    //        //                this.msg = "Data successfully deleted.";
    //        //                this.LoadData();
    //        //            }
    //        //            else {
    //        //                this.msg = "There is some issue in deleting records, please contact to system administrator!"
    //        //            }
    //        //            this.displayDialog = false;
    //        //        },
    //        //        error => {
    //        //            this.msg = error;
    //        //        }
    //        //    );
    //        //    break;
    //    }
    //}
    //SetControlsState(isEnable: boolean) {
    //    isEnable ? this.mainFrm.enable() : this.mainFrm.disable();
    //}
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
    PageComponent.prototype.addNewPage = function () {
        this.router.navigate(['/page/0']);
    };
    PageComponent.prototype.onRowSelect = function (event) {
        //this.mainobj = event.data;
        //this.mainFrm.patchValue(this.mainobj);
        //this.displayDialog = true;
        //this.dbops = DBOperation.update;
        //this.isNewForm = false;
        this.router.navigate(['/page', event.data.PageModelid]);
    };
    PageComponent = __decorate([
        core_1.Component({
            //selector: 'app-register-form',
            templateUrl: 'app/dashboard/Page/Page.component.html',
        }),
        __metadata("design:paramtypes", [authcrud_service_1.AuthcrudService, router_1.Router])
    ], PageComponent);
    return PageComponent;
}());
exports.PageComponent = PageComponent;
//# sourceMappingURL=Page.component.js.map