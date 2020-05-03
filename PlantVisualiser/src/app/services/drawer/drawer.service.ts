import {Injectable, Renderer2, ViewContainerRef} from '@angular/core';
import {Svg} from '@svgdotjs/svg.js';
import {PlantNodeModel, SimulationState} from '../simulation/simulation-state';
import {Drawer} from './drawer';
import {SvgDrawer} from './svg-drawer';

@Injectable({
  providedIn: 'root'
})
export class DrawerService {

  private drawer: Drawer;

  constructor() {
  }

  public draw(svg: Svg, state: SimulationState): void {
    if (!this.drawer) { this.drawer = new SvgDrawer(svg); }

    this.drawer.draw(state);
  }
}
