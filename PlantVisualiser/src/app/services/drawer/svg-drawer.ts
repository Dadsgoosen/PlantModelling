import {Svg, TransformData} from '@svgdotjs/svg.js';
import {Drawer} from './drawer';
import {PlantModel, PlantNodeModel, SimulationState} from '../simulation/simulation-state';
import {getRootGradient, getShootGradient} from "../../pages/simulation/components/plant-drawer/plant-drawer-gradients";


export class SvgDrawer implements Drawer {

  private readonly svg: Svg;

  constructor(svg: Svg) {
    this.svg = svg;
    this.svg.transform({rotate: 180} as TransformData).viewbox(-175, -175, 350, 350).size('100%', '500');
  }

  public draw(state: SimulationState): void {
    this.clear();
    this.drawPlant(state.plant);
  }

  private drawPlant(plant: PlantModel): void {
    this.drawSystem(plant.rootSystem, false);
    this.drawSystem(plant.shootSystem, true);
  }

  private drawSystem(nodeModels: PlantNodeModel[], isShoot: boolean): void {
    for (const nodeModel of nodeModels) {
      this.drawPlantNodeModel(nodeModel, isShoot);
    }
  }

  private drawPlantNodeModel(nodeModel: PlantNodeModel, isShoot: boolean): void {
    this.drawCoordinates(nodeModel.coordinates, nodeModel.thickness, nodeModel.description, isShoot);
  }

  private drawCoordinates(coordinates: [number, number][], thickness: number, description:string, isShoot: boolean): void {
    const gradient: string = this.getGradient(isShoot).id();
    const c: string = isShoot ? 'shoot' : 'root';
    this.svg.line(coordinates).addClass(c).stroke({width: thickness, color: `url(#${gradient})`, linecap: 'round'}).node.setAttribute('title', description);
  }

  private getGradient(isShoot: boolean) {
    return isShoot ? getShootGradient(this.svg) : getRootGradient(this.svg);
  }

  private clear(): void {
    this.svg.clear();
  }
/*
  private static constrain(x: number): number {
    if (x > 0) {
      return 10;
    } else if (x < 0) {
      return -10;
    }
    return x;
  }*/

}
