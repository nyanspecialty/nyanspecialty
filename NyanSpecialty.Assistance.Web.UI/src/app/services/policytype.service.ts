import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RepositoryFactory } from '../factory/repositoryfactory.service';
import { environment } from '../environment';

@Injectable({
    providedIn: 'root'
})
export class PolicyTypeService {

    constructor(private repositoryFactory: RepositoryFactory) { }

    insertOrUpdatePolicyTypeAsync(policyType: any): Observable<any> {
        return this.repositoryFactory.sendAsync('POST', environment.UrlConstants.SavePolicyType, policyType);
    }
    
    getAllPolicyTypeAsync(): Observable<any> {
        return this.repositoryFactory.sendAsync('GET', environment.UrlConstants.GetPolicyTypes);
    }

    getPolicyTypeByIdAsync(policyTypeId: any): Observable<any> {
        return this.repositoryFactory.sendAsync('GET',`${environment.UrlConstants.GetPolicyTypeById}/${policyTypeId}`);
    }

}