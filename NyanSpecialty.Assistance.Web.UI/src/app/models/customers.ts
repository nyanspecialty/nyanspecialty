import { CommonProps } from "./commonprops";

export interface Customers extends CommonProps {
    customerID: number;
    name: string;
    contactNumber: string;
    email: string;
    address: string;
    insurancePolicyID?: number; 
}