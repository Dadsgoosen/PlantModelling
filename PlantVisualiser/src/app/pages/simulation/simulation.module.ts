import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SimulationNewComponent } from './simulation-new/simulation-new.component';
import {RouterModule, Routes} from '@angular/router';
import {LayoutModule} from '../../shared/layout/layout.module';
import { SimulationViewComponent } from './simulation-view/simulation-view.component';
import {SimulationResolverService} from '../../services/simulation/simulation-resolver.service';
import { PlantDrawerComponent } from './components/plant-drawer/plant-drawer.component';
import {DrawerModule} from '../../services/drawer/drawer.module';
import { PlantDrawerPlayerComponent } from './components/plant-drawer-player/plant-drawer-player.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'new',
    pathMatch: 'full'
  },
  {
    path: 'new',
    component: SimulationNewComponent
  },
  {
    path: ':id',
    component: SimulationViewComponent,
    resolve: {simulation: SimulationResolverService}
  }
];


@NgModule({
  declarations: [SimulationNewComponent, SimulationViewComponent, PlantDrawerComponent, PlantDrawerPlayerComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    LayoutModule,
    DrawerModule
  ]
})
export class SimulationModule { }
