import { CommonProps } from "./commonprops";

export interface Case extends CommonProps {
    caseId: number;
    insurancePolicyId?: number;
    description?: string;
    customerName?: string;
    phone?: string;
    email?: string;
    currentLocation?: string;
    langitude?: string;
    latitude?: string;
    serviceTypeId?: number;
    statusId?: number;
    serviceProviderId?: number;
    serviceRequestDate?: Date;
    responseTime?: Date;
    completionTime?: Date;
    rating?: number;
    feedback?: string;
    priority?: number;
    notes?: string;
}
