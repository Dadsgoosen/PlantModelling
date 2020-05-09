import {Injectable, Renderer2, ViewContainerRef} from '@angular/core';
import {Svg} from '@svgdotjs/svg.js';
import {PlantNodeModel, SimulationState} from '../simulation/simulation-state';
import {Drawer} from './drawer';
import {SvgDrawer} from './svg-drawer';
import {Observable, Subject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class DrawerService {

  constructor() {
  }

  public draw(svg: Svg, state: SimulationState): void {
    new SvgDrawer(svg).draw(state);
  }

}
