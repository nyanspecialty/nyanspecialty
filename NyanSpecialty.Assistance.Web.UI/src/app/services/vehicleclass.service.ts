import { Injectable } from '@angular/core';
import { RepositoryFactory } from '../factory/repositoryfactory.service';
import { Observable } from 'rxjs';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root'
})
export class VehicleClassService {

  constructor(private repositoryFactory: RepositoryFactory) { }

  insertOrUpdateVehicleClassAsync(vehicleClass: any): Observable<any> {
    return this.repositoryFactory.sendAsync('POST', environment.UrlConstants.SaveVehicleClass, vehicleClass);
  }
  
  fetchVehicleClassesAsync(): Observable<any> {
    return this.repositoryFactory.sendAsync('GET', environment.UrlConstants.GetVehicleClasses);
  }
}
