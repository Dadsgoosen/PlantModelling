import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {FrontpageComponent} from './pages/frontpage/frontpage.component';


const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('src/app/pages/frontpage/frontpage.module').then(m => m.FrontpageModule)
  },
  {
    path: 'simulation',
    loadChildren: () => import('src/app/pages/simulation/simulation.module').then(m => m.SimulationModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
