import { User } from "./user.model";

export interface ProjectTableEntry {
    id: number;
    name: string;
    content: string;
    order: number;
    finished: boolean;
    priority: number;
    color: string;
    userAdded: User;
    userFinished: User;
    startingDate: Date;
    endingDate: Date;
    finishDate: Date;
    notify: boolean;
}