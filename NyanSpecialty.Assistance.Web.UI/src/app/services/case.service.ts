import { Injectable } from '@angular/core';
import { RepositoryFactory } from '../factory/repositoryfactory.service';
import { Observable } from 'rxjs';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root'
})
export class CaseService {

  constructor(private repositoryFactory: RepositoryFactory) { }

  InsertOrUpdateCaseAsync(insuranceCase: any): Observable<any> {
    return this.repositoryFactory.sendAsync('POST', environment.UrlConstants.InsertOrUpdateCase, insuranceCase);
  }
  CaseStatusProcessAsync(caseProcess: any): Observable<any> {
    return this.repositoryFactory.sendAsync('POST', environment.UrlConstants.CaseStatusProcess, caseProcess);
  }
  GetCaseDetailsAsync(): Observable<any> {
    return this.repositoryFactory.sendAsync('GET', environment.UrlConstants.GetCaseDetails);
  }
  GetCaseDetailsByCaseIdAsync(insurancecaseid:any): Observable<any> {
    return this.repositoryFactory.sendAsync('GET',`${environment.UrlConstants.GetCaseDetailsByCaseId}/${insurancecaseid}`);
  }
}
