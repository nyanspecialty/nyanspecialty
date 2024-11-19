import { Injectable } from '@angular/core';
import { RepositoryFactory } from '../factory/repositoryfactory.service';
import { Observable } from 'rxjs';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root'
})
export class InsurancePolicyService {

  constructor(private repositoryFactory: RepositoryFactory) { }

  insertOrUpdateInsurancePolicyAsync(policy: any): Observable<any> {
    return this.repositoryFactory.sendAsync('POST', environment.UrlConstants.SaveInsurancePolicy, policy);
  }
  fetchInsurancePolicysAsync(): Observable<any> {
    return this.repositoryFactory.sendAsync('GET', environment.UrlConstants.GetInsurancePolicies);
  }
  fetchInsurancePolicyDetailssAsync(insurancepolicyid:any): Observable<any> {
    return this.repositoryFactory.sendAsync('GET',`${environment.UrlConstants.GetInsurancePolicyById}/${insurancepolicyid}`);
  }
}
