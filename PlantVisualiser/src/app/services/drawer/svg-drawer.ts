import {MatrixExtract, Svg, TransformData} from '@svgdotjs/svg.js';
import {Drawer} from './drawer';
import {PlantNodeModel, SimulationState} from '../simulation/simulation-state';

type Coordinate = [number, number];
type LineCoordinates = Coordinate[];
type FigureDimensions = { width: number, height: number };

const shoot: LineCoordinates = [
  [0, 0],
  [0, 50],
  [5, 70],
  [7, 80],
  [8, 90],
  [5, 100],
  [8, 150],
  [6, 160],
  [4, 165],
  [5, 175],
  [2, 179],
  [3, 185],
  [4, 200]
];

const branches: LineCoordinates[] = [
  [[5, 70], [-5, 85], [-7, 90], [-10, 95], [-15, 100]],
  [[8, 90], [10, 95], [12, 105], [17, 110], [30, 120], [35, 135]],
  [[8, 150], [-10, 160], [-16, 170], [-30, 180]]
];

const taproot: LineCoordinates = [
  [0, 0],
  [0, -100]
];

const lateralRoots: LineCoordinates[] = [
  [[0, -20], [5, -22], [10, -25], [12, -30]],
  [[0, -5], [-10, -20]],
  [[0, 5], [10, -10]],
  [[0, -15], [-10, -20]]
]

export class SvgDrawer implements Drawer {

  private readonly svg: Svg;

  private static padding: number = 40;

  private shoot: LineCoordinates = [];

  private root: LineCoordinates = [];

  private dimensions: {shoot: FigureDimensions, root: FigureDimensions} = {
    shoot: {width: 0, height: 0},
    root: {width: 0, height: 0}
  };

  constructor(svg: Svg) {
    this.svg = svg;
    this.svg.transform({rotate: 180} as TransformData);
  }

  public draw(state: SimulationState): void {
    this.shoot = shoot;
    this.root = taproot;
    this.drawLines(shoot, true);
    this.drawLines(taproot, false);
    for (const b of branches) {
      this.drawLines(b, true);
    }
    for (const r of lateralRoots) {
      this.drawLines(r, false);
    }
    this.fitToViewBox();
  }

  private drawLines(lines: LineCoordinates, isShoot: boolean): void {
    const gradient = isShoot ? 'shoot-gradient' : 'root-gradient';
    this.getFigureSize(lines, isShoot);
    this.svg.polyline(lines).fill('none').stroke({width: 8, color: `url(#${gradient})`, linecap: 'round'});
  }

  public fitToViewBox(): void {
    const {root, shoot} = this.dimensions;

    const x = -(root.width + shoot.width) / 2;
    const y = -(root.height + shoot.height) / 2;
    const width = root.width + shoot.width;
    const height = (shoot.height / 2) + (root.height / 2) + root.height + shoot.height;

    this.svg
      .viewbox(-200, -200, 450, 450)
      .height(750)
      .width(750);
  }

  public getFigureSize(coordinates: LineCoordinates, isShoot: boolean): void {
    let lowestX = Number.MAX_VALUE;
    let lowestY = Number.MAX_VALUE;
    let highestX = Number.MIN_VALUE;
    let highestY = Number.MIN_VALUE;

    for (const s of coordinates) {
      const [x, y] = s;
      if (x < lowestX) {
        lowestX = x;
      } else if (x > highestX) {
        highestX = x;
      }
      if (y < lowestY) {
        lowestY = y;
      } else if (y > highestY) {
        highestY = y;
      }
    }

    if (isShoot) {
      this.dimensions.shoot.height += SvgDrawer.calculateLength(lowestY, highestY);
      this.dimensions.shoot.width += SvgDrawer.calculateLength(lowestX, highestX);
    } else {
      this.dimensions.root.height += SvgDrawer.calculateLength(lowestY, highestY);
      this.dimensions.root.width += SvgDrawer.calculateLength(lowestX, highestX);
    }
  }

  private static calculateLength(a: number | Coordinate, b?: number) {
    let xC = 0;
    let yC = 0;

    if (Array.isArray(a)) {
      [xC, yC] = a;
    } else if (a !== undefined && b !== undefined) {
      xC = a;
      yC = b;
    } else {
      throw new Error('You must supply either [number, number] or both x and y');
    }
    return Math.sqrt(Math.pow(xC + yC, 2));
  }

}
