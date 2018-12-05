
<div class=''>
   
    <div class=''>
        <div class="ui-widget-header" style="padding:4px 10px;border-bottom: 0 none">
            <i class="fa fa-search" style="margin:4px 4px 0 0"></i>
            <input #gb type="text" pInputText size="50" placeholder="Global Filter">
        </div>
        <p-dataTable #dt [value]="mainobjlist" [rows]="10" [paginator]="true" [pageLinks]="3" [rowsPerPageOptions]="[5,10,20]" [globalFilter]="gb"
                     selectionMode="single" (onRowSelect)="onRowSelect($event)">
 	<p-column field="TCSSRelModelid" header="TCSSRelModelid" [sortable]="true"></p-column>
	 
	<p-column field="Teacherid" header="Teacherid" [sortable]="true"></p-column>
	 
	<p-column field="ClassModelid" header="ClassModelid" [sortable]="true"></p-column>
	 
	<p-column field="SectionModelid" header="SectionModelid" [sortable]="true"></p-column>
	 
	<p-column field="SubjectModelid" header="SubjectModelid" [sortable]="true"></p-column>
	 
           
            <p-footer><div class="ui-helper-clearfix" style="width:100%"><button type="button" pButton icon="fa-plus" style="float:left" (click)="showDialogToAdd()" label="Add"></button></div></p-footer>
        </p-dataTable>



        <p-dialog header="TCSSRel" [(visible)]="displayDialog" [responsive]="true" showEffect="fade" [modal]="true" [width]="500">
            <form novalidate [formGroup]="mainFrm">

			 	 <div class="form-group">

                    <label for="TCSSRelModelid">TCSSRelModelid</label>
                    <input type="text" class="form-control" id="TCSSRelModelid"  placeholder="Enter TCSSRelModelid" pInputText formControlName="TCSSRelModelid">
                </div>
	 
	 <div class="form-group">

                    <label for="Teacherid">Teacherid</label>
                    <input type="text" class="form-control" id="Teacherid"  placeholder="Enter Teacherid" pInputText formControlName="Teacherid">
                </div>
	 
	 <div class="form-group">

                    <label for="ClassModelid">ClassModelid</label>
                    <input type="text" class="form-control" id="ClassModelid"  placeholder="Enter ClassModelid" pInputText formControlName="ClassModelid">
                </div>
	 
	 <div class="form-group">

                    <label for="SectionModelid">SectionModelid</label>
                    <input type="text" class="form-control" id="SectionModelid"  placeholder="Enter SectionModelid" pInputText formControlName="SectionModelid">
                </div>
	 
	 <div class="form-group">

                    <label for="SubjectModelid">SubjectModelid</label>
                    <input type="text" class="form-control" id="SubjectModelid"  placeholder="Enter SubjectModelid" pInputText formControlName="SubjectModelid">
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


