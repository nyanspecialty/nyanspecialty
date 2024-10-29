import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams, HttpRequest, HttpResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, filter, map } from 'rxjs/operators';
import { environment } from '../environment';
import { TokenService } from './token.service';


export interface IRepositoryFactory {
  sendAsync<TResponse>(method: string, uri: string, body?: any): Observable<TResponse>;
}

@Injectable({
  providedIn: 'root'
})
export class RepositoryFactory implements IRepositoryFactory {
  private token: string;
  private baseUrl: string = environment.coreApiUrl; // Set the base URL here

  constructor(private httpClient: HttpClient, private tokenService: TokenService) {
    this.token = this.tokenService.getToken();
  }

  sendAsync<TResponse>(method: string, uri: string, body?: any): Observable<TResponse> {
    const fullUrl = `${this.baseUrl}${uri}`; 
    const request = this.createRequest(method, fullUrl, body);
    
    return this.httpClient.request<TResponse>(request).pipe(
      filter((event): event is HttpResponse<TResponse> => event instanceof HttpResponse), // Filter to ensure we're dealing with HttpResponse
      map((response: HttpResponse<TResponse>) => response.body as TResponse), // Map to AmplifyResponse
      catchError((error: any) => throwError(error))
    );
  }

  private createRequest(method: string, uri: string, body?: any): HttpRequest<any> {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    if (this.token) {
      headers = headers.set('Authorization', `Bearer ${this.token}`);
    }

    return new HttpRequest(method, uri, body, {
      headers,
      params: new HttpParams() // Adjust as needed
    });
  }
}
