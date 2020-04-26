import { Injectable } from '@angular/core';
import {MatDialog, MatDialogRef} from '@angular/material/dialog';
import {Observable} from 'rxjs';
import {ConfirmDialogComponent} from './confirm-dialog.component';

@Injectable({
  providedIn: 'root'
})
export class ConfirmDialogService {

  private  _dialogRef: MatDialogRef<ConfirmDialogComponent>;

  constructor(private _dialog: MatDialog) { }

  public show(): Observable<boolean>  {
    this._dialogRef = this._dialog.open(ConfirmDialogComponent);
    return this._dialogRef.afterClosed();
  }

  public close(): void {
    if (!this._dialogRef) { return; }
    this._dialogRef.close(false);
  }
}
