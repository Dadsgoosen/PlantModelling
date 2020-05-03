import {Component, Input, OnInit} from '@angular/core';
import {DrawerService} from '../../../../services/drawer/drawer.service';
import {SimulationState} from '../../../../services/simulation/simulation-state';
import {simulationStateTest} from '../../../../services/drawer/test-drawer-data';

@Component({
  selector: 'app-plant-drawer',
  templateUrl: './plant-drawer.component.html',
  styleUrls: ['./plant-drawer.component.scss']
})
export class PlantDrawerComponent implements OnInit {

  @Input()
  public simulation: SimulationState;

  constructor(private _drawer: DrawerService) { }

  ngOnInit(): void {
    this.simulation = simulationStateTest;
  }

}
