import {HttpClient, HttpErrorResponse, HttpHeaders, HttpParams} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {environment} from '../../../environments/environment';
import {catchError, retry} from 'rxjs/operators';
import {MatSnackBar} from '@angular/material/snack-bar';

export abstract class HttpService {

  protected constructor(private _http: HttpClient, private _snackBar: MatSnackBar) {
    this.handleError = this.handleError.bind(this);
  }

  private static createUrl(url: string): string {
    return environment.apiServer + url;
  }

  protected getRequest<T extends object>(url: string, params?: HttpParams, headers?: HttpHeaders): Observable<T> {
    return this._http.get(HttpService.createUrl(url), {
      params,
      headers: this.createHeader(headers)
    }).pipe(catchError(this.handleError)) as Observable<T>;
  }

  protected postRequest<T extends object>(url: string, body?: any, params?: HttpParams, headers?: HttpHeaders): Observable<T> {
    return this._http.post(HttpService.createUrl(url), body, {
      params,
      headers: this.createHeader(headers)
    })
      .pipe(catchError(this.handleError)) as Observable<T>;
  }

  protected deleteRequest<T extends object>(url: string, params?: HttpParams, headers?: HttpHeaders): Observable<T> {

    return this._http.delete(HttpService.createUrl(url), {
      params,
      headers: this.createHeader(headers)
    })
      .pipe(catchError(this.handleError)) as Observable<T>;
  }

  private handleError(error: HttpErrorResponse) {
    console.log(error);
    this._snackBar.open('Error sending request to server', 'Ok', {duration: 3000});
    return throwError('Something bad happened; please try again later.');
  }

  private createHeader(headers?: HttpHeaders): HttpHeaders {
    let h = headers ? headers : new HttpHeaders();

    if (!h.has('Content-Type')) {
      h = h.append('Content-Type', 'application/json');
    }

    return headers;
  }

}
