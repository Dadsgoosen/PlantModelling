import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('src/app/pages/frontpage/frontpage.module').then(m => m.FrontpageModule)
  },
  {
    path: 'simulations',
    loadChildren: () => import('src/app/pages/simulation/simulation.module').then(m => m.SimulationModule)
  },
  {
    path: 'clients',
    loadChildren: () => import('src/app/pages/client/client.module').then(m => m.ClientModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
