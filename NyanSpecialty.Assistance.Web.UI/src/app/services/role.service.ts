import { Injectable } from '@angular/core';
import { RepositoryFactory } from '../factory/repositoryfactory.service';
import { environment } from '../environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  constructor(private repositoryFactory: RepositoryFactory) { }

  insertOrUpdateRoleAsync(role: any): Observable<any> {
    return this.repositoryFactory.sendAsync('POST', environment.UrlConstants.SaveRole, role);
  }
  fetchRolesAsync(): Observable<any> {
    return this.repositoryFactory.sendAsync('GET', environment.UrlConstants.GetRoles);
  }
}
