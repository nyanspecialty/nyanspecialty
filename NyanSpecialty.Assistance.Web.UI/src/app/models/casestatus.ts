import { CommonProps } from "./commonprops";

export interface CaseStatus extends CommonProps {
    caseStatusId: number;
    caseId?: number;
    statusId?: number;
    notes?: string;
}
