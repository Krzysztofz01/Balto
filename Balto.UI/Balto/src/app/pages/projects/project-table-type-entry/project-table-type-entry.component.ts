import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from 'src/app/authentication/services/auth.service';
import { ProjectTableEntry } from 'src/app/core/models/project-table-entry.model';
import { DateParserService } from 'src/app/core/services/date-parser.service';
import { ProjectService } from 'src/app/core/services/project.service';
import { SoundService } from 'src/app/core/services/sound.service';
import { ProjectTableEntryDetailModalComponent } from '../project-table-entry-detail-modal/project-table-entry-detail-modal.component';
import { ViewSettings } from '../view-settings.interface';

@Component({
  selector: 'app-project-table-type-entry',
  templateUrl: './project-table-type-entry.component.html',
  styleUrls: ['./project-table-type-entry.component.css']
})
export class ProjectTableTypeEntryComponent implements OnInit {
  @Input() entry: ProjectTableEntry;
  @Input() tableId: number;
  @Input() projectId: number;
  @Input() viewSettings: ViewSettings;
  @Output() reloadEvent = new EventEmitter<ProjectTableEntry>();

  public status: boolean;

  constructor(private projectService: ProjectService, private modalService: NgbModal, private soundService: SoundService, private dateService: DateParserService, private authService: AuthService) { }

  ngOnInit(): void {
    this.status = this.entry.finished;
  }

  public viewDetails(): void {
    const ref = this.modalService.open(ProjectTableEntryDetailModalComponent);
    ref.componentInstance.entry = this.entry;

    ref.result.then((result) => {
      if(result == null) {
        this.projectService.deleteOneProjectTableEntry(this.projectId, this.tableId, this.entry.id, 1).subscribe((res) => {
          this.reloadEvent.emit(null);
        },
        (error) => {
          console.error(error);
        });
        return;
      }

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
    this.soundService.play('notification1');
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

  public entryColor(): string {
    if(this.entry.color != null) {
      return this.entry.color;
    }
    return '#000';
  }
}
