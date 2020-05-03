import {MatrixExtract, Svg, TransformData} from '@svgdotjs/svg.js';
import {Drawer} from './drawer';
import {PlantNodeModel, SimulationState} from '../simulation/simulation-state';

export class SvgDrawer implements Drawer {

  private readonly svg: Svg;

  private readonly boundaries: { top: number, bottom: number, left: number, right: number }
    = {top: 0, bottom: 0, left: 0, right: 0};

  constructor(svg: Svg) {
    this.svg = svg;
    this.svg.transform({rotate: 180} as TransformData);
  }

  draw(state: SimulationState): void {
    this.drawPlant(state);
    const h = Math.abs(this.boundaries.top + this.boundaries.bottom);
    const w = Math.abs(this.boundaries.left + this.boundaries.right)
    this.svg.viewbox(-(w / 2), -(h / 2), w, h);
  }

  private drawPlant(state: SimulationState): void {
    this.drawSystem(state.plant.shootSystem);
    // this.drawSystem(state.plant.rootSystem);
  }

  private drawSystem(node: PlantNodeModel[]): void {
    const coords = this.getCoordinatesFromNodes(node);
    this.svg.polyline(coords).addClass('shoot-system-stem');

    for (const n of node) {
      if (n.connections && n.connections.length > 0) {
        for (const connection of n.connections) {
          this.drawSystem(connection);
        }
      }
    }
  }

  private getCoordinatesFromNodes(node: PlantNodeModel[]): [number, number][] {
    const coordinates: [number, number][] = [];
    for (const n of node) {
      if (n.x > this.boundaries.top) {
        this.boundaries.top = n.x;
      } else if (n.x < this.boundaries.bottom) {
        this.boundaries.bottom = n.x;
      }
      if (n.y > this.boundaries.right) {
        this.boundaries.right = n.y;
      } else if (n.y < this.boundaries.left) {
        this.boundaries.left = n.y;
      }
      coordinates.push([n.x, n.y]);
    }
    return coordinates;
  }

  private clearDom() {
    this.svg.clear();
  }
}
