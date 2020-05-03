import { Component, OnInit } from '@angular/core';
import {SimulationState} from '../../../services/simulation/simulation-state';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-simulation-view',
  templateUrl: './simulation-view.component.html',
  styleUrls: ['./simulation-view.component.scss']
})
export class SimulationViewComponent implements OnInit {

  public simulation: SimulationState;

  constructor(private _route: ActivatedRoute) { }

  ngOnInit(): void {
    this._route.data.subscribe(value => this.simulation = value.simulation);
  }

  public parseDateTime(date: string): string {
    const d = new Date(date);
    return `${d.getDate()}/${d.getMonth()} - ${d.getFullYear()} ${d.getHours()}:${d.getMinutes()}`;
  }

}
