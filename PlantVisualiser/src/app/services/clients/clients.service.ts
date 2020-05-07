import { Injectable } from '@angular/core';
import {ConnectedClient} from './connected-client';
import {Observable, of, Subject, Subscription, timer} from 'rxjs';
import {HttpService} from "../http/http.service";
import {HttpClient} from "@angular/common/http";
import {MatSnackBar} from "@angular/material/snack-bar";

@Injectable({
  providedIn: 'root'
})
export class ClientsService extends HttpService {

  private running: Subscription;

  private $clients: Subject<ConnectedClient[]> = new Subject<ConnectedClient[]>();

  constructor(http: HttpClient, snackbar : MatSnackBar) {
    super(http, snackbar);
  }

  public connectedClients(): Observable<ConnectedClient[]> {
    this.running = timer(0, 5000).subscribe(v => {
      this.getRequest<ConnectedClient[]>('/client').subscribe(clients => this.$clients.next(clients));
    });
    return this.$clients.asObservable();
  }

  public stopSimulation(id: string): Observable<null> {
    console.log('Stopping simulation for id ' + id);
    return of(null);
  }

  public dipose() {
    this.running.unsubscribe();
    this.running = undefined;
  }

}
