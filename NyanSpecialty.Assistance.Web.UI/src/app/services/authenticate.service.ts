import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RepositoryFactory } from '../factory/repositoryfactory.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticateService {
  constructor(private repositoryFactory: RepositoryFactory) { }

  authenticateUser(userData: any): Observable<any> {
    const url = "/Account/AuthenticateUserAsync";
    return this.repositoryFactory.sendAsync('POST', url, userData);
  }

  generateUserClaims(authResponse: any): Observable<any> {
    const url = "/Account/GenarateUserClaimsAsync";
    return this.repositoryFactory.sendAsync('POST', url, authResponse);
  }
}