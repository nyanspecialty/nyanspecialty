import { Injectable } from '@angular/core';
import { ApplicationUser } from '../models/appUser';

@Injectable({
    providedIn: 'root'
})
export class CommonPropsService {
    applicationUser: ApplicationUser = {} as ApplicationUser;
    constructor() { }

    prepareModelForSave(model: any): any {
        const appuser = sessionStorage.getItem("ApplicationUser");
        if (appuser) {
            this.applicationUser = JSON.parse(appuser);
        }
        const currentUserId = this.applicationUser.id || 1;
        const currentDate = new Date();
        model.createdBy = currentUserId;
        model.createdOn = currentDate;
        model.modifiedBy = currentUserId;
        model.modifiedOn = currentDate;
        model.isActive = model.isActive !== undefined ? model.isActive : true;
        return model;
    }
}