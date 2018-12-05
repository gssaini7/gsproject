
<div class=''>
   
    <div class=''>
        <div class="ui-widget-header" style="padding:4px 10px;border-bottom: 0 none">
            <i class="fa fa-search" style="margin:4px 4px 0 0"></i>
            <input #gb type="text" pInputText size="50" placeholder="Global Filter">
        </div>
        <p-dataTable #dt [value]="mainobjlist" [rows]="10" [paginator]="true" [pageLinks]="3" [rowsPerPageOptions]="[5,10,20]" [globalFilter]="gb"
                     selectionMode="single" (onRowSelect)="onRowSelect($event)">
 	<p-column field="ImageGalleryModelid" header="ImageGalleryModelid" [sortable]="true"></p-column>
	 
	<p-column field="ImageName" header="ImageName" [sortable]="true"></p-column>
	 
           
            <p-footer><div class="ui-helper-clearfix" style="width:100%"><button type="button" pButton icon="fa-plus" style="float:left" (click)="showDialogToAdd()" label="Add"></button></div></p-footer>
        </p-dataTable>



        <p-dialog header="ImageGallery" [(visible)]="displayDialog" [responsive]="true" showEffect="fade" [modal]="true" [width]="500">
            <form novalidate [formGroup]="mainFrm">

			 	 <div class="form-group">

                    <label for="ImageGalleryModelid">ImageGalleryModelid</label>
                    <input type="text" class="form-control" id="ImageGalleryModelid"  placeholder="Enter ImageGalleryModelid" pInputText formControlName="ImageGalleryModelid">
                </div>
	 
	 <div class="form-group">

                    <label for="ImageName">ImageName</label>
                    <input type="text" class="form-control" id="ImageName"  placeholder="Enter ImageName" pInputText formControlName="ImageName">
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


