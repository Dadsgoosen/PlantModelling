import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FrontpageComponent } from './frontpage.component';
import {LayoutModule} from '../../shared/layout/layout.module';
import {RouterModule, Routes} from '@angular/router';
import {MatCardModule} from '@angular/material/card';
import { SimulationOverviewComponent } from './components/simulation-overview/simulation-overview.component';
import { SimulationCardComponent } from './components/simulation-card/simulation-card.component';

const routes: Routes = [{
  path: '',
  component: FrontpageComponent
}];

@NgModule({
  declarations: [
    FrontpageComponent,
    SimulationOverviewComponent,
    SimulationCardComponent
  ],
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        LayoutModule,
        MatCardModule
    ]
})
export class FrontpageModule { }
