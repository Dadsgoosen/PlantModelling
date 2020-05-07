import {Injectable} from '@angular/core';
import {HttpService} from '../http/http.service';
import {HttpClient} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {SimulationReplay, SimulationState} from './simulation-state';
import {catchError} from 'rxjs/operators';
import {MatSnackBar} from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class SimulationService extends HttpService {

  constructor(http: HttpClient, snackbar: MatSnackBar) {
    super(http, snackbar);
  }

  public getSimulations(): Observable<SimulationState[]> {
    return this.getRequest('/simulation');
  }

  public getSimulation(id: string): Observable<SimulationReplay> {
    return this.getRequest('/simulation/' + id);
  }

  public deleteSimulation(id: string): Observable<null> {
    return this.deleteRequest('/simulation/' + id);
  }

  public startSimulation(id: string): Observable<null> {
    return this.postRequest('/simulation/' + id + '/start');
  }
}
