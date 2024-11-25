import { Case } from "./case";
import { CaseStatus } from "./casestatus";
import { ServiceProviderAssignment } from "./serviceproviderassignment";

export interface CaseDetails {
    caseDetails: Case,
    caseStatuses: CaseStatus[];
    serviceProviderAssignment: ServiceProviderAssignment[];
    isChecked?: boolean
}
