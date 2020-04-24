import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SimulationNewComponent } from './simulation-new/simulation-new.component';
import {RouterModule, Routes} from '@angular/router';
import {LayoutModule} from '../../shared/layout/layout.module';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'new',
    pathMatch: 'full'
  },
  {
    path: 'new',
    component: SimulationNewComponent
  }
];


@NgModule({
  declarations: [SimulationNewComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    LayoutModule
  ]
})
export class SimulationModule { }
