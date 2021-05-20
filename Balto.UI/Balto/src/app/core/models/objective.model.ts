export interface Objective {
    id: number;
    name: string;
    description: string;
    finished: boolean;
    daily: boolean;
    startingDate: Date;
    endingDate: Date;
    notify: boolean;
}
