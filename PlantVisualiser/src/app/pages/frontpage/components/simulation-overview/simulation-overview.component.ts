import { Component, OnInit } from '@angular/core';
import {SimulationService} from '../../../../services/simulation/simulation.service';
import {SimulationState} from '../../../../services/simulation/simulation-state';

@Component({
  selector: 'app-simulation-overview',
  templateUrl: './simulation-overview.component.html',
  styleUrls: ['./simulation-overview.component.scss']
})
export class SimulationOverviewComponent implements OnInit {

  public simulations: SimulationState[] = [];

  constructor(private _simulation: SimulationService) { }

  ngOnInit(): void {
    this._simulation.getSimulations().subscribe(simulations => this.simulations = simulations);
  }

}
