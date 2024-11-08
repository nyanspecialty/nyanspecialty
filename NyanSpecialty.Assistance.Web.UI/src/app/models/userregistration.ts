import { CommonProps } from "./commonprops";

export interface UserRegistration extends CommonProps {
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    phone: string;
    password: string;
    customerId?: number;
    providerId?: number;
    roleId?: number;
    lastPasswordChangedOn?: Date;
    isBlocked?: boolean;
}
