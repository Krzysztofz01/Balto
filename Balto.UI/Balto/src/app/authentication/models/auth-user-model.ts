export interface AuthUser {
    id: number;
    email: string;
    name: string;
    role: string;
    jwt?: string; 
}