

import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { BlogModel, BlogTypeModel, TeacherBlogModel, newuser } from '../../../Models/credentials.interface';
import { AuthcrudService } from '../../../Services/authcrud.service';
import { Global } from '../../../Shared/global';
import { MenuItem } from 'primeng/components/common/api';

@Component({
    selector: 'list-type-crud',
    templateUrl: 'app/dashboard/CRUDType/ListType/ListType.component.html',
    //styleUrls: ['./login-form.component.scss']
})

export class ListTypeComponent implements OnInit {
    msg: string;
    blogtypelist: BlogTypeModel[];

    mainobjlist: BlogModel[];
    mainobj: BlogModel;
    indLoading: boolean = false;
    currentblogtype: BlogTypeModel;
    currentblogtypename: string;
    cmitems: MenuItem[];
    blogteacherrel: TeacherBlogModel;
    listadmins: newuser[];
    blogcreater: newuser;
    isAssignment: boolean = false;

    //@Input() myBlogType: string="Blog";

    constructor(private _crudService: AuthcrudService, private router: Router, private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.LoadData();
        this.route.params.subscribe(params => {
            this.currentblogtypename = params['type']; // --> Name must match wanted parameter
        });

        this.cmitems = [
            { label: 'Edit', icon: 'fa-edit', command: (event) => this.onRowSelect(this.mainobj) },
            { label: 'Forward', icon: 'fa-share-square', command: (event) => this.onForward(this.mainobj) },
        ];

        let ismainadmin = sessionStorage.getItem('isMainAdmin');
        if (ismainadmin === "true") {
            this.LoadAdmins();
        }
    }

    LoadAdmins() {
        this._crudService.get(Global.BASE_USER_ENDPOINT + "Admin/Admins")
            .subscribe(records => {
                this.listadmins = [];
                this.loopthroughrecords(records);
            },
            error => this.msg = <any>error);
    }

    loopthroughrecords(records: any) {
        records.forEach((item: any) => {
            this.listadmins.push(item);
            if (item.AdminChildren != null) {
              this.loopthroughrecords(item.AdminChildren);
            }
            
        });
    }

    LoadData(): void {
        this.indLoading = true;
        this.blogtypelist = null;

        this._crudService.get(Global.BASE_USER_ENDPOINT + "BlogType/GetAll")
            .subscribe(records => {
                this.blogtypelist = records;


                if (this.currentblogtypename === undefined) {
                    this.currentblogtype = records[0];
                }
                else {
                    let blogstypes = records.filter((a: BlogTypeModel) => a.BlogTypeName === this.currentblogtypename)[0];
                    if (blogstypes !== undefined)
                        this.currentblogtype = blogstypes;
                    else
                        this.currentblogtype = records[0];
                }

                this.LoadDataBlogs();
                this.indLoading = false;
            },
            error => this.msg = <any>error);
    }

    OnChangeAdmin() {
        this.LoadDataBlogs();
    }
   
    
    LoadDataBlogs(): void {
        this.msg = "";
        this.indLoading = true;
        this.mainobjlist = null;
        this.isAssignment = false;

        //let newblogteacher: TeacherBlogModel = { BlogModelid: this.currentblogtype.BlogTypeModelid, TeacherId: null };
        //this.blogteacherrel = newblogteacher;
        let createrid = "";
        if (this.blogcreater !== undefined) {
            createrid = this.blogcreater.UserID;
        }
        this._crudService.get(Global.BASE_USER_ENDPOINT + "blog/GetAll?id=" + this.currentblogtype.BlogTypeModelid + "&createrid=" + createrid)
            .subscribe(records => {
                this.mainobjlist = records;
                

                this.indLoading = false;

                let checkisassignment = records.filter((a: any) => a.SubjectModelid !== null);
                if (checkisassignment.length !== 0)
                    this.isAssignment = true;


            },
            error => this.msg = <any>error);
        this.router.navigate(['/blog', this.currentblogtype.BlogTypeName]);

    }
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

    addNewPage() {
        this.router.navigate(['/detail', this.currentblogtype.BlogTypeName, this.currentblogtype.BlogTypeModelid, 'new']);
    }

    onForward(event: any) {
        this.router.navigate(['/forward', event.BlogModelid]);
    }

    onRowSelect(event: any) {
        
        this.router.navigate(['/detail', this.currentblogtype.BlogTypeName, this.currentblogtype.BlogTypeModelid, event.BlogModelid]);
    }

    changetab(selectedblogtype: BlogTypeModel) {
        this.currentblogtype = selectedblogtype;
        //this.currentblogtypename = selectedblogtype.BlogTypeName;
        this.LoadDataBlogs();
    }

 
}


