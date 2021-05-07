import { ProjectTable } from "./project-table.model";
import { User } from "./user.model";

export interface Project {
    id: number;
    name: string;
    owner: User;
    readWriteUser: Array<User>;
    tabels: Array<ProjectTable>;
}