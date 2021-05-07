import { Team } from "./team.model";

export interface User {
    id: number;
    name: string;
    email: string;
    team: Team;
    isLeader: boolean;
}
