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
var forms_1 = require("@angular/forms");
var enum_1 = require("../../Shared/enum");
var dropdown_1 = require("primeng/components/dropdown/dropdown");
//import  Quill  from  "quill";
var PageDetailComponent = /** @class */ (function () {
    function PageDetailComponent(fb, _crudService, route) {
        this.fb = fb;
        this._crudService = _crudService;
        this.route = route;
        this.msgs = [];
        //mainobjlist: PageModel[];
        //mainobj: PageModel;
        this.indLoading = false;
        this.isNewForm = false;
        this.isMultipleContent = false;
    }
    PageDetailComponent.prototype.ngOnInit = function () {
        var _this = this;
        //this.InitilizeFormItems();
        this.isMultipleContent = false;
        this.route.params.subscribe(function (params) {
            _this.currentid = params['id']; // --> Name must match wanted parameter
        });
        this.LoadData();
        this.conrentoptions = [
            { label: 'Add Content Type', value: enum_1.PageContentType.none },
            { label: 'HTML', value: enum_1.PageContentType.htmlcode },
            { label: 'Text', value: enum_1.PageContentType.simpletext },
            { label: 'Image', value: enum_1.PageContentType.image },
        ];
    };
    PageDetailComponent.prototype.InitilizeFormItems = function () {
        this.mainFrm = this.fb.group({
            PageModelid: [''],
            PageTitle: ['', forms_1.Validators.required],
            pageurl: ['', forms_1.Validators.required],
            pcontents: this.fb.array([]),
            //pcontents: this.fb.array([this.initPContents_fn(PageContentType.htmlcode,""),]),
            pagecontent: [''],
            isPublished: [''],
            remarks: [''],
        });
    };
    PageDetailComponent.prototype.initPContents_fn = function (pctype, pcon) {
        var pccontent = this.initPContents();
        pccontent.setValue({ PCType: pctype, PContent: pcon });
        return pccontent;
    };
    PageDetailComponent.prototype.initPContents = function () {
        return this.fb.group({
            PCType: [''],
            PContent: ['']
        });
    };
    PageDetailComponent.prototype.addAnotherContentFormd = function (event, lcontent) {
        if (event === enum_1.PageContentType.none)
            return;
        if (this.isMultipleContent)
            this.dropDownPCType.updateSelectedOption(enum_1.PageContentType.none);
        var control = this.mainFrm.controls['pcontents'];
        //const addrCtrl = this.initPContents();
        var addrCtrl = this.initPContents_fn(event, lcontent);
        control.push(addrCtrl);
        /* subscribe to individual address value changes */
        // addrCtrl.valueChanges.subscribe(x => {
        //   console.log(x);
        // })
    };
    //addAnotherContentForm() {
    //    const control = <FormArray>this.mainFrm.controls['pcontents'];
    //    const addrCtrl = this.initPContents();
    //    control.push(addrCtrl);
    //    /* subscribe to individual address value changes */
    //    // addrCtrl.valueChanges.subscribe(x => {
    //    //   console.log(x);
    //    // })
    //}
    PageDetailComponent.prototype.removeContentForm = function (i) {
        var r = confirm("Are you sure want to delete?");
        if (r == true) {
            var control = this.mainFrm.controls['pcontents'];
            control.removeAt(i);
        }
    };
    PageDetailComponent.prototype.LoadData = function () {
        var _this = this;
        this.indLoading = true;
        this.InitilizeFormItems();
        if (this.currentid != "0") {
            this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "page/GetPage?id=" + this.currentid)
                .subscribe(function (records) {
                _this.mainFrm.patchValue(records);
                var a = records.pagecontent;
                var jsoncontent = JSON.parse(a);
                jsoncontent.pcary.forEach(function (avalue) {
                    _this.addAnotherContentFormd(avalue.PCType, avalue.PContent);
                });
                _this.indLoading = false;
                _this.dbops = enum_1.DBOperation.update;
            }, function (error) { return _this.msg = error; });
        }
        else {
            this.addAnotherContentFormd(enum_1.PageContentType.htmlcode, "");
            this.dbops = enum_1.DBOperation.create;
        }
    };
    PageDetailComponent.prototype.onSubmit = function (formData) {
        var _this = this;
        this.msg = "";
        formData._value.pagecontent = JSON.stringify({ pcary: formData._value.pcontents });
        switch (this.dbops) {
            case enum_1.DBOperation.create:
                var curl = global_1.Global.BASE_USER_ENDPOINT + "page/Create";
                if (formData._value.isPublished == null || formData._value.isPublished == '')
                    formData._value.isPublished = false;
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
                var curl = global_1.Global.BASE_USER_ENDPOINT + "page/Edit";
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
        }
    };
    PageDetailComponent.prototype.SetControlsState = function (isEnable) {
        isEnable ? this.mainFrm.enable() : this.mainFrm.disable();
    };
    //showDialogToAdd() {
    //    this.isNewForm = true;
    //    this.mainFrm.reset();
    //    this.displayDialog = true;
    //    this.dbops = DBOperation.create;
    //}
    PageDetailComponent.prototype.save = function (formData) {
        //this.SetControlsState(true);
        //this.dbops = DBOperation.create;
        this.onSubmit(formData);
    };
    //onRowSelect(event: any) {
    //    this.mainobj = event.data;
    //    this.mainFrm.patchValue(this.mainobj);
    //    this.displayDialog = true;
    //    this.dbops = DBOperation.update;
    //    this.isNewForm = false;
    //}
    PageDetailComponent.prototype.delete = function () {
        if (confirm("Are you sure to delete ")) {
            this.dbops = enum_1.DBOperation.delete;
            this.onSubmit(this.mainFrm);
        }
    };
    PageDetailComponent.prototype.onKey = function (txtvalue) {
        var ntxt = txtvalue.toLowerCase();
        ntxt = ntxt.trim().replace(/[^a-zA-Z0-9]/g, '-');
        ntxt = ntxt.replace(/-+/g, '-');
        var firstchar = ntxt[0];
        var lastchar = ntxt[ntxt.length - 1];
        if (firstchar == '-')
            ntxt = ntxt.slice(1);
        if (lastchar == '-')
            ntxt = ntxt.slice(0, -1);
        return ntxt;
    };
    PageDetailComponent.prototype.onBlurTitle = function (txtval) {
        this.urltext = this.onKey(txtval);
    };
    PageDetailComponent.prototype.onBlurURL = function (txtval) {
        this.mainFrm.patchValue({ pageurl: this.onKey(txtval) });
        //this.selectedurl = this.onKey(txtval);
    };
    PageDetailComponent.prototype.seturl = function () {
        var r = confirm("Are you sure want to add URL?");
        if (r == true) {
            //this.selectedurl = this.urltext;
            this.mainFrm.patchValue({ pageurl: this.urltext });
            this.urltext = "";
        }
    };
    PageDetailComponent.prototype.showError = function (errormsg) {
        this.msgs = [];
        this.msgs.push({ severity: 'error', summary: 'Error Message', detail: errormsg });
    };
    __decorate([
        core_1.ViewChild('dropDownPCType'),
        __metadata("design:type", dropdown_1.Dropdown)
    ], PageDetailComponent.prototype, "dropDownPCType", void 0);
    PageDetailComponent = __decorate([
        core_1.Component({
            //selector: 'app-register-form',
            templateUrl: 'app/dashboard/PageDetail/PageDetail.component.html',
        }),
        __metadata("design:paramtypes", [forms_1.FormBuilder, authcrud_service_1.AuthcrudService, router_1.ActivatedRoute])
    ], PageDetailComponent);
    return PageDetailComponent;
}());
exports.PageDetailComponent = PageDetailComponent;
//# sourceMappingURL=PageDetail.component.js.map