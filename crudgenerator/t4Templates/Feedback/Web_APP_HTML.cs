
<div class=''>
   
    <div class=''>
        <div class="ui-widget-header" style="padding:4px 10px;border-bottom: 0 none">
            <i class="fa fa-search" style="margin:4px 4px 0 0"></i>
            <input #gb type="text" pInputText size="50" placeholder="Global Filter">
        </div>
        <p-dataTable #dt [value]="mainobjlist" [rows]="10" [paginator]="true" [pageLinks]="3" [rowsPerPageOptions]="[5,10,20]" [globalFilter]="gb"
                     selectionMode="single" (onRowSelect)="onRowSelect($event)">
 	<p-column field="FeedbackModelid" header="FeedbackModelid" [sortable]="true"></p-column>
	 
	<p-column field="FeedbackType" header="FeedbackType" [sortable]="true"></p-column>
	 
	<p-column field="StudentModelID" header="StudentModelID" [sortable]="true"></p-column>
	 
	<p-column field="FeedbackDate" header="FeedbackDate" [sortable]="true"></p-column>
	 
	<p-column field="FeedbackMessage" header="FeedbackMessage" [sortable]="true"></p-column>
	 
           
            <p-footer><div class="ui-helper-clearfix" style="width:100%"><button type="button" pButton icon="fa-plus" style="float:left" (click)="showDialogToAdd()" label="Add"></button></div></p-footer>
        </p-dataTable>



        <p-dialog header="Feedback" [(visible)]="displayDialog" [responsive]="true" showEffect="fade" [modal]="true" [width]="500">
            <form novalidate [formGroup]="mainFrm">

			 	 <div class="form-group">

                    <label for="FeedbackModelid">FeedbackModelid</label>
                    <input type="text" class="form-control" id="FeedbackModelid"  placeholder="Enter FeedbackModelid" pInputText formControlName="FeedbackModelid">
                </div>
	 
	 <div class="form-group">

                    <label for="FeedbackType">FeedbackType</label>
                    <input type="text" class="form-control" id="FeedbackType"  placeholder="Enter FeedbackType" pInputText formControlName="FeedbackType">
                </div>
	 
	 <div class="form-group">

                    <label for="StudentModelID">StudentModelID</label>
                    <input type="text" class="form-control" id="StudentModelID"  placeholder="Enter StudentModelID" pInputText formControlName="StudentModelID">
                </div>
	 
	 <div class="form-group">

                    <label for="FeedbackDate">FeedbackDate</label>
                    <input type="text" class="form-control" id="FeedbackDate"  placeholder="Enter FeedbackDate" pInputText formControlName="FeedbackDate">
                </div>
	 
	 <div class="form-group">

                    <label for="FeedbackMessage">FeedbackMessage</label>
                    <input type="text" class="form-control" id="FeedbackMessage"  placeholder="Enter FeedbackMessage" pInputText formControlName="FeedbackMessage">
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


