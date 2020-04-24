import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FrontpageComponent } from './frontpage.component';
import {LayoutModule} from '../../shared/layout/layout.module';
import {RouterModule, Routes} from '@angular/router';
import {MatCardModule} from '@angular/material/card';

const routes: Routes = [{
  path: '',
  component: FrontpageComponent
}];

@NgModule({
  declarations: [
    FrontpageComponent
  ],
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        LayoutModule,
        MatCardModule
    ]
})
export class FrontpageModule { }
