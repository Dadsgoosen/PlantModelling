import {Component, OnInit} from '@angular/core';
import {SimulationService} from "../../../services/simulation/simulation.service";
import {HttpClient} from "@angular/common/http";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-simulation-new',
  templateUrl: './simulation-new.component.html',
  styleUrls: ['./simulation-new.component.scss']
})
export class SimulationNewComponent implements OnInit {

  constructor(private _simulationService: SimulationService, private _snackBar: MatSnackBar) {
  }

  ngOnInit(): void {
  }

  public startSimulation() {
    console.log("Starting simulation")
    this._simulationService.startSimulation().subscribe(
      value => {
        console.log("Simulation started", value);
        this.snackbar("Simulation started")
      },
      err => {
        console.log("Error starting simulation", err)
        this.snackbar("Error starting simulation");
      });
  }

  private snackbar(message: string): void {
    this._snackBar.open(message, "Ok", {duration: 3000})
  }

}
