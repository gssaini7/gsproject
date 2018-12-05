
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { PageModel, ImageGalleryModel, AlbumModel } from '../../Models/credentials.interface';
import { AuthcrudService } from '../../Services/authcrud.service';
import { Global } from '../../Shared/global';

@Component({
  selector: 'popup-dialog',
  templateUrl: 'app/dashboard/PopupDialog/PopupDialog.component.html',
  //styleUrls: ['./login-form.component.scss']
})

export class PopupDialogComponent implements OnInit {

    msg: string;
    pagesobjlist: PageModel[];
    selectedpage: PageModel;

    albumobjlist: AlbumModel[];
    selectedalbum: AlbumModel;

    imageobjlist: ImageGalleryModel[];
    selectedimage: ImageGalleryModel;

    indLoading: boolean = false;
    displayDialog: boolean = false;

    @Input() toShow: string;

    constructor(private _crudService: AuthcrudService) { }

    ngOnInit(): void {
        if (this.toShow === undefined)
            this.toShow = 'link';
    }

    @Output()
    notify: EventEmitter<string> = new EventEmitter<string>();

    sendTitle(str: string): void {
        this.notify.emit(str);
    }

    showdialog(): void {
        this.displayDialog = true;
        this.LoadPages();
        this.LoadAlbums();
    }

    LoadPages(): void {
        this.indLoading = true;
        this.pagesobjlist = null;
        this._crudService.get(Global.BASE_USER_ENDPOINT + "page/GetAll")
            .subscribe(records => {
                this.pagesobjlist = records.filter((s: any) => s.isPublished);
                this.indLoading = false;
            },
            error => this.msg = <any>error);
    }

    LoadAlbums(): void {
        this.indLoading = true;
        this.albumobjlist = null;
        this._crudService.get(Global.BASE_USER_ENDPOINT + "Album/GetAll")
            .subscribe(records => {
                this.albumobjlist = records.filter((s: any) => s.isPublished);
                this.indLoading = false;
                if (this.albumobjlist.length != 0) {
                    var firstalbum = this.albumobjlist[0];
                    this.selectedalbum = firstalbum;
                    this.LoadImages(firstalbum.AlbumModelid);
                }
            },
            error => this.msg = <any>error);
    }

    LoadImages(id: string): void {
        this.msg = '';
        if (id == undefined)
            return;
        this.indLoading = true;
        this.imageobjlist = null;
        this._crudService.get(Global.BASE_USER_ENDPOINT + "ImageGallery/GetAll?id=" + id)
            .subscribe(records => {
                this.imageobjlist = records;
                this.indLoading = false;
            },
            error => this.msg = <any>error);
    }
    

    onpageselection(): void {
        this.sendTitle(this.selectedpage.pageurl);
        this.displayDialog = false;
    }

    onalbumselection(): void {
        //this.sendTitle(this.selectedpage.pageurl);
        //this.displayDialog = false;
        this.LoadImages(this.selectedalbum.AlbumModelid);
    }

    onimageselection(data: ImageGalleryModel): void {
        this.sendTitle(data.ImageName);
        this.displayDialog = false;
    }
}


