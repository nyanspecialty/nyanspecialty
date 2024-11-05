import { Injectable } from '@angular/core';
import { RepositoryFactory } from '../factory/repositoryfactory.service';
import { Observable } from 'rxjs';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root'
})
export class ServiceTypeService {

  constructor(private repositoryFactory: RepositoryFactory) { }

  insertOrUpdateServiceTypeAsync(serviceType: any): Observable<any> {
    return this.repositoryFactory.sendAsync('POST', environment.UrlConstants.SaveServiceType, serviceType);
  }
  fetchServiceTypesAsync(): Observable<any> {
    return this.repositoryFactory.sendAsync('GET', environment.UrlConstants.GetServiceTypes);
  }
}
