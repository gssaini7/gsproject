
import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

import { newuser, RolesDetailModel } from '../../Models/credentials.interface';
import { AuthcrudService } from '../../Services/authcrud.service';
import { Global } from '../../Shared/global';
import { DataTableModule } from 'primeng/components/datatable/datatable';
import { Message } from 'primeng/components/common/api';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DBOperation } from '../../Shared/enum';
import { EqualsValidator } from '../../Services/equals.validators';
import { SelectItem, TreeNode, MenuItem } from 'primeng/components/common/api';


@Component({
  //selector: 'app-register-form',
    templateUrl: 'app/dashboard/Admins/Admins.component.html',
  //styleUrls: ['./login-form.component.scss']
})

export class AdminsComponent implements OnInit {
    msgs: Message[] = [];
    msg: string;
    mainFrm: FormGroup;
    displayDialog: boolean;
    mainobjlist: newuser[];
    mainobj: newuser;
    indLoading: boolean = false;
    dbops: DBOperation;
    isNewForm: boolean = false;
    isnewuser: boolean = false;

    //isFirstUser: boolean = false;

   

    ExpiryItems: SelectItem[];
    data1: TreeNode[];
    itemsmi: MenuItem[];
    selectedNode: TreeNode;

    roleslist: string[];
    selectedRoles: string[] = [];

    constructor(private fb: FormBuilder, private _crudService: AuthcrudService, private router: Router) { }

    ngOnInit(): void {
        this.InitilizeFormItems();
        this.LoadData();
        
    }

    InitilizeFormItems() {
        this.mainFrm = this.fb.group({
            nameofuser: ['', Validators.required],
            Mobile: ['', Validators.required],
            Email: ['', Validators.required],
            remarks: [''],
            parent: [''],
            UserID:[''],
        });
        //this.itemsmi = [
        //    { label: 'Add Child', icon: 'fa-plus', command: (event) => this.showDialogToAdd() },
        //    { label: 'View', icon: 'fa-search', command: (event) => this.onRowSelect(this.selectedNode.data), disabled: this.isFirstUser },
        //    { label: 'Roles', icon: 'fa-user-plus', command: (event) => this.showDialogRoles(), disabled: this.isFirstUser },

        //    //{ label: 'Delete', icon: 'fa-close', command: (event) => ""},
        //];
       
    }

    LoadData(): void {
        this.indLoading = true;
        this.mainobjlist = null;
        this._crudService.get(Global.BASE_USER_ENDPOINT + "Admin/Admins")
            .subscribe(records => {
                this.mainobjlist = records;
                this.setformattreedata();
                this.indLoading = false;
            },
            error => this.msg = <any>error);
    }

   

    //settreenode(ipdata: any) {
    //    var opdata: TreeNode = {
    //        label: ipdata.nameofuser,
    //        expanded: true,
    //        data: { 'email': ipdata.Email, 'mobile': ipdata.Mobile },
    //        children:[],
    //    };
    //    return opdata;
    //}
    settreenode(ipdata: any) {
        var opdata: TreeNode = {
            label: ipdata.nameofuser,
            //expanded: true,
            data: ipdata,
           // children: [],
        };
        return opdata;
    }

    loopthroughrecords(records: any) {
        var maintree: TreeNode[] = [];
        records.forEach((item: any) => {
            var node = this.settreenode(item);
            if (item.AdminChildren != null) {
                var childnodes = this.loopthroughrecords(item.AdminChildren);
                node.children = childnodes;
                node.expanded = true;
               
            }
            
            maintree.push(node);
        });
        //maintree.push(this.addtreenode());
        return maintree;
    }

    setformattreedata() {
        var records = this.mainobjlist;
        var bb = this.loopthroughrecords(records);
        
        this.data1 = bb;

    }


    onSubmit(formData: any) {
        this.msg = "";
        
        switch (this.dbops) {
            case DBOperation.create:
                
                var curl = Global.BASE_USER_ENDPOINT + "Admin/Create";
               
                this._crudService.post(curl, formData._value).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "Data successfully added.";
                            this.LoadData();
                        }
                        else {
                            this.msg = "There is some issue in saving records, please contact to system administrator!"
                        }
                        this.displayDialog = false;
                    },
                    error => {
                        this.msg = error;
                        this.showError('error',this.msg);
                    }
                );
                break;
            case DBOperation.update:

                var curl = Global.BASE_USER_ENDPOINT + "Admin/UpdateUser";
                this._crudService.post(curl, formData._value).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "Data successfully updated.";
                            this.LoadData();
                        }
                        else {
                            this.msg = "There is some issue in saving records, please contact to system administrator!"
                        }
                        this.displayDialog = false;
                    },
                    error => {
                        this.msg = error;
                        this.showError('error', this.msg);

                    }
                );
                break;
            case DBOperation.delete:
                var curl = Global.BASE_USER_ENDPOINT + "Admin/Delete";

                this._crudService.post(curl, formData._value).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "Data successfully deleted.";
                            this.LoadData();
                        }
                        else {
                            this.msg = "There is some issue in deleting records, please contact to system administrator!"
                        }
                        this.displayDialog = false;
                    },
                    error => {
                        this.msg = error;
                        this.showError('error', this.msg);

                    }
                );
                break;

        }
    }

    SetControlsState(isEnable: boolean) {
        isEnable ? this.mainFrm.enable() : this.mainFrm.disable();
    }

    showDialogToAdd() {
        this.isNewForm = true;
        this.mainFrm.reset();
        this.displayDialog = true;
        this.isnewuser = true;

        this.dbops = DBOperation.create;
    }

    save(formData: any) {
        this.SetControlsState(true);
        
        formData._value.parentid = this.selectedNode.data.UserID;
        
        this.onSubmit(formData);
    }

    onRowSelect(event: any) {
        
        this.mainobj = event;
        this.mainFrm.patchValue(this.mainobj);
        this.displayDialog = true;
        this.isnewuser = true;

        this.dbops = DBOperation.update;
        this.isNewForm = false;
    }

    nodeSelect(event: any) {
        //this.onRowSelect(event.node.data);
    }

    nodeRightClick(event: any) {


        var firstuser = this.mainobjlist[0];
        let firstdisabled = false;

        //this.isFirstUser = false;
        if (event.node.data.UserID === firstuser.UserID) {
            firstdisabled = true;

            let ismainadmin = sessionStorage.getItem('isLegitimate');
            if (ismainadmin === "true") {
                firstdisabled = false;
            }
        }
        this.itemsmi = [
            { label: 'Add Child', icon: 'fa-user-plus', command: (event) => this.showDialogToAdd() },
            { label: 'View', icon: 'fa-search', command: (event) => this.onRowSelect(this.selectedNode.data), disabled: firstdisabled },
            { label: 'Roles', icon: 'fa-user-secret', command: (event) => this.showDialogRoles(), disabled: firstdisabled },
            { label: 'Assign Class', icon: 'fa-address-card-o', command: (event) => this.AdminDetail(this.selectedNode.data), disabled: firstdisabled },


            //{ label: 'Delete', icon: 'fa-close', command: (event) => ""},
        ];
    
    }

    delete() {
        if (confirm("Are you sure to delete? All the child users also be deleted.")) {
            this.dbops = DBOperation.delete;
            this.onSubmit(this.mainFrm);
        }
    }

    AdminDetail(data: any) {
        this.router.navigate(['/admindetail', data.UserID]);
    }

    showDialogRoles() {
        this.displayDialog = true;
        this.isnewuser = false;
        this.LoadDataRoles();
    }
    LoadDataRoles(): void {
        this.indLoading = true;
        this.roleslist = null;
        this.selectedRoles = null;
        if (this.selectedNode.data.parentid !== "00000000-0000-0000-0000-000000000000") {
            this._crudService.get(Global.BASE_USER_ENDPOINT + "Admin/GetRolesByID?id=" + this.selectedNode.data.parentid)
                .subscribe(records => {
                    this.roleslist = records;
                    this._crudService.get(Global.BASE_USER_ENDPOINT + "Admin/GetRolesByID?id=" + this.selectedNode.data.UserID)
                        .subscribe(records => {

                            this.selectedRoles = records;
                            this.indLoading = false;
                        },
                        error => this.msg = <any>error);
                    this.indLoading = false;
                },
                error => this.msg = <any>error);
        }
        else {
            this._crudService.get(Global.BASE_USER_ENDPOINT + "Admin/GetRoles")
                .subscribe(records => {
                    this.roleslist = records.map((a: any) => a.Name);
                    this._crudService.get(Global.BASE_USER_ENDPOINT + "Admin/GetRolesByID?id=" + this.selectedNode.data.UserID)
                        .subscribe(records => {
                            this.selectedRoles = records;
                            this.indLoading = false;
                        },
                        error => this.msg = <any>error);
                    this.indLoading = false;
                },
                error => this.msg = <any>error);
        }
    }

    updateroles(): void {
        var formData = {
            UserID: this.selectedNode.data.UserID,
            Rolenames: this.selectedRoles
        };
       
        var curl = Global.BASE_USER_ENDPOINT + "Admin/AssignRole";
        this._crudService.post(curl, formData).subscribe(
            data => {
                if (data == 1) //Success
                {
                    this.msg = "Data successfully added.";
                }
                else {
                    this.msg = "There is some issue in saving records, please contact to system administrator!"
                }
                this.displayDialog = false;
            },
            error => {
                this.msg = error;
                this.showError('error',this.msg);
            }
        );
    }


    showError(severity:any, errormsg:any) {
        this.msgs = [];
        this.msgs.push({ severity: severity, summary: 'Message', detail: errormsg });
    }
   
   
   
}


