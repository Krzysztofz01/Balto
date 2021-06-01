import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/authentication/services/auth.service';
import { Note } from 'src/app/core/models/note.model';
import { Objective } from 'src/app/core/models/objective.model';
import { Project } from 'src/app/core/models/project.model';
import { Team } from 'src/app/core/models/team.model';
import { NoteService } from 'src/app/core/services/note.service';
import { ObjectiveService } from 'src/app/core/services/objective.service';
import { ProjectService } from 'src/app/core/services/project.service';
import { UserService } from 'src/app/core/services/user.service';

@Component({
  selector: 'app-summary-widget',
  templateUrl: './summary-widget.component.html',
  styleUrls: ['./summary-widget.component.css']
})
export class SummaryWidgetComponent implements OnInit {
  public userId: number;
  public userName: string;
  public userEmail: string;
  public userTeam: Team;
  
  public objectives: Array<Objective>;
  public objCount: number
  public objFinished: number;
  public objDailyCount: number;
  public objDailyFinished: number;
  public objOverDeadline: number;
  
  public ftProjects: Array<Project>;
  public projCount: number;
  public projAny: boolean;

  public ftNotes: Array<Note>;
  public noteCount: number;
  public noteAny: boolean;

  public success: string = 'var(--bs-success)';
  public danger: string = 'var(--bs-danger)';

  constructor(private authService: AuthService, private objectiveService: ObjectiveService, private noteService: NoteService, private projectService: ProjectService, private userService: UserService) { }

  ngOnInit(): void {
    this.initializeDate();
  }

  private initializeDate(): void {
    //User
    const user = this.authService.userValue;
    this.userId = user.id;
    this.userName = user.name;
    this.userEmail = user.email;
    this.userService.getOne(user.id, 1).subscribe((res) => {
      this.userTeam = res.team;
    },
    (error) => {
      console.error(error);
    });

    //Objectives
    this.objectives = new Array<Objective>();
    this.objectiveService.getAll(1).subscribe((res) => {
      this.objectives = res;

      this.objCount = res.filter(o => o.daily == false).length;
      this.objFinished = res.filter(o => o.daily == false && o.finished == true).length;
      this.objDailyCount = res.filter(o => o.daily == true).length;
      this.objDailyFinished = res.filter(o => o.daily == true && o.finished == true).length;
      this.objOverDeadline = res.filter(o => new Date(o.endingDate).getTime() < Date.now() && o.finished == false).length;
    },
    (error) => {
      console.error(error);
    });

    //Projects
    this.ftProjects = new Array<Project>();
    this.projAny = false;
    this.projectService.getAllProjects(1).subscribe((res) => {
      if(res.length > 0) this.projAny = true;
      
      if(res.length > 3) {
        res.every((p, i) => {
          if(i >= 3) return false;
          this.ftProjects.push(p);
          return true;
        });

        this.projCount = res.length - 3;
      } else {
        this.ftProjects = res;
        this.projCount = 0;
      }
    },
    (error) => {
      console.error(error);
    });

    //Notes
    this.ftNotes = new Array<Note>();
    this.noteAny = false
    this.noteService.getAll(1).subscribe((res) => {
      if(res.length > 0) this.noteAny = true;
      
      if(res.length > 3) {
        res.every((n, i) => {
          if(i >= 3) return false;
          this.ftNotes.push(n);
          return true;
        });

        this.noteCount = res.length - 3;
      } else {
        this.ftNotes = res;
        this.noteCount = 0;
      }
    },
    (error) => {
      console.error(error);
    });
  }

  public calcProg(current: number, max: number): string {
    return `${ Number((current * 100) / max).toFixed() }%`;
  }

  public projectUserStatus(project: Project): string {
    if(project.owner.id == this.userId) return 'Owner';
    return 'Contributor';
  }

  public noteUserStatus(note: Note): string {
    if(note.owner.id == this.userId) return 'Owner';
    return 'Contributor';
  }
}
