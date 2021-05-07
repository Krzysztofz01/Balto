import { User } from "./user.model";

export interface ProjectTableEntry {
    id: number;
    name: string;
    content: string;
    order: number;
    finished: boolean;
    priority: number;
    userAdded: User;
    userFinished: User;
}