import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/authentication/services/auth.service';
import { ProjectTable } from 'src/app/core/models/project-table.model';
import { Project } from 'src/app/core/models/project.model';
import { User } from 'src/app/core/models/user.model';
import { ProjectService } from 'src/app/core/services/project.service';
import { AddTableModalComponent } from '../add-table-modal/add-table-modal.component';
import { InviteModalComponent } from '../invite-modal/invite-modal.component';
import { ProjectSyncService } from '../project-sync/project-sync.service';
import { ViewSettings } from '../view-settings.interface';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css']
})
export class ProjectComponent implements OnInit, OnDestroy {
  @Input() viewSettings: ViewSettings;
  @Output() reloadEvent = new EventEmitter<Project>();
  public project: Project;
  public selectedTable: ProjectTable;

  public projectSubscription: Subscription;

  constructor(private projectService: ProjectService, private projectSyncService: ProjectSyncService, private authService: AuthService, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.projectSubscription = this.projectSyncService.project.subscribe(p => {
      this.project = p;

      //By default select the first table
      if(p.tabels.length > 0) {
        this.selectedTable = this.project.tabels[0];
      } else {
        this.selectedTable = null;
      } 
    });
  }

  ngOnDestroy(): void {
    this.projectSubscription.unsubscribe();
  }

  public parseUserForTableClass(user: User): string {
    if(user.isLeader) return 'leader';
    if(user.id == this.project.owner.id) return 'owner';
    return 'default';
  }

  public parseUserForTable(user: User): string {
    let name = user.name;
    if(this.project.owner.id == user.id) name = `${ name } (Owner)`;
    if(user.isLeader) name = `${ name } (Leader)`;
    if(user.team != null) {
      return `${ name } - ${ user.team.name }`;
    }
    return `${ name }`;
  }

  public addTable(): void {
    const ref = this.modalService.open(AddTableModalComponent);

    ref.result.then((result) => {
      const projectTable: ProjectTable = result;
      this.projectService.postOneProjectTable(this.project.id, projectTable, 1).subscribe((res) => {
        //Emit the relaod event to update tables from API
        this.reloadEvent.emit(this.project);
      },
      (error) => {
        console.error(error);
      });
    }, () => {});
  }

  public inviteUser(): void {
    const ref = this.modalService.open(InviteModalComponent);

    ref.result.then((result) => {
      const email: string = result;
      console.log(email);
      this.projectService.inviteUser(this.project.id, { email }, 1).subscribe((res) => {
        //Emit the relaod event to update tables from API
        this.reloadEvent.emit(this.project);
      },
      (error) => {
        console.error(error);
      });
    }, () => {});
  }

  public isOwner(): boolean {
    return this.authService.userValue.id == this.project.owner.id;
  }

  public selectTable(table: ProjectTable): void {
    this.selectedTable = table;
  }

  public reload($event): void {
    this.reloadEvent.emit(this.project);
    this.selectedTable = $event;
  }

  public parseUserColor(user: User): string {
    if(user.team != null) {
      if(user.team.color.length) {
        return user.team.color;
      }
    }
    return '#000';
  }

  public leaveProject(): void {
    this.projectService.leave(this.project.id, 1).subscribe((res) => {
      this.reloadEvent.emit(null);
    },
    (error) => {
      console.error(error);
    });
  }
}
