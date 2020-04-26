import {CollectionViewer, DataSource} from '@angular/cdk/collections';
import {ConnectedClient} from './connected-client';
import {BehaviorSubject, Observable, of} from 'rxjs';
import {ClientsService} from './clients.service';
import {catchError} from 'rxjs/operators';

export class ConnectedClientsDataSource implements DataSource<ConnectedClient> {

  private clientsSubject = new BehaviorSubject<ConnectedClient[]>([]);

  constructor(private _clients: ClientsService) {
  }

  public connect(collectionViewer: CollectionViewer): Observable<ConnectedClient[] | ReadonlyArray<ConnectedClient>> {
    return this.clientsSubject.asObservable();
  }

  public disconnect(collectionViewer: CollectionViewer): void {
    this.clientsSubject.complete();
  }

  public connectedClients() {
    this._clients
      .connectedClients()
      .pipe(
        catchError(err => of([]))
      )
      .subscribe(value => this.clientsSubject.next(value));
  }

}
