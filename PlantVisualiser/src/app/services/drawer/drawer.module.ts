import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {PlantDrawerDirective} from './plant-drawer.directive';



@NgModule({
  declarations: [
    PlantDrawerDirective
  ],
  exports: [
    PlantDrawerDirective
  ],
  imports: [
    CommonModule
  ]
})
export class DrawerModule { }
