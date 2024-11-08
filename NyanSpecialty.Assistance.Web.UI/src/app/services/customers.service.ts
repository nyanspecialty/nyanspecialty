import { Injectable } from '@angular/core';
import { RepositoryFactory } from '../factory/repositoryfactory.service';
import { Observable } from 'rxjs';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private repositoryFactory: RepositoryFactory) { }

  insertOrUpdateCustomerAsync(role: any): Observable<any> {
    return this.repositoryFactory.sendAsync('POST', environment.UrlConstants.SaveCustomers, role);
  }
  fetchCustomersAsync(): Observable<any> {
    return this.repositoryFactory.sendAsync('GET', environment.UrlConstants.GetCustomers);
  }
}
