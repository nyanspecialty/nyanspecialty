import { Injectable } from '@angular/core';
import { RepositoryFactory } from '../factory/repositoryfactory.service';
import { Observable } from 'rxjs';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root'
})
export class VehiclesizeService {

  constructor(private repositoryFactory: RepositoryFactory) { }

  insertOrUpdateVehiclesizeAsync(vehiclesize: any): Observable<any> {
    return this.repositoryFactory.sendAsync('POST', environment.UrlConstants.SaveVehicleSize, vehiclesize);
  }
  
  fetchVehiClesizesAsync(): Observable<any> {
    return this.repositoryFactory.sendAsync('GET', environment.UrlConstants.GetVehicleSizes);
  }
}
