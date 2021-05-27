import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from 'src/app/authentication/services/auth.service';
import { ProjectTableEntry } from 'src/app/core/models/project-table-entry.model';
import { DateParserService } from 'src/app/core/services/date-parser.service';
import { ProjectService } from 'src/app/core/services/project.service';
import { ProjectTableEntryDetailModalComponent } from '../project-table-entry-detail-modal/project-table-entry-detail-modal.component';
import { ViewSettings } from '../view-settings.interface';

@Component({
  selector: 'app-project-table-entry',
  templateUrl: './project-table-entry.component.html',
  styleUrls: ['./project-table-entry.component.css']
})
export class ProjectTableEntryComponent implements OnInit {
  @Input() entry: ProjectTableEntry;
  @Input() tableId: number;
  @Input() projectId: number;
  @Input() viewSettings: ViewSettings;
  @Output() reloadEvent = new EventEmitter<ProjectTableEntry>();
  
  public status: boolean;

  constructor(private projectService: ProjectService, private modalService: NgbModal, private dateService: DateParserService, private authService: AuthService) { }

  ngOnInit(): void {
    this.status = this.entry.finished;
  }

  public viewDetails(): void {
    const ref = this.modalService.open(ProjectTableEntryDetailModalComponent);
    ref.componentInstance.entry = this.entry;

    ref.result.then((result) => {
      const entry: ProjectTableEntry = result;
      this.projectService.patchOneProjectTableEntry(this.projectId, this.tableId, this.entry.id, entry, 1).subscribe((res) => {
        this.reloadEvent.emit(entry);
      },
      (error) => {
        console.error(error);
      });
    }, () => {});
  }

  public changeState(): void {
    this.projectService.changeState(this.projectId, this.tableId, this.entry.id, 1).subscribe((resState) => {
      this.projectService.getOneProjectTableEntry(this.projectId, this.tableId, this.entry.id, 1).subscribe((resEntry) => {
        this.entry = resEntry;
        this.status = resEntry.finished;
      },
      (error) => {
        console.error(error);
      });
    },
    (error) => {
      console.error(error);
    });
  }

  public addedDaysAgo(): string {
    return this.dateService.daysAgo(new Date(this.entry.startingDate)).toString();
  }

  public finishedDaysAgo(): string {
    return this.dateService.daysAgo(new Date(this.entry.finishDate)).toString();
  }

  public isDeadline(): boolean {
    return this.dateService.inDays(new Date(this.entry.endingDate)) < 1;
  }

  public deadlineInDays(): number {
    return Math.abs(this.dateService.inDays(new Date(this.entry.endingDate)));
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

  public cardHeaderClass(): string {
    if(this.entry.finished) return 'finished';
    if(new Date(this.entry.endingDate).getTime() < new Date(Date.now()).getTime()) return 'expired';
    return '';
  }

  public priorityClass(): string {
    if(this.entry.priority == 1) return 'important';
    if(this.entry.priority == 2) return 'leading';
    return '';
  }

}
