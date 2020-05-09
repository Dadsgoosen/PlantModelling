import { Injectable } from '@angular/core';
import {Observable, Subject} from "rxjs";
import {SimulationState} from "./simulation-state";

@Injectable({
  providedIn: 'root'
})
export class SimulationStateService {

  private readonly $stateChange: Subject<SimulationState> = new Subject<SimulationState>();

  constructor() { }

  public get stateChange(): Observable<SimulationState> {
    return this.$stateChange.asObservable();
  }

  public changeState(state: SimulationState): void {
    this.$stateChange.next(state);
  }

}
