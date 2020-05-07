import {Component, OnInit} from '@angular/core';
import {SimulationReplay, SimulationState} from '../../../services/simulation/simulation-state';
import {ActivatedRoute} from '@angular/router';
import {map} from "rxjs/operators";

@Component({
  selector: 'app-simulation-view',
  templateUrl: './simulation-view.component.html',
  styleUrls: ['./simulation-view.component.scss']
})
export class SimulationViewComponent implements OnInit {

  public replay: SimulationReplay;

  public id: string;

  current: SimulationState;

  constructor(private _route: ActivatedRoute) {
    this.setData = this.setData.bind(this);
  }

  ngOnInit(): void {
    this._route.data.pipe(map(v => v.simulation)).subscribe(this.setData);
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
