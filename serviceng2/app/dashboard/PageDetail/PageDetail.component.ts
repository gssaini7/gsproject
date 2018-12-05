
import { Component, OnInit, Pipe, ViewChild } from '@angular/core';
import { PageModel } from '../../Models/credentials.interface';
import { ActivatedRoute } from '@angular/router';
import { AuthcrudService } from '../../Services/authcrud.service';
import { Global } from '../../Shared/global';
//import { DataTableModule } from 'primeng/components/datatable/datatable';
import { Message } from 'primeng/components/common/api';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { DBOperation, PageContentType } from '../../Shared/enum';
import { EqualsValidator } from '../../Services/equals.validators';
import { SelectItem } from 'primeng/components/common/api';
import { Dropdown } from "primeng/components/dropdown/dropdown";
//import  Quill  from  "quill";

@Component({
  //selector: 'app-register-form',
    templateUrl: 'app/dashboard/PageDetail/PageDetail.component.html',
  //styleUrls: ['./login-form.component.scss']
    //styleUrls: ["../node_modules/quill/dist/quill.core.css", "../node_modules/quill/dist/quill.snow.css"],
    

})

export class PageDetailComponent implements OnInit {

    msgs: Message[] = [];
    msg: string;
    public mainFrm: FormGroup;
    //public pcontentForm: FormGroup;
    displayDialog: boolean;
    //mainobjlist: PageModel[];
    //mainobj: PageModel;
    indLoading: boolean = false;
    dbops: DBOperation;
    isNewForm: boolean = false;
    currentid: string;
    urltext: string;
    //selectedurl: string="";
    conrentoptions: SelectItem[];

    isMultipleContent: boolean = false;

    @ViewChild('dropDownPCType')
    dropDownPCType: Dropdown;
    
    constructor(private fb: FormBuilder, private _crudService: AuthcrudService, private route: ActivatedRoute) { }

    ngOnInit(): void {
        //this.InitilizeFormItems();
        this.isMultipleContent = false;
        this.route.params.subscribe(params => {
            this.currentid = params['id']; // --> Name must match wanted parameter
        });
        this.LoadData();

        this.conrentoptions = [
            { label: 'Add Content Type', value: PageContentType.none },
            { label: 'HTML', value: PageContentType.htmlcode },
            { label: 'Text', value: PageContentType.simpletext },
            { label: 'Image', value: PageContentType.image },
        ];
    }

    InitilizeFormItems() {
        this.mainFrm = this.fb.group({
            PageModelid: [''],
            PageTitle: ['', Validators.required],
            pageurl: ['', Validators.required],
            pcontents: this.fb.array([]),
            //pcontents: this.fb.array([this.initPContents_fn(PageContentType.htmlcode,""),]),
            pagecontent: [''],
            isPublished: [''],
            remarks: [''],
        });
    }

    initPContents_fn(pctype: any, pcon: any) {
        var pccontent = this.initPContents();
        pccontent.setValue({ PCType: pctype, PContent: pcon });
        return pccontent;
    }

    initPContents() {
        return this.fb.group({
            PCType: [''],
            PContent: ['']
        });
    }

   

    addAnotherContentFormd(event: any, lcontent: any) {

        if (event === PageContentType.none)
            return;
        if (this.isMultipleContent)
            this.dropDownPCType.updateSelectedOption(PageContentType.none);
        const control = <FormArray>this.mainFrm.controls['pcontents'];
        //const addrCtrl = this.initPContents();
        const addrCtrl = this.initPContents_fn(event, lcontent);

        control.push(addrCtrl);

        /* subscribe to individual address value changes */
        // addrCtrl.valueChanges.subscribe(x => {
        //   console.log(x);
        // })
    }

    //addAnotherContentForm() {

    //    const control = <FormArray>this.mainFrm.controls['pcontents'];
    //    const addrCtrl = this.initPContents();
    //    control.push(addrCtrl);

    //    /* subscribe to individual address value changes */
    //    // addrCtrl.valueChanges.subscribe(x => {
    //    //   console.log(x);
    //    // })
    //}
    removeContentForm(i: number) {
       
        var r = confirm("Are you sure want to delete?");
        if (r == true) {
            const control = <FormArray>this.mainFrm.controls['pcontents'];
            control.removeAt(i);
        }
    }

    LoadData(): void {
        this.indLoading = true;
        
        this.InitilizeFormItems();
        if (this.currentid != "0") {
            
            this._crudService.get(Global.BASE_USER_ENDPOINT + "page/GetPage?id=" + this.currentid)
                .subscribe(records => {
                    this.mainFrm.patchValue(records);
                    var a = records.pagecontent;
                    var jsoncontent = JSON.parse(a);
                    jsoncontent.pcary.forEach((avalue: any) => {
                        this.addAnotherContentFormd(avalue.PCType, avalue.PContent);
                    });
                    
                    this.indLoading = false;
                    this.dbops = DBOperation.update;
                },
                error => this.msg = <any>error);
        }
        else {
            this.addAnotherContentFormd(PageContentType.htmlcode, "");
            this.dbops = DBOperation.create;
        }
        

    }


    onSubmit(formData: any) {
        this.msg = "";
        
        formData._value.pagecontent = JSON.stringify({pcary: formData._value.pcontents });
        switch (this.dbops) {
            case DBOperation.create:
               
                var curl = Global.BASE_USER_ENDPOINT + "page/Create";
                if (formData._value.isPublished == null || formData._value.isPublished == '')
                    formData._value.isPublished = false;
                this._crudService.post(curl, formData._value).subscribe(
                    data => {
                        if (data == 1) //Success
                        {
                            this.msg = "Data successfully added.";
                            this.LoadData();
                        }
                        else {
                            this.msg = "There is some issue in saving records, please contact to system administrator!";
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

                var curl = Global.BASE_USER_ENDPOINT + "page/Edit";
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
                        this.showError(this.msg);
                    }
                );
                break;
            //case DBOperation.delete:
            //    var curl = Global.BASE_USER_ENDPOINT + "item/Delete";

            //    this._crudService.post(curl, formData._value).subscribe(
            //        data => {
            //            if (data == 1) //Success
            //            {
            //                this.msg = "Data successfully deleted.";
            //                this.LoadData();
            //            }
            //            else {
            //                this.msg = "There is some issue in deleting records, please contact to system administrator!"
            //            }
            //            this.displayDialog = false;
            //        },
            //        error => {
            //            this.msg = error;
            //        }
            //    );
            //    break;

        }
    }

    SetControlsState(isEnable: boolean) {
        isEnable ? this.mainFrm.enable() : this.mainFrm.disable();
    }

    //showDialogToAdd() {
    //    this.isNewForm = true;
    //    this.mainFrm.reset();
    //    this.displayDialog = true;
    //    this.dbops = DBOperation.create;
    //}

    save(formData: any) {
        //this.SetControlsState(true);
        //this.dbops = DBOperation.create;
        this.onSubmit(formData);
    }

    //onRowSelect(event: any) {
    //    this.mainobj = event.data;
    //    this.mainFrm.patchValue(this.mainobj);
    //    this.displayDialog = true;
    //    this.dbops = DBOperation.update;
    //    this.isNewForm = false;
    //}

    delete() {
        if (confirm("Are you sure to delete ")) {
            this.dbops = DBOperation.delete;
            this.onSubmit(this.mainFrm);
        }
    }

    onKey(txtvalue: any) {
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
    }

    onBlurTitle(txtval: any) {
        this.urltext = this.onKey(txtval);
    }
    onBlurURL(txtval: any) {
        this.mainFrm.patchValue({ pageurl: this.onKey(txtval) });
        //this.selectedurl = this.onKey(txtval);
    }
   
    seturl() {
        var r = confirm("Are you sure want to add URL?");
        if (r == true) {
            //this.selectedurl = this.urltext;
            this.mainFrm.patchValue({ pageurl: this.urltext });
            this.urltext = "";

        } 
    }

    showError(errormsg:any) {
        this.msgs = [];
        this.msgs.push({ severity: 'error', summary: 'Error Message', detail: errormsg });
    }

   
}


