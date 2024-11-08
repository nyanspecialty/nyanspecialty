import { Injectable } from '@angular/core';
import { RepositoryFactory } from '../factory/repositoryfactory.service';
import { Observable } from 'rxjs';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private repositoryFactory: RepositoryFactory) { }

  insertOrUpdateUserAsync(user: any): Observable<any> {
    return this.repositoryFactory.sendAsync('POST', environment.UrlConstants.SaveUser, user);
  }
  fetchUsersAsync(): Observable<any> {
    return this.repositoryFactory.sendAsync('GET', environment.UrlConstants.GetUsers);
  }
  fetchUserById(userId: any): Observable<any> {
    return this.repositoryFactory.sendAsync("GET", `${environment.UrlConstants.GetUserById}/${userId}`);
  }
}
