import {Directive, ElementRef, Input, OnInit, Renderer2, ViewContainerRef} from '@angular/core';
import {DrawerService} from './drawer.service';
import {SimulationState} from '../simulation/simulation-state';
import {Svg, SVG} from '@svgdotjs/svg.js';

@Directive({
  selector: '[appPlantDrawer]'
})
export class PlantDrawerDirective implements OnInit {

  @Input()
  public simulation: SimulationState;

  private readonly svgElement: SVGElement;

  private readonly svg: Svg;

  constructor(private _drawer: DrawerService,
              private _renderer: Renderer2,
              private _ele: ElementRef) {
    this.svgElement = _ele.nativeElement;
    this.svg = SVG(this.svgElement) as Svg;
  }

  ngOnInit(): void {
    this.checkIfSvgElement();
    this._drawer.draw(this.svg, this.simulation);
  }

  private checkIfSvgElement() {
    if (this.svgElement.localName !== 'svg') {
      throw Error('PlantDrawerDirective must be put on a svg element');
    }
  }

}
