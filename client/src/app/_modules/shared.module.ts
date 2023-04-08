import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import {TabsModule} from 'ngx-bootstrap/tabs'
import { ToastrModule } from 'ngx-toastr';
import {NgxGalleryModule} from '@kolkov/ngx-gallery'
import { NgxSpinnerModule } from 'ngx-spinner';
import { FileUploadModule } from 'ng2-file-upload';
import {BsDatepickerModule} from 'ngx-bootstrap/datepicker';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    NgxGalleryModule,    
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    ToastrModule.forRoot({
      positionClass:'toast-bottom-center'
    }),
    NgxSpinnerModule.forRoot({
      type: 'ball-atom'
    }),
    FileUploadModule,
    BsDatepickerModule.forRoot()
  ],
  exports: [
    BsDropdownModule,
    ToastrModule,
    TabsModule,
    NgxGalleryModule,
    NgxSpinnerModule,
    FileUploadModule,
    BsDatepickerModule
  ]
})
export class SharedModule { }
