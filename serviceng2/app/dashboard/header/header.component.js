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
//import { Router } from '@angular/router';
//import {Subscription} from 'rxjs/Subscription';
//import { Observable } from 'rxjs/Rx';
var user_service_1 = require("../../Services/user.service");
var authcrud_service_1 = require("../../Services/authcrud.service");
var global_1 = require("../../Shared/global");
var HeaderComponent = /** @class */ (function () {
    function HeaderComponent(userService, _crudService) {
        this.userService = userService;
        this._crudService = _crudService;
    }
    HeaderComponent.prototype.logout = function () {
        this.userService.logout();
        //this.router.navigate(['/login']);
        //this.router.navigate(['/home/account']);
        window.location.href = "/home/account";
    };
    HeaderComponent.prototype.ngOnInit = function () {
        //this.subscription = this.userService.authNavStatus$.subscribe(status => this.status = status);
        var _this = this;
        this.useritems = [
            {
                label: 'Change Password',
                //icon: 'fa-tachometer',
                routerLink: ['/profile']
            },
        ];
        this._crudService.get(global_1.Global.BASE_USER_ENDPOINT + "Admin/GetCurrentUserRoles")
            .subscribe(function (records) {
            _this.sidemenuitems = [];
            var newobj = {
                label: 'Dashboard',
                icon: 'fa-tachometer',
                routerLink: ['/']
            };
            _this.sidemenuitems.push(newobj);
            if (records.includes('DbMain')) {
                var newobj = {
                    label: 'Databases',
                    icon: 'fa-server',
                    routerLink: ['/dbs']
                };
                _this.sidemenuitems.push(newobj);
                var newobj = {
                    label: 'Roles',
                    icon: 'fa-user-secret',
                    routerLink: ['/roles']
                };
                _this.sidemenuitems.push(newobj);
                return;
            }
            records.forEach(function (item) {
                switch (item) {
                    case "Admin": {
                        var newobj = {
                            label: 'Admins',
                            icon: 'fa-users',
                            routerLink: ['/admins']
                        };
                        _this.sidemenuitems.push(newobj);
                        break;
                    }
                    case "ImageGallery": {
                        var newobj = {
                            label: 'Image Gallery',
                            icon: 'fa-image',
                            routerLink: ['/Imagegallery']
                        };
                        _this.sidemenuitems.push(newobj);
                        break;
                    }
                    case "Legitimate": {
                        var newobj = {
                            label: 'Roles',
                            icon: 'fa-user-secret',
                            routerLink: ['/roles']
                        };
                        _this.sidemenuitems.push(newobj);
                        var newobj = {
                            label: 'Blog Type',
                            icon: 'fa-list-alt',
                            routerLink: ['/blogtype']
                        };
                        _this.sidemenuitems.push(newobj);
                        //var newobjinternal1: MenuItem = {
                        //    label: 'SMS',
                        //    icon: 'fa-commenting-o',
                        //    routerLink: ['/settings/SMS']
                        //};
                        //var newobjinternal2: MenuItem = {
                        //    label: 'eMail',
                        //    icon: 'fa-envelope-o',
                        //    routerLink: ['/settings/Mail']
                        //};
                        //var newobjinternal3: MenuItem = {
                        //    label: 'Online Payment',
                        //    icon: 'fa-credit-card',
                        //    routerLink: ['/settings/OnlinePayment']
                        //};
                        var newobj = {
                            label: 'Settings',
                            icon: 'fa-cog',
                            //items: [newobjinternal1, newobjinternal2, newobjinternal3],
                            routerLink: ['/settings/SMS']
                        };
                        _this.sidemenuitems.push(newobj);
                        sessionStorage.setItem('isLegitimate', "true");
                        break;
                    }
                    case "SuperAdmin": {
                        sessionStorage.setItem('isMainAdmin', "true");
                        break;
                    }
                    case "Notification": {
                        var newobj = {
                            label: 'Daily Activity',
                            icon: 'fa-newspaper-o',
                            routerLink: ['/blog']
                        };
                        _this.sidemenuitems.push(newobj);
                        break;
                    }
                    case "Feedback": {
                        var newobj = {
                            label: 'Feedback',
                            icon: 'fa-envelope',
                            routerLink: ['/feedback']
                        };
                        _this.sidemenuitems.push(newobj);
                        break;
                    }
                }
            });
        });
        //this.sidemenuitems = [
        //    {
        //        label: 'Dashboard',
        //        icon: 'fa-tachometer',
        //        routerLink: ['/']
        //    },
        //    {
        //        label: 'Support',
        //        icon: 'fa-shopping-cart',
        //        routerLink: ['/supportadmin']
        //    },
        //    {
        //        label: 'Clients',
        //        icon: 'fa-file',
        //        routerLink: ['/clients']
        //    },
        //    {
        //        label: 'Pages',
        //        icon: 'fa-folder',
        //        routerLink: ['/pages']
        //    },
        //    {
        //        label: 'Settings',
        //        icon: 'fa-cog',
        //        routerLink: ['/settings']
        //    },
        //    {
        //        label: 'Roles',
        //        icon: 'fa-cog',
        //        routerLink: ['/roles']
        //    },
        //    {
        //        label: 'Menu',
        //        icon: 'fa-cog',
        //        routerLink: ['/menu']
        //    },
        //    {
        //        label: 'Layout',
        //        icon: 'fa-cog',
        //        routerLink: ['/layout']
        //    },
        //    {
        //        label: 'Image Gallery',
        //        icon: 'fa-cog',
        //        routerLink: ['/Imagegallery']
        //    },
        //    {
        //        label: 'Site',
        //        icon: 'fa-cog',
        //        routerLink: ['/site']
        //    },
        //    {
        //        label: 'Admins',
        //        icon: 'fa-cog',
        //        routerLink: ['/admins']
        //    },
        //    //{
        //    //    label: 'Manager',
        //    //    icon: 'fa-file-o',
        //    //    items: [{
        //    //        label: 'New',
        //    //        icon: 'fa-plus',
        //    //        items: [
        //    //            { label: 'Project' },
        //    //            { label: 'Other' },
        //    //        ]
        //    //    },
        //    //    { label: 'Open' },
        //    //    { separator: true },
        //    //    { label: 'Quit' }
        //    //    ]
        //    //},
        //    //{
        //    //    label: 'Edit',
        //    //    icon: 'fa-edit',
        //    //    items: [
        //    //        { label: 'Undo', icon: 'fa-mail-forward' },
        //    //        { label: 'Redo', icon: 'fa-mail-reply' }
        //    //    ]
        //    //},
        //    //{
        //    //    label: 'Help',
        //    //    icon: 'fa-question',
        //    //    items: [
        //    //        {
        //    //            label: 'Contents'
        //    //        },
        //    //        {
        //    //            label: 'Search',
        //    //            icon: 'fa-search',
        //    //            items: [
        //    //                {
        //    //                    label: 'Text',
        //    //                    items: [
        //    //                        {
        //    //                            label: 'Workspace'
        //    //                        }
        //    //                    ]
        //    //                },
        //    //                {
        //    //                    label: 'File'
        //    //                }
        //    //            ]
        //    //        }
        //    //    ]
        //    //},
        //    //{
        //    //    label: 'Actions',
        //    //    icon: 'fa-gear',
        //    //    items: [
        //    //        {
        //    //            label: 'Edit',
        //    //            icon: 'fa-refresh',
        //    //            items: [
        //    //                { label: 'Save', icon: 'fa-save' },
        //    //                { label: 'Update', icon: 'fa-save' },
        //    //            ]
        //    //        },
        //    //        {
        //    //            label: 'Other',
        //    //            icon: 'fa-phone',
        //    //            items: [
        //    //                { label: 'Delete', icon: 'fa-minus' }
        //    //            ]
        //    //        }
        //    //    ]
        //    //}
        //];
        //this.topmenuitems = [
        //    {
        //        label: 'File',
        //        icon: 'fa-file-o',
        //        items: [{
        //            label: 'New',
        //            icon: 'fa-plus',
        //            items: [
        //                { label: 'Project' },
        //                { label: 'Other' },
        //            ]
        //        },
        //        { label: 'Open' },
        //        { separator: true },
        //        { label: 'Quit' }
        //        ]
        //    },
        //    {
        //        label: 'Edit',
        //        icon: 'fa-edit',
        //        items: [
        //            { label: 'Undo', icon: 'fa-mail-forward' },
        //            { label: 'Redo', icon: 'fa-mail-reply' }
        //        ]
        //    },
        //    {
        //        label: 'Help',
        //        icon: 'fa-question',
        //        items: [
        //            {
        //                label: 'Contents'
        //            },
        //            {
        //                label: 'Search',
        //                icon: 'fa-search',
        //                items: [
        //                    {
        //                        label: 'Text',
        //                        items: [
        //                            {
        //                                label: 'Workspace'
        //                            }
        //                        ]
        //                    },
        //                    {
        //                        label: 'File'
        //                    }
        //                ]
        //            }
        //        ]
        //    },
        //    {
        //        label: 'Actions',
        //        icon: 'fa-gear',
        //        items: [
        //            {
        //                label: 'Edit',
        //                icon: 'fa-refresh',
        //                items: [
        //                    { label: 'Save', icon: 'fa-save' },
        //                    { label: 'Update', icon: 'fa-save' },
        //                ]
        //            },
        //            {
        //                label: 'Other',
        //                icon: 'fa-phone',
        //                items: [
        //                    { label: 'Delete', icon: 'fa-minus' }
        //                ]
        //            }
        //        ]
        //    },
        //    {
        //        label: 'Quit', icon: 'fa-minus'
        //    }
        //];
        //this.sidemenuitems = [
        //    {
        //        label: 'File',
        //        icon: 'fa-file-o',
        //        items: [{
        //            label: 'New',
        //            icon: 'fa-plus',
        //            items: [
        //                { label: 'Project' },
        //                { label: 'Other' },
        //            ]
        //        },
        //        { label: 'Open' },
        //        { separator: true },
        //        { label: 'Quit' }
        //        ]
        //    },
        //    {
        //        label: 'Edit',
        //        icon: 'fa-edit',
        //        items: [
        //            { label: 'Undo', icon: 'fa-mail-forward' },
        //            { label: 'Redo', icon: 'fa-mail-reply' }
        //        ]
        //    },
        //    {
        //        label: 'Help',
        //        icon: 'fa-question',
        //        items: [
        //            {
        //                label: 'Contents'
        //            },
        //            {
        //                label: 'Search',
        //                icon: 'fa-search',
        //                items: [
        //                    {
        //                        label: 'Text',
        //                        items: [
        //                            {
        //                                label: 'Workspace'
        //                            }
        //                        ]
        //                    },
        //                    {
        //                        label: 'File'
        //                    }
        //                ]
        //            }
        //        ]
        //    },
        //    {
        //        label: 'Actions',
        //        icon: 'fa-gear',
        //        items: [
        //            {
        //                label: 'Edit',
        //                icon: 'fa-refresh',
        //                items: [
        //                    { label: 'Save', icon: 'fa-save' },
        //                    { label: 'Update', icon: 'fa-save' },
        //                ]
        //            },
        //            {
        //                label: 'Other',
        //                icon: 'fa-phone',
        //                items: [
        //                    { label: 'Delete', icon: 'fa-minus' }
        //                ]
        //            }
        //        ]
        //    }
        //];
    };
    HeaderComponent.prototype.ngOnDestroy = function () {
        // prevent memory leak when component is destroyed
        //this.subscription.unsubscribe();
    };
    HeaderComponent = __decorate([
        core_1.Component({
            selector: 'admin-header',
            templateUrl: 'app/dashboard/header/header.component.html',
        }),
        __metadata("design:paramtypes", [user_service_1.UserService, authcrud_service_1.AuthcrudService])
    ], HeaderComponent);
    return HeaderComponent;
}());
exports.HeaderComponent = HeaderComponent;
//# sourceMappingURL=header.component.js.map