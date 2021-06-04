import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { ProjectTableEntry } from 'src/app/core/models/project-table-entry.model';
import { ProjectTable } from 'src/app/core/models/project-table.model';
import { Project } from 'src/app/core/models/project.model';
import { User } from 'src/app/core/models/user.model';
import { ProjectService } from 'src/app/core/services/project.service';
import { AddTableEntryModalComponent } from '../add-table-entry-modal/add-table-entry-modal.component';
import { AddTableModalComponent } from '../add-table-modal/add-table-modal.component';
import { InviteModalComponent } from '../invite-modal/invite-modal.component';
import { ProjectSyncService } from '../project-sync/project-sync.service';
import { ViewSettings } from '../view-settings.interface';

@Component({
  selector: 'app-project-table-type',
  templateUrl: './project-table-type.component.html',
  styleUrls: ['./project-table-type.component.css']
})
export class ProjectTableTypeComponent implements OnInit, OnDestroy {
  @Input() viewSettings: ViewSettings;
  @Output() reloadEvent = new EventEmitter<Project>();

  public project: Project;

  public projectSubscription: Subscription;

  constructor(private projectService: ProjectService, private projectSyncService: ProjectSyncService, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.projectSubscription = this.projectSyncService.project.subscribe((p) => {
      this.project = p;
    });
  }

  ngOnDestroy(): void {
    this.projectSubscription.unsubscribe();
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

  public parseUserColor(user: User): string {
    if(user.team != null) {
      if(user.team.color.length) {
        return user.team.color;
      }
    }
    return '#000';
  }

  public reload(): void {
    this.reloadEvent.emit(this.project);
  }

  public deleteTable(table: ProjectTable): void {
    this.projectService.deleteOneProjectTable(this.project.id, table.id, 1).subscribe((res) => {
      this.reload();
    },
    (error) => {
      console.error(error);
    });
  }

  public addEntry(table: ProjectTable): void {
    const ref = this.modalService.open(AddTableEntryModalComponent);

    ref.result.then((result) => {
      const entry: ProjectTableEntry = result;
      this.projectService.postOneProjectTableEntry(this.project.id, table.id, entry, 1).subscribe((res) => {
        this.reload();
      },
      (error) => {
        console.error(error);
      });
    }, () => {});
  }

  public addTable(): void {
    const ref = this.modalService.open(AddTableModalComponent);

    ref.result.then((result) => {
      const projectTable: ProjectTable = result;
      this.projectService.postOneProjectTable(this.project.id, projectTable, 1).subscribe((res) => {
        this.reload();
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
        this.reload();
      },
      (error) => {
        console.error(error);
      });
    }, () => {});
  }
}
