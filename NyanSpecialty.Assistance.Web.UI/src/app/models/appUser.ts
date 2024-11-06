export interface ApplicationUser  {
    id: number; 
    fullName: string;
    firstName: string;
    lastName: string;
    email: string;
    phone?: string; 
    customerId?: number; 
    providerId?: number;
    roleId?: number; 
    createdBy?: number; 
    createdOn?: Date;
    modifiedBy?: number; 
    modifiedOn?: Date;
    isActive: boolean; 
}