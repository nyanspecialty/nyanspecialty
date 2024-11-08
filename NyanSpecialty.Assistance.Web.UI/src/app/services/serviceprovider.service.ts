import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RepositoryFactory } from '../factory/repositoryfactory.service';
import { environment } from '../environment';
import { setUncaughtExceptionCaptureCallback } from 'process';

@Injectable({
  providedIn: 'root'
})
export class ServiceProviderService {

  constructor(private repositoryFactory: RepositoryFactory) { }

  insertOrUpdateerviceProviderAsync(servicProvider: any): Observable<any> {
    return this.repositoryFactory.sendAsync('POST', environment.UrlConstants.SaveServiceProvider, servicProvider);
  }
  fetcherviceProvidersAsync(): Observable<any> {
    return this.repositoryFactory.sendAsync('GET', environment.UrlConstants.GetAllServiceProviders);
  }
  fetchServiceProviderById(providerId: any): Observable<any> {
    return this.repositoryFactory.sendAsync("GET", `${environment.UrlConstants.GetAllServiceProviders}/${providerId}`);
  }
}
