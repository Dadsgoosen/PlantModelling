import {Component, OnInit} from '@angular/core';
import {SimulationReplay, SimulationState} from '../../../services/simulation/simulation-state';
import {ActivatedRoute} from '@angular/router';
import {map} from "rxjs/operators";
import {SimulationStateService} from "../../../services/simulation/simulation-state.service";

@Component({
  selector: 'app-simulation-view',
  templateUrl: './simulation-view.component.html',
  styleUrls: ['./simulation-view.component.scss']
})
export class SimulationViewComponent implements OnInit {

  public replay: SimulationReplay;

  public id: string;

  public current: SimulationState;

  constructor(private _route: ActivatedRoute, private _simulationState: SimulationStateService) {
    this.setData = this.setData.bind(this);
    this.onSimulationChange = this.onSimulationChange.bind(this);
  }

  ngOnInit(): void {
    this._route.data.pipe(map(v => v.simulation)).subscribe(this.setData);
    this._simulationState.stateChange.subscribe(this.onSimulationChange);
  }

  private setData(replay: SimulationReplay): void {
    this.replay = replay;

    for (const time in replay) {
      if (!replay.hasOwnProperty(time)) {
        continue;
      }

      this.current = replay[time];
      break;
    }
  }

  public parseDateTime(date: string): string {
    const d = new Date(date);
    return `${d.getDate()}/${d.getMonth()} - ${d.getFullYear()} ${d.getHours()}:${d.getMinutes()}`;
  }

  public onSimulationChange(state: SimulationState) {
    this.current = state;
  }
}
