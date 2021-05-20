import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from 'src/app/authentication/services/auth.service';
import { ProjectTableEntry } from 'src/app/core/models/project-table-entry.model';
import { ProjectService } from 'src/app/core/services/project.service';

@Component({
  selector: 'app-project-table-entry',
  templateUrl: './project-table-entry.component.html',
  styleUrls: ['./project-table-entry.component.css']
})
export class ProjectTableEntryComponent implements OnInit {
  @Input() entry: ProjectTableEntry;
  @Input() tableId: number;
  @Input() projectId: number;
  
  public status: boolean;

  constructor(private projectService: ProjectService, private modalService: NgbModal, private authService: AuthService) { }

  ngOnInit(): void {
    this.status = this.entry.finished;
  }

  public viewDetails(): void {
    
  }

  public changeState(): void {
    this.entry.finished = true;
    //finished date to today
    // this.entry.userFini/shed = this.authService.userValue.
  }

  public addedDaysAgo(): string {
    //const today = new Date(Date.now());
    //const timeDiff = today.getTime() - this.entry.
    return '3';
  }

  public finishedDaysAgo(): string {
    const today = new Date(Date.now());
    // const timeDiff = today.getTime() - this.entry
    return '3';
  }

  public deadlineInDays(): string {
    return '3';
  }

  public userAdded(): string {
    if(this.entry.userAdded != null) {
      if(this.entry.userAdded.team != null) {
        return `${ this.entry.userAdded.name } - ${ this.entry.userAdded.team.name }`;
      }
      return `${ this.entry.userAdded.name }`;
    }
    return 'Unknown';
  }

  public userFinished(): string {
    if(this.entry.userFinished != null) {
      if(this.entry.userFinished.team != null) {
        return `${ this.entry.userFinished.name } - ${ this.entry.userFinished.team.name }`;
      }
      return `${ this.entry.userFinished.name }`;
    }
    return 'Unknown';
  }

  public cardHeader(): string {
    if(this.entry.finished) return 'finished';
    // if(new Date(this.objective.endingDate).getTime() < new Date(Date.now()).getTime()) return 'expired';
    return '';
  }

}
