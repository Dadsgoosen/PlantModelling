import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {SimulationState} from '../../../../services/simulation/simulation-state';
import {PlantDrawerDirective} from "../../../../services/drawer/plant-drawer.directive";
import {SimulationStateService} from "../../../../services/simulation/simulation-state.service";

@Component({
  selector: 'app-plant-drawer',
  templateUrl: './plant-drawer.component.html',
  styleUrls: ['./plant-drawer.component.scss']
})
export class PlantDrawerComponent implements OnInit {

  @Input()
  public simulation: SimulationState;

  @ViewChild(PlantDrawerDirective)
  plantDrawer: PlantDrawerDirective;

  constructor(private _simulationState: SimulationStateService) {
    this.draw = this.draw.bind(this);
  }

  ngOnInit(): void {
    this._simulationState.stateChange.subscribe(this.draw);
  }

  public draw(simulation: SimulationState) {
    this.plantDrawer.draw(simulation);
  }

}
