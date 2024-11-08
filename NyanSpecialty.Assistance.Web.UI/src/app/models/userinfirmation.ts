import { CommonProps } from "./commonprops";

export interface UserInfirmation extends CommonProps {
    id?: number;
    firstName: string;
    lastName: string;
    fullName: string;
    email: string;
    phone: string;
    customerId?: number;
    providerId?: number;
    roleId?: number;
    lastPasswordChangedOn?: string;
    isBlocked?: boolean;
}
