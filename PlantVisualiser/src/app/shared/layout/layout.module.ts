import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MaterialComponentsModule} from '../material-components.module';
import { SideNavMenuComponent } from './side-nav-menu/side-nav-menu.component';
import {RouterModule} from '@angular/router';
import { PageContainerComponent } from './page-container/page-container.component';
import { PageTitleComponent } from './page-title/page-title.component';
import { PageTopComponent } from './page-top/page-top.component';
import { PageTitleMenuComponent } from './page-title-menu/page-title-menu.component';
import { PageBodyComponent } from './page-body/page-body.component';



@NgModule({
  declarations: [
    SideNavMenuComponent,
    PageContainerComponent,
    PageTitleComponent,
    PageTopComponent,
    PageTitleMenuComponent,
    PageBodyComponent
  ],
  exports: [
    MaterialComponentsModule,
    SideNavMenuComponent,
    PageContainerComponent,
    PageTopComponent,
    PageTitleMenuComponent,
    PageTitleComponent,
    PageBodyComponent
  ],
  imports: [
    CommonModule,
    MaterialComponentsModule,
    RouterModule
  ]
})
export class LayoutModule { }
