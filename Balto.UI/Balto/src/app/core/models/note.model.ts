import { User } from "./user.model";

export interface Note {
    id: number;
    name: string;
    content: string;
    owner: User;
    readWriteUsers: Array<User>;
}
