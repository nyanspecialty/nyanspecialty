import { Injectable } from '@angular/core';
import { RepositoryFactory } from '../factory/repositoryfactory.service';
import { Observable } from 'rxjs';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root'
})
export class PolicyCategoryService {

  constructor(private repositoryFactory: RepositoryFactory) { }

  insertOrUpdatePolicyCategoryAsync(policyCategory: any): Observable<any> {
    return this.repositoryFactory.sendAsync("POST", environment.UrlConstants.SavePolicyCategory, policyCategory);
  }
  fetchPolicyCategoriesAsync(): Observable<any> {
    return this.repositoryFactory.sendAsync("GET", environment.UrlConstants.GetPolicyCategories);
  }
}
