

import { Component, OnInit, Input } from '@angular/core';
import { MenuItemModel, MenuModel } from '../../Models/credentials.interface';
import { AuthcrudService } from '../../Services/authcrud.service';
import { Global } from '../../Shared/global';
import { Message } from 'primeng/components/common/api';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DBOperation } from '../../Shared/enum';
//import { TreeNode } from 'primeng/api';
import { MenuItem, TreeNode, SelectItem } from 'primeng/components/common/api';

import { SortByPipe } from '../../Shared/sortby.pipe';

@Component({
    selector: 'role-assign',
    templateUrl: 'app/dashboard/RoleAssign/RoleAssign.component.html',
    //styleUrls: ['./login-form.component.scss']
})

export class RoleAssignComponent implements OnInit {
    

    msgs: Message[] = [];
    msg: string;
    mainFrm: FormGroup;
    displayDialog: boolean;
    //displayDialogOrder: boolean;

    
    mainobjlist: MenuItemModel[];
    mainobjItem: MenuItemModel;
    //sortobjlist: MenuItemModel[];

    indLoading: boolean = false;
    dbops: DBOperation;
    isNewForm: boolean = false;
    currentmenuid: string;

    files2: TreeNode[];

    firstlevelmenu: MenuItemModel[];
    itemsmi: MenuItem[];

    selectedFile: TreeNode;
    //postionslist: SelectItem[] = [];
    nullparentid: string = '00000000-0000-0000-0000-000000000000';


    constructor(private fb: FormBuilder, private _crudService: AuthcrudService) { }

    ngOnInit(): void {
        this.InitilizeFormItems();
        //this.LoadData();
        //this.showalerttest();

        this.itemsmi = [
            { label: 'View', icon: 'fa-search', command: (event) => this.onRowSelect(this.selectedFile.data) },
            { label: 'Delete', icon: 'fa-close', command: (event) => this.delete(this.selectedFile) },
            //{ label: 'Sort', icon: 'fa-random', command: (event) => this.sortNode(this.selectedFile) }
        ];
    }

    InitilizeFormItems() {
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
    }

    

    LoadData(id: any): void {
                this.msg = '';

        this.files2 = null;
        if (id == undefined)
            return;
        this.currentmenuid = id;
        this.indLoading = true;
        //this.mainobjlist = null;
        this._crudService.get(Global.BASE_USER_ENDPOINT + "MenuItem/GetAll?id=" + this.currentmenuid)
            .subscribe(records => {
                this.mainobjlist = records;

                let sortPipeFilter = new SortByPipe();
                sortPipeFilter.transform(records, 'itemPriority')
                

                this.indLoading = false;
                    //if (item.Parentid == '00000000-0000-0000-0000-000000000000')

                //this.firstlevelmenu = records.filter((menuitem: any) => menuitem.Parentid === '00000000-0000-0000-0000-000000000000');
                //this.firstlevelmenu.push(null);

                //this.LoadParentMenuDropDown();



                var maintree: TreeNode[]=[];
                records.forEach((item: any) => {
                    if (item.Parentid === this.nullparentid) { 
                        var c: TreeNode[] = [];
                        records.filter((menuitem: any) => menuitem.Parentid === item.MenuItemModelid).forEach((item2: any) => {
                            var b: TreeNode = {
                                data: item2,
                            };
                            c.push(b);
                        });

                    var a: TreeNode = {
                        data: item,
                        children: c
                    };
                    maintree.push(a);
                   }
                });

                this.files2 = maintree;
                //this.msg = '';
            },
            error => this.msg = <any>error);
    }

    LoadParentMenuDropDown() {
        this.firstlevelmenu = this.mainobjlist.filter((menuitem: any) => menuitem.Parentid === this.nullparentid && menuitem.MenuModelid === this.currentmenuid);
        this.firstlevelmenu.push(null);

        //this.fillpositionlist({value: null });
    }


    onSubmit(formData: any) {
        if (this.currentmenuid == undefined)
            return;
        formData.MenuModelid = this.currentmenuid;

        this.msg = "";
        switch (this.dbops) {
            case DBOperation.create:
                if (formData.isPublished == null || formData.isPublished == '')
                    formData.isPublished = false;
                var curl = Global.BASE_USER_ENDPOINT + "MenuItem/Create";
                this._crudService.post(curl, formData).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.LoadData(this.currentmenuid);
                            this.msg = "Data successfully added.";
                            
                        }
                        else {
                            this.msg = "There is some issue in saving records, please contact to system administrator!"
                        }
                        this.displayDialog = false;
                    },
                    error => {
                        this.msg = error;
                        this.showError(this.msg);
                    }
                );
                break;
            case DBOperation.update:
                var curl = Global.BASE_USER_ENDPOINT + "MenuItem/Edit";
                this._crudService.post(curl, formData).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.LoadData(this.currentmenuid);

                            this.msg = "Data successfully updated.";
                        }
                        else {
                            this.msg = "There is some issue in saving records, please contact to system administrator!"
                        }
                        this.displayDialog = false;
                    },
                    error => {
                        this.msg = error;
                        this.showError(this.msg);
                    }
                );
                break;
            case DBOperation.delete:
                var curl = Global.BASE_USER_ENDPOINT + "MenuItem/Delete";

                this._crudService.post(curl, formData).subscribe(
                    data => {

                        if (data == 1) //Success
                        {
                            this.LoadData(this.currentmenuid);

                            this.msg = "Data successfully deleted.";
                        }
                        else {
                            this.msg = "There is some issue in deleting records, please contact to system administrator!"
                        }
                        this.displayDialog = false;
                    },
                    error => {
                        this.msg = error;
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
        this.dbops = DBOperation.create;
        this.mainobjItem = null;
        this.LoadParentMenuDropDown();
        //this.fillpositionlist({value: null });

        //this.postionslist.push({ label: (this.postionslist.length + 1).toString(), value: this.postionslist.length + 1 });
    }

    save(formData: any) {
        this.SetControlsState(true);
        console.log(formData._value);
        if (formData._value.ParentMenu != null)
            formData._value.Parentid = formData._value.ParentMenu.MenuItemModelid;
        else
            formData._value.Parentid = null;
        this.onSubmit(formData._value);
    }

    onRowSelect(event: any) {
        
        this.mainobjItem = event;
       
        this.mainFrm.patchValue(this.mainobjItem);

        var selectedmenu = this.mainobjlist.filter(mid => mid.MenuItemModelid === this.mainobjItem.Parentid)[0];

        this.mainFrm.patchValue({ ParentMenu: selectedmenu  });
        
        //this.mainFrm.patchValue({ itemPriority: this.mainobjItem.itemPriority });


        this.displayDialog = true;
        this.dbops = DBOperation.update;
        this.isNewForm = false;
        this.currentmenuid = event.MenuModelid;
        //this.LoadData(this.currentmenuid);
        this.LoadParentMenuDropDown();

        this.firstlevelmenu = this.firstlevelmenu.filter(obj => obj !== this.mainobjItem); // remove current menu

        //this.fillpositionlist({ value: selectedmenu });

    }

    delete(event: any) {
        if (confirm("Are you sure to delete ")) {
            this.dbops = DBOperation.delete;
            
            this.onSubmit(event.data);
        }
    }

    showError(errormsg: any) {
        this.msgs = [];
        this.msgs.push({ severity: 'error', summary: 'Error Message', detail: errormsg });
    }

    //sortNode(event: any) {
    //    this.displayDialogOrder = true;
    //    this.sortobjlist = this.mainobjlist.filter(p => p.Parentid === event.data.Parentid);
    //}
    nodeSelect(event: any) {
        this.onRowSelect(event.node.data);
        //console.log(event.node.data);
        //this.msgs = [];
        //this.msgs.push({ severity: 'info', summary: 'Node Selected', detail: event.node.data.name });
    }

    nodeUnselect(event: any) {
        this.msgs = [];
        this.msgs.push({ severity: 'info', summary: 'Node Unselected', detail: event.node.data.name });
    }

    nodeExpand(event: any) {
        if (event.node) {
            //in a real application, make a call to a remote url to load children of the current node and add the new nodes as children
            //this.nodeService.getLazyFilesystem().then(nodes => event.node.children = nodes);
            console.log(event.node);
        }
        this.msgs = [];
        this.msgs.push({ severity: 'info', summary: 'Node Expand', detail: event.node.data.name });
    }

    viewNode(node: TreeNode) {
        this.msgs = [];
        this.msgs.push({ severity: 'info', summary: 'Node Selected', detail: node.data.name });
    }

    deleteNode(node: TreeNode) {
        node.parent.children = node.parent.children.filter(n => n.data !== node.data);
        this.msgs = [];
        this.msgs.push({ severity: 'info', summary: 'Node Deleted', detail: node.data.name });
    }

    orderItemList(event: any) {
        console.log(event);
    }

    getPagePopupLink(pagelink: string): void {
        this.mainFrm.patchValue({ ItemLink: pagelink });
        //console.log("Received child's title: " + title);
    }

    //fillpositionlist(event: any) {
    //    console.log(event);
    //    this.postionslist = [];
    //    var parentid = this.nullparentid;
    //    if (event.value  != undefined) {
           
    //        if (event.value !== null) {
    //            //if (event.value.Parentid === this.nullparentid)
    //            //    parentid = event.value.MenuItemModelid;
    //            // else
    //            parentid = event.value.MenuItemModelid;

    //        }
    //    }
    //    var parentlistlength = this.mainobjlist.filter(p => p.Parentid === parentid && p.MenuModelid === this.currentmenuid).length;
    //    for(var i = 1; i <= parentlistlength; i++)
    //    {
    //        this.postionslist.push({ label: i.toString(), value: i });
    //    }
        
    //    //this.mainFrm.patchValue({ itemPriority: null });
        
    //}
}


