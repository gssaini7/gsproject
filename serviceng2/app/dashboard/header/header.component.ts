import { Component, OnInit, OnDestroy } from '@angular/core';
//import { Router } from '@angular/router';

//import {Subscription} from 'rxjs/Subscription';
//import { Observable } from 'rxjs/Rx';
import { UserService } from '../../Services/user.service';
import { MenuItem } from 'primeng/components/common/api';
import { AuthcrudService } from '../../Services/authcrud.service';
import { Global } from '../../Shared/global';



@Component({
    selector: 'admin-header',
    templateUrl: 'app/dashboard/header/header.component.html',
   

    //styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit, OnDestroy {

    //status: boolean;
    //private subscription: Subscription;
    sidemenuitems: MenuItem[];
    topmenuitems: MenuItem[];
    useritems: MenuItem[];


   
    constructor(private userService: UserService, private _crudService: AuthcrudService) {
       
    }

    logout() {
        this.userService.logout();
        //this.router.navigate(['/login']);
        //this.router.navigate(['/home/account']);
        window.location.href = "/home/account";
    }

    ngOnInit() {
        //this.subscription = this.userService.authNavStatus$.subscribe(status => this.status = status);

        this.useritems = [
            {
                label: 'Change Password',
                //icon: 'fa-tachometer',
                routerLink: ['/profile']

            },
        ];

        this._crudService.get(Global.BASE_USER_ENDPOINT + "Admin/GetCurrentUserRoles")
            .subscribe(records => {

               
                this.sidemenuitems = [];
                var newobj: MenuItem = {
                    label: 'Dashboard',
                    icon: 'fa-tachometer',
                    routerLink: ['/']
                };
                this.sidemenuitems.push(newobj);

                if (records.includes('DbMain')) {
                    var newobj: MenuItem = {
                        label: 'Databases',
                        icon: 'fa-server',
                        routerLink: ['/dbs']
                    };
                    this.sidemenuitems.push(newobj);
                    var newobj: MenuItem = {
                        label: 'Roles',
                        icon: 'fa-user-secret',
                        routerLink: ['/roles']
                    };
                    this.sidemenuitems.push(newobj);
                    return;
                }



                records.forEach((item: any) => {
                    switch (item) {
                      
                        case "Admin": {
                            var newobj: MenuItem = {
                                label: 'Admins',
                                icon: 'fa-users',
                                routerLink: ['/admins']
                            };
                            this.sidemenuitems.push(newobj);

                            break;
                        }
                        case "ImageGallery": {
                            var newobj: MenuItem = {
                                label: 'Image Gallery',
                                icon: 'fa-image',
                                routerLink: ['/Imagegallery']
                            };
                            this.sidemenuitems.push(newobj);
                            break;
                        }
                       
                        case "Legitimate": {
                            var newobj: MenuItem = {
                                label: 'Roles',
                                icon: 'fa-user-secret',
                                routerLink: ['/roles']
                            };
                            this.sidemenuitems.push(newobj);

                            var newobj: MenuItem = {
                                label: 'Blog Type',
                                icon: 'fa-list-alt',
                                routerLink: ['/blogtype']

                            };
                            this.sidemenuitems.push(newobj);

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
                            var newobj: MenuItem = {
                                label: 'Settings',
                                icon: 'fa-cog',
                                //items: [newobjinternal1, newobjinternal2, newobjinternal3],
                                routerLink: ['/settings/SMS']
                            };
                            this.sidemenuitems.push(newobj);



                            sessionStorage.setItem('isLegitimate', "true");

                            break;
                        }
                        case "SuperAdmin": {
                            sessionStorage.setItem('isMainAdmin', "true");
                            break;
                        }
                        case "Notification": {
                            var newobj: MenuItem = {
                                label: 'Daily Activity',
                                icon: 'fa-newspaper-o',
                                routerLink: ['/blog']
                            };
                            this.sidemenuitems.push(newobj);
                            break;
                        }
                        case "Feedback": {
                            var newobj: MenuItem = {
                                label: 'Feedback',
                                icon: 'fa-envelope',
                                routerLink: ['/feedback']
                            };
                            this.sidemenuitems.push(newobj);
                            break;
                        }
                       
                       

                        //default: {
                        //    console.log("Invalid choice");
                        //    break;
                        //}
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

    }

    ngOnDestroy() {
        // prevent memory leak when component is destroyed
        //this.subscription.unsubscribe();
    }
}
