import { Injectable } from '@angular/core';
import { RepositoryFactory } from '../factory/repositoryfactory.service';
import { Observable } from 'rxjs';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root'
})
export class InsurancePolicyService {

  constructor(private repositoryFactory: RepositoryFactory) { }

  insertOrUpdateInsurancePolicyAsync(role: any): Observable<any> {
    return this.repositoryFactory.sendAsync('POST', environment.UrlConstants.SaveInsurancePolicy, role);
  }
  fetchInsurancePolicysAsync(): Observable<any> {
    return this.repositoryFactory.sendAsync('GET', environment.UrlConstants.GetInsurancePolicies);
  }
}
