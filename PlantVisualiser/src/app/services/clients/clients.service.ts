import { Injectable } from '@angular/core';
import {ConnectedClient} from './connected-client';
import {Observable, of} from 'rxjs';

const connectedClients: ConnectedClient[] = [
  {
    id: 'fe2ac4fe-450f-4224-b07c-62213254ebba',
    available: true,
    host: 'https://localhost:5001'
  },
  {
    id: 'fe2ac4fe-450f-4225-b07c-62213254ebba',
    available: true,
    host: 'https://localhost:5002'
  },
  {
    id: 'fe2ac4de-450f-4225-b07c-62213256ebba',
    available: false,
    host: 'https://localhost:5003'
  }
];


@Injectable({
  providedIn: 'root'
})
export class ClientsService {

  constructor() { }

  public connectedClients(): Observable<ConnectedClient[]> {
    return of(connectedClients);
  }

  public stopSimulation(id: string): Observable<null> {
    console.log('Stopping simulation for id ' + id);
    return of(null);
  }

}
