export interface ServiceType {
    serviceTypeId: number;
    serviceName: string;
    description: string;
    createdBy?: number;
    createdOn?: Date;
    modifiedBy?: number;
    modifiedOn?: Date;
    isActive: boolean;
    isChecked?: boolean;
}
