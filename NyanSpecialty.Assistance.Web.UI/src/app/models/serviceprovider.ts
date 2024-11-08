import { CommonProps } from "./commonprops";

export interface ServiceProvider extends CommonProps {
    providerId: number; 
    name: string;
    contactNumber: string;
    email: string;
    serviceArea: string;
    availabilityStatus: string;
    rating: string;
    longitude: string;
    latitude: string;
    address: string;
}