import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClientOverviewComponent } from './client-overview/client-overview.component';
import {RouterModule, Routes} from '@angular/router';
import {LayoutModule} from '../../shared/layout/layout.module';
import {MatTableModule} from '@angular/material/table';
import { ClientTableComponent } from './components/client-table/client-table.component';
import {ConfirmDialogModule} from '../../services/confirm-dialog/confirm-dialog.module';

const routes: Routes = [
  {
    path: '',
    component: ClientOverviewComponent,
    pathMatch: 'full'
  }
];

@NgModule({
  declarations: [ClientOverviewComponent, ClientTableComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    LayoutModule,
    MatTableModule,
    ConfirmDialogModule
  ]
})
export class ClientModule { }
