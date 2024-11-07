export interface Workflow {
    workFLowId: number;
    name: string;
    code: string;
    createdBy?: number;
    createdOn?: Date;
    modifiedBy?: number;
    modifiedOn?: Date;
    isActive: boolean;
    isChecked?: boolean;
}
