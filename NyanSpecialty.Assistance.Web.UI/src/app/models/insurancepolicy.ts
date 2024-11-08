import { CommonProps } from "./commonprops";

export interface InsurancePolicy extends CommonProps {
    insurancePolicyId: number;
    policyReference: string;
    vin: string;
    carRegistrationNo: string;
    vehicleClass: string;
    vehicleSize: string;
    customerName: string;
    customerPhone: string;
    customerEmail: string;
    customerLifetimeValue?: number;
    gender?: string;
    education: string;
    employmentStatus: string;
    income?: number;
    maritalStatus: string;
    policyType: string;
    policy: string;
    coverage: string;
    effectiveToDate?: Date;
    monthlyPremiumAuto?: number;
    monthsSinceLastClaim?: number;
    monthsSincePolicyInception?: number;
    numberOfOpenComplaints?: number;
    numberOfPolicies?: number;
    renewOfferType: string;
    salesChannel: string;
    totalClaimAmount?: number;
    state: string;
    locationCode: string;
    response: string;
}