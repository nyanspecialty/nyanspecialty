import { CommonProps } from "./commonprops";

export interface ServiceProviderAssignment extends CommonProps {
    assignmentId: number;
    caseId?: number;
    serviceProviderId?: number;
    assignedOn?: string;
    response?: string;
    responseOn?: string;
}
