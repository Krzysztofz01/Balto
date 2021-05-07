import { ProjectTableEntry } from "./project-table-entry.model";

export interface ProjectTable {
    id: number;
    name: string;
    entries: Array<ProjectTableEntry>;
}