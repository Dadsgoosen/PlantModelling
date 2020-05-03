import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot} from '@angular/router';
import {SimulationState} from './simulation-state';
import {EMPTY, Observable, of} from 'rxjs';
import {SimulationService} from './simulation.service';
import {catchError, mergeMap} from 'rxjs/operators';
import {MatSnackBar} from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class SimulationResolverService implements Resolve<SimulationState> {

  constructor(private _simulation: SimulationService, private _router: Router, private _snackbar: MatSnackBar) {
    this.merge = this.merge.bind(this);
    this.handleError = this.handleError.bind(this);
  }

  resolve(route: ActivatedRouteSnapshot,
          state: RouterStateSnapshot): Observable<SimulationState> | Promise<SimulationState> | SimulationState {
    const id: string = route.paramMap.get('id');
    return this._simulation.getSimulation(id).pipe(mergeMap(this.merge));
  }

  private merge(value: SimulationState): Observable<SimulationState> {
    if (value) {
      return of(value);
    }
    return this.handleError();
  }

  private handleError(): Observable<never> {
    this._snackbar.open('Could not find simulation');
    this._router.navigate(['/']);
    return EMPTY;
  }
}
