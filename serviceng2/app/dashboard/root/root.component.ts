import { Component, OnInit, ViewEncapsulation } from '@angular/core';
//import { DataTableModule } from 'primeng/components/datatable/datatable';


@Component({
  selector: 'app-root',
  templateUrl: 'app/dashboard/root/root.component.html',
  encapsulation: ViewEncapsulation.None,

  styleUrls: [
      
      'Content/font-awesome.min.css',
      '../node_modules/primeng/resources/themes/omega/theme.css',
      '../node_modules/primeng/resources/primeng.min.css',
      'Content/sbadmin/sb-admin.min.css',
  ]
})
export class RootComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
