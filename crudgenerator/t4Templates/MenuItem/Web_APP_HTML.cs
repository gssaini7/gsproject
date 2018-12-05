
<div class=''>
   
    <div class=''>
        <div class="ui-widget-header" style="padding:4px 10px;border-bottom: 0 none">
            <i class="fa fa-search" style="margin:4px 4px 0 0"></i>
            <input #gb type="text" pInputText size="50" placeholder="Global Filter">
        </div>
        <p-dataTable #dt [value]="mainobjlist" [rows]="10" [paginator]="true" [pageLinks]="3" [rowsPerPageOptions]="[5,10,20]" [globalFilter]="gb"
                     selectionMode="single" (onRowSelect)="onRowSelect($event)">
 	<p-column field="MenuItemModelid" header="MenuItemModelid" [sortable]="true"></p-column>
	 
	<p-column field="ItemTitle" header="ItemTitle" [sortable]="true"></p-column>
	 
	<p-column field="ItemText" header="ItemText" [sortable]="true"></p-column>
	 
	<p-column field="ItemLink" header="ItemLink" [sortable]="true"></p-column>
	 
	<p-column field="Classli" header="Classli" [sortable]="true"></p-column>
	 
	<p-column field="Classa" header="Classa" [sortable]="true"></p-column>
	 
	<p-column field="Parentid" header="Parentid" [sortable]="true"></p-column>
	 
	<p-column field="MenuModel_MenuModelid" header="MenuModel_MenuModelid" [sortable]="true"></p-column>
	 
           
            <p-footer><div class="ui-helper-clearfix" style="width:100%"><button type="button" pButton icon="fa-plus" style="float:left" (click)="showDialogToAdd()" label="Add"></button></div></p-footer>
        </p-dataTable>



        <p-dialog header="MenuItem" [(visible)]="displayDialog" [responsive]="true" showEffect="fade" [modal]="true" [width]="500">
            <form novalidate [formGroup]="mainFrm">

			 	 <div class="form-group">

                    <label for="MenuItemModelid">MenuItemModelid</label>
                    <input type="text" class="form-control" id="MenuItemModelid"  placeholder="Enter MenuItemModelid" pInputText formControlName="MenuItemModelid">
                </div>
	 
	 <div class="form-group">

                    <label for="ItemTitle">ItemTitle</label>
                    <input type="text" class="form-control" id="ItemTitle"  placeholder="Enter ItemTitle" pInputText formControlName="ItemTitle">
                </div>
	 
	 <div class="form-group">

                    <label for="ItemText">ItemText</label>
                    <input type="text" class="form-control" id="ItemText"  placeholder="Enter ItemText" pInputText formControlName="ItemText">
                </div>
	 
	 <div class="form-group">

                    <label for="ItemLink">ItemLink</label>
                    <input type="text" class="form-control" id="ItemLink"  placeholder="Enter ItemLink" pInputText formControlName="ItemLink">
                </div>
	 
	 <div class="form-group">

                    <label for="Classli">Classli</label>
                    <input type="text" class="form-control" id="Classli"  placeholder="Enter Classli" pInputText formControlName="Classli">
                </div>
	 
	 <div class="form-group">

                    <label for="Classa">Classa</label>
                    <input type="text" class="form-control" id="Classa"  placeholder="Enter Classa" pInputText formControlName="Classa">
                </div>
	 
	 <div class="form-group">

                    <label for="Parentid">Parentid</label>
                    <input type="text" class="form-control" id="Parentid"  placeholder="Enter Parentid" pInputText formControlName="Parentid">
                </div>
	 
	 <div class="form-group">

                    <label for="MenuModel_MenuModelid">MenuModel_MenuModelid</label>
                    <input type="text" class="form-control" id="MenuModel_MenuModelid"  placeholder="Enter MenuModel_MenuModelid" pInputText formControlName="MenuModel_MenuModelid">
                </div>
	 

 <div class="form-group">
                    <label for="Remarks">Remarks</label>
                    <textarea class="form-control" id="Remarks" placeholder="Enter remarks" pInputText formControlName="remarks"></textarea>
                </div>
                <div class="form-group">
                    <label for="isPublishedc">Published</label>
                    <p-checkbox name="isPublishedc" value="isPublished" binary="true" formControlName="isPublished"></p-checkbox>

                </div>
                <p-footer>
                    <div class="ui-dialog-buttonpane ui-helper-clearfix">
                        <button type="button" pButton icon="fa-check" label="Save" (click)="save(mainFrm)" [disabled]="mainFrm.invalid"></button>
                    </div>
                </p-footer>
            </form>
        </p-dialog>



        <div *ngIf="msg" role="alert" class="alert alert-info alert-dismissible">
            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <span class="sr-only">Error:</span>
            {{msg}}
        </div>
    </div>
</div>
<p-growl [(value)]="msgs"></p-growl>


