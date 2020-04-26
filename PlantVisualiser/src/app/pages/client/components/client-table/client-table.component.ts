import {Component, Input, OnInit} from '@angular/core';
import {ConnectedClientsDataSource} from '../../../../services/clients/connected-clients-data-source';
import {ClientsService} from '../../../../services/clients/clients.service';
import {ConnectedClient} from '../../../../services/clients/connected-client';
import {ConfirmDialogService} from '../../../../services/confirm-dialog/confirm-dialog.service';

@Component({
  selector: 'app-client-table',
  templateUrl: './client-table.component.html',
  styleUrls: ['./client-table.component.scss']
})
export class ClientTableComponent implements OnInit {

  public dataSource: ConnectedClientsDataSource;

  public columns: string[] = ['id', 'host', 'available', 'stop'];

  constructor(private _clients: ClientsService, private _confirm: ConfirmDialogService) { }

  ngOnInit(): void {
    this.dataSource = new ConnectedClientsDataSource(this._clients);
    this.dataSource.connectedClients();
  }

  public handleClick(client: ConnectedClient): void {
    this._confirm.show().subscribe(shouldStop => {
      if (shouldStop) { this._clients.stopSimulation(client.id); }
    });
  }

}
