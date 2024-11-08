import { CommonProps } from "./commonprops";

export interface Role extends CommonProps{
    roleId: number;
    name: string;
    code: string;
}