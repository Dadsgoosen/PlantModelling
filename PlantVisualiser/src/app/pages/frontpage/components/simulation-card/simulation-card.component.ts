import {Component, Input, OnInit} from '@angular/core';
import {SimulationState} from '../../../../services/simulation/simulation-state';

@Component({
  selector: 'app-simulation-card',
  templateUrl: './simulation-card.component.html',
  styleUrls: ['./simulation-card.component.scss']
})
export class SimulationCardComponent implements OnInit {

  @Input()
  public simulation: SimulationState;

  constructor() { }

  ngOnInit(): void {
  }

}
