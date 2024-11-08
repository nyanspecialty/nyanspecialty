import { CommonProps } from "./commonprops";

export interface User extends CommonProps {
    userId: number;
    firstName: string;
    lastName: string;
    email: string;
    phone: string;
    passwordHash: string;
    passwordSalt: string;
    customerId?: number;
    providerId?: number;
    roleId?: number;
    lastPasswordChangedOn?: Date;
    isBlocked?: boolean;
}
