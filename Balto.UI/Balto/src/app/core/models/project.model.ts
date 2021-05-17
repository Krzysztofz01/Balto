import { ProjectTable } from "./project-table.model";
import { User } from "./user.model";

export interface Project {
    id: number;
    name: string;
    owner: User;
    readWriteUsers: Array<User>;
    tabels: Array<ProjectTable>;
}