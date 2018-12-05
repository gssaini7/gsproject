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
var sortby_pipe_1 = require("../../Shared/sortby.pipe");
var MenuItemComponent = /** @class */ (function () {
    function MenuItemComponent(fb, _crudService) {
        this.fb = fb;
        this._crudService = _crudService;
        this.msgs = [];
        //sortobjlist: MenuItemModel[];
        this.indLoading = false;
        this.isNewForm = false;
        //postionslist: SelectItem[] = [];
        this.nullparentid = '00000000-0000-0000-0000-000000000000';
    }
    MenuItemComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.InitilizeFormItems();
        //this.LoadData();
        //this.showalerttest();
        this.itemsmi = [
            { label: 'View', icon: 'fa-search', command: function (event) { return _this.onRowSelect(_this.selectedFile.data); } },
            { label: 'Delete', icon: 'fa-close', command: function (event) { return _this.delete(_this.selectedFile); } },
        ];
    };
    MenuItemComponent.prototype.InitilizeFormItems = function () {
        this.mainFrm = this.fb.group({
            MenuItemModelid: [''],
            ItemTitle: [''],
            ItemText: [''],
            ItemLink: [''],
            Classli: [''],
            Classa: [''],
            ParentMenu: [''],
            Parentid: [''],
            MenuModelid: [''],
            isPublished: [''],
            remarks: [''],
            itemPriority: [''],
        });
    };
    MenuItemComponent.prototype.LoadData = function (id) {
        var _this = this;
        this.msg = '';
        this.files2 = null;
        if (id == undefined)
            return;
        this.currentmenuid = id;
        this.indLoading = true;
        //this.mainobjlist = null;
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "MenuItem/GetAll?id=" + this.currentmenuid)
            .subscribe(function (records) {
            _this.mainobjlist = records;
            var sortPipeFilter = new sortby_pipe_1.SortByPipe();
            sortPipeFilter.transform(records, 'itemPriority');
            _this.indLoading = false;
            //if (item.Parentid == '00000000-0000-0000-0000-000000000000')
            //this.firstlevelmenu = records.filter((menuitem: any) => menuitem.Parentid === '00000000-0000-0000-0000-000000000000');
            //this.firstlevelmenu.push(null);
            //this.LoadParentMenuDropDown();
            var maintree = [];
            records.forEach(function (item) {
                if (item.Parentid === _this.nullparentid) {
                    var c = [];
                    records.filter(function (menuitem) { return menuitem.Parentid === item.MenuItemModelid; }).forEach(function (item2) {
                        var b = {
                            data: item2,
                        };
                        c.push(b);
                    });
                    var a = {
                        data: item,
                        children: c
                    };
                    maintree.push(a);
                }
            });
            _this.files2 = maintree;
            //this.msg = '';
        }, function (error) { return _this.msg = error; });
    };
    MenuItemComponent.prototype.LoadParentMenuDropDown = function () {
        var _this = this;
        this.firstlevelmenu = this.mainobjlist.filter(function (menuitem) { return menuitem.Parentid === _this.nullparentid && menuitem.MenuModelid === _this.currentmenuid; });
        this.firstlevelmenu.push(null);
        //this.fillpositionlist({value: null });
    };
    MenuItemComponent.prototype.onSubmit = function (formData) {
        var _this = this;
        if (this.currentmenuid == undefined)
            return;
        formData.MenuModelid = this.currentmenuid;
        this.msg = "";
        switch (this.dbops) {
            case enum_1.DBOperation.create:
                if (formData.isPublished == null || formData.isPublished == '')
                    formData.isPublished = false;
                var curl = global_1.Global.BASE_USER_ENDPOINT + "MenuItem/Create";
                this._crudService.post(curl, formData).subscribe(function (data) {
                    if (data == 1) {
                        _this.LoadData(_this.currentmenuid);
                        _this.msg = "Data successfully added.";
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
                var curl = global_1.Global.BASE_USER_ENDPOINT + "MenuItem/Edit";
                this._crudService.post(curl, formData).subscribe(function (data) {
                    if (data == 1) {
                        _this.LoadData(_this.currentmenuid);
                        _this.msg = "Data successfully updated.";
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
            case enum_1.DBOperation.delete:
                var curl = global_1.Global.BASE_USER_ENDPOINT + "MenuItem/Delete";
                this._crudService.post(curl, formData).subscribe(function (data) {
                    if (data == 1) {
                        _this.LoadData(_this.currentmenuid);
                        _this.msg = "Data successfully deleted.";
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
    MenuItemComponent.prototype.SetControlsState = function (isEnable) {
        isEnable ? this.mainFrm.enable() : this.mainFrm.disable();
    };
    MenuItemComponent.prototype.showDialogToAdd = function () {
        this.isNewForm = true;
        this.mainFrm.reset();
        this.displayDialog = true;
        this.dbops = enum_1.DBOperation.create;
        this.mainobjItem = null;
        this.LoadParentMenuDropDown();
        //this.fillpositionlist({value: null });
        //this.postionslist.push({ label: (this.postionslist.length + 1).toString(), value: this.postionslist.length + 1 });
    };
    MenuItemComponent.prototype.save = function (formData) {
        this.SetControlsState(true);
        console.log(formData._value);
        if (formData._value.ParentMenu != null)
            formData._value.Parentid = formData._value.ParentMenu.MenuItemModelid;
        else
            formData._value.Parentid = null;
        this.onSubmit(formData._value);
    };
    MenuItemComponent.prototype.onRowSelect = function (event) {
        var _this = this;
        this.mainobjItem = event;
        this.mainFrm.patchValue(this.mainobjItem);
        var selectedmenu = this.mainobjlist.filter(function (mid) { return mid.MenuItemModelid === _this.mainobjItem.Parentid; })[0];
        this.mainFrm.patchValue({ ParentMenu: selectedmenu });
        //this.mainFrm.patchValue({ itemPriority: this.mainobjItem.itemPriority });
        this.displayDialog = true;
        this.dbops = enum_1.DBOperation.update;
        this.isNewForm = false;
        this.currentmenuid = event.MenuModelid;
        //this.LoadData(this.currentmenuid);
        this.LoadParentMenuDropDown();
        this.firstlevelmenu = this.firstlevelmenu.filter(function (obj) { return obj !== _this.mainobjItem; }); // remove current menu
        //this.fillpositionlist({ value: selectedmenu });
    };
    MenuItemComponent.prototype.delete = function (event) {
        if (confirm("Are you sure to delete ")) {
            this.dbops = enum_1.DBOperation.delete;
            this.onSubmit(event.data);
        }
    };
    MenuItemComponent.prototype.showError = function (errormsg) {
        this.msgs = [];
        this.msgs.push({ severity: 'error', summary: 'Error Message', detail: errormsg });
    };
    //sortNode(event: any) {
    //    this.displayDialogOrder = true;
    //    this.sortobjlist = this.mainobjlist.filter(p => p.Parentid === event.data.Parentid);
    //}
    MenuItemComponent.prototype.nodeSelect = function (event) {
        this.onRowSelect(event.node.data);
        //console.log(event.node.data);
        //this.msgs = [];
        //this.msgs.push({ severity: 'info', summary: 'Node Selected', detail: event.node.data.name });
    };
    MenuItemComponent.prototype.nodeUnselect = function (event) {
        this.msgs = [];
        this.msgs.push({ severity: 'info', summary: 'Node Unselected', detail: event.node.data.name });
    };
    MenuItemComponent.prototype.nodeExpand = function (event) {
        if (event.node) {
            //in a real application, make a call to a remote url to load children of the current node and add the new nodes as children
            //this.nodeService.getLazyFilesystem().then(nodes => event.node.children = nodes);
            console.log(event.node);
        }
        this.msgs = [];
        this.msgs.push({ severity: 'info', summary: 'Node Expand', detail: event.node.data.name });
    };
    MenuItemComponent.prototype.viewNode = function (node) {
        this.msgs = [];
        this.msgs.push({ severity: 'info', summary: 'Node Selected', detail: node.data.name });
    };
    MenuItemComponent.prototype.deleteNode = function (node) {
        node.parent.children = node.parent.children.filter(function (n) { return n.data !== node.data; });
        this.msgs = [];
        this.msgs.push({ severity: 'info', summary: 'Node Deleted', detail: node.data.name });
    };
    MenuItemComponent.prototype.orderItemList = function (event) {
        console.log(event);
    };
    MenuItemComponent.prototype.getPagePopupLink = function (pagelink) {
        this.mainFrm.patchValue({ ItemLink: pagelink });
        //console.log("Received child's title: " + title);
    };
    MenuItemComponent = __decorate([
        core_1.Component({
            selector: 'menu-items',
            templateUrl: 'app/dashboard/MenuItem/MenuItem.component.html',
        }),
        __metadata("design:paramtypes", [forms_1.FormBuilder, authcrud_service_1.AuthcrudService])
    ], MenuItemComponent);
    return MenuItemComponent;
}());
exports.MenuItemComponent = MenuItemComponent;
//# sourceMappingURL=MenuItem.component.js.map