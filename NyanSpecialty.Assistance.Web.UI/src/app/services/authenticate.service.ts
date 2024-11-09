import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { RepositoryFactory } from '../factory/repositoryfactory.service';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticateService {
  private isLoggedInSubject: BehaviorSubject<boolean>;
  isLoggedIn$: Observable<boolean>;
  constructor(private repositoryFactory: RepositoryFactory) {
    // Initialize the BehaviorSubject and Observable here
    this.isLoggedInSubject = new BehaviorSubject<boolean>(this.isAuthenticated());
    this.isLoggedIn$ = this.isLoggedInSubject.asObservable();
  }

  authenticateUser (userData: any): Observable<any> {
    return this.repositoryFactory.sendAsync('POST', environment.UrlConstants.Authenticate, userData);
  }

  generateUserClaims(authResponse: any): Observable<any> {
    return this.repositoryFactory.sendAsync('POST', environment.UrlConstants.GenerateUserClaims, authResponse);
  }

  isAuthenticated(): boolean {
    const token = sessionStorage.getItem('AccessToken');
    if (!token) return false;

    const expiration = sessionStorage.getItem('tokenExpiration'); 
    if (expiration && new Date().getTime() > +expiration) {
      this.logout();
      return false;
    }
    return true;
  }

  loginSuccess(token: string, expirationDuration: number) {
    sessionStorage.setItem('AccessToken', token);
    const expirationTime = new Date().getTime() + expirationDuration;
    sessionStorage.setItem('tokenExpiration', expirationTime.toString());
    this.isLoggedInSubject.next(true);
  }

  logout() {
    sessionStorage.removeItem('AccessToken');
    sessionStorage.removeItem('tokenExpiration');
    if (this.isLoggedInSubject) { // Check if defined
      this.isLoggedInSubject.next(false);
    }
  }
}