import {AfterViewInit, Component, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {SimulationReplay, SimulationState} from "../../../../services/simulation/simulation-state";
import {Subscription, timer} from "rxjs";
import {MatSlider, MatSliderChange} from "@angular/material/slider";
import {PlantDrawerComponent} from "../plant-drawer/plant-drawer.component";
import {SimulationStateService} from "../../../../services/simulation/simulation-state.service";

type PlayerMeta = {count: number, first: number, last: number, interval: number, current: number};

@Component({
  selector: 'app-plant-drawer-player',
  templateUrl: './plant-drawer-player.component.html',
  styleUrls: ['./plant-drawer-player.component.scss']
})
export class PlantDrawerPlayerComponent implements OnInit, AfterViewInit {

  private totalPlayTime = 10000;

  private intervalTime: number;

  @Input()
  public simulation: SimulationReplay;

  @ViewChild(MatSlider)
  public slider: MatSlider;

  @ViewChild(PlantDrawerComponent)
  public plantDrawer: PlantDrawerComponent;

  public meta: PlayerMeta = {
    count: 0,
    first: 0,
    last: 0,
    interval: 1,
    current: 0
  };

  public current: SimulationState;

  public playing: boolean = false;

  private obs: Subscription;

  constructor(private _simulationState: SimulationStateService) {
    this.tick = this.tick.bind(this);
    this.onSliderChange = this.onSliderChange.bind(this);
  }

  ngOnInit(): void {
    this.computeMetaData();
    this.current = this.simulation[Object.keys(this.simulation)[0]];
  }

  public ngAfterViewInit(): void {
    this.slider.change.subscribe(this.onSliderChange);
  }

  public toggleStartStop() {
    this.playing = !this.playing;
    this.playing ? this.start() : this.stop();
  }

  private computeMetaData(): void {
    for (const time in this.simulation) {
      if(!this.simulation.hasOwnProperty(time)) {
        continue;
      }
      const state = this.simulation[time];

      if(this.meta.count === 0) {
        this.meta.current = state.simulationTime;
      }

      if(this.meta.count === 1) {
        this.meta.interval = state.simulationTime;
      }

      this.meta.count++;

      if(this.meta.first > state.simulationTime) {
        this.meta.first = state.simulationTime;
      }
      if (this.meta.last < state.simulationTime) {
        this.meta.last = state.simulationTime;
      }
    }
    this.intervalTime = this.totalPlayTime / this.meta.count;
  }

  public stop(): void {
    this.obs.unsubscribe();
    this.playing = false;
  }

  public start(): void {
    if(this.meta.current === this.meta.last) {
      this.reset();
    }

    this.playing = true;

    this.obs = timer(this.intervalTime, this.intervalTime).subscribe(this.tick);
  }

  private tick(): void {
    this.meta.current += this.meta.interval;

    if(this.meta.current >= this.meta.last) {
      this.stop();
    }

    this.changeToCurrent();
  }

  private reset(): void {
    this.current = this.simulation[this.meta.first];
    this.meta.current = this.meta.first;
    this.slider.value = this.meta.current;
    this._simulationState.changeState(this.current);
  }

  private onSliderChange(valueChange: MatSliderChange): void {
    this.meta.current = valueChange.value;
    this.changeToCurrent()
  }

  private changeToCurrent(): void {
    this.current = this.simulation[this.meta.current];
    if(this.current === undefined) return;
    this.slider.value = this.current.simulationTime;
    this._simulationState.changeState(this.current);
  }

}
