export interface PolicyType {
    policyTypeId: number;
    name: string;
    code: string;
    createdBy?: number;
    createdOn?: string;
    modifiedBy?: number;
    modifiedOn?: string; 
    isActive: boolean;
};