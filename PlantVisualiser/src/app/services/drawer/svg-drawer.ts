import {Gradient, Svg, TransformData} from '@svgdotjs/svg.js';
import {Drawer} from './drawer';
import {PlantModel, PlantNodeModel, SimulationState} from '../simulation/simulation-state';
import {getRootGradient, getShootGradient} from "../../pages/simulation/components/plant-drawer/plant-drawer-gradients";


export class SvgDrawer implements Drawer {

  private readonly svg: Svg;

  constructor(svg: Svg) {
    this.svg = svg;
    this.svg.transform({rotate: 180} as TransformData).viewbox(-250, -250, 500, 500).size('100%', '500');
  }

  public draw(state: SimulationState): void {
    this.clear();
    this.drawPlant(state.plant);
  }

  private drawPlant(plant: PlantModel): void {
    this.drawSystem(plant.shootSystem, true);
    this.drawSystem(plant.rootSystem, false);
  }

  private drawSystem(nodeModels: PlantNodeModel[], isShoot: boolean): void {
    for (const nodeModel of nodeModels) {
      this.drawPlantNodeModel(nodeModel, isShoot);
    }
  }

  private drawPlantNodeModel(nodeModel: PlantNodeModel, isShoot: boolean): void {
    this.drawCoordinates(nodeModel.coordinates, nodeModel.thickness, isShoot);
  }

  private drawCoordinates(coordinates: [number, number][], thickness: number, isShoot: boolean): void {
    const gradient: string = this.getGradient(isShoot).id();
    this.svg.line(coordinates).stroke({width: 8, color: `url(#${gradient})`, linecap: 'round'});
  }


  private getGradient(isShoot: boolean) {
    return isShoot ? getShootGradient(this.svg) : getRootGradient(this.svg);
  }

  private clear(): void {
    this.svg.clear();
  }

}
