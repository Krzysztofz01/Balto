import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from 'src/app/authentication/services/auth.service';
import { ProjectTableEntry } from 'src/app/core/models/project-table-entry.model';
import { ProjectTable } from 'src/app/core/models/project-table.model';
import { Project } from 'src/app/core/models/project.model';
import { ProjectService } from 'src/app/core/services/project.service';
import { SoundService } from 'src/app/core/services/sound.service';
import { AddTableEntryModalComponent } from '../add-table-entry-modal/add-table-entry-modal.component';
import { ViewSettings } from '../view-settings.interface';

@Component({
  selector: 'app-project-table',
  templateUrl: './project-table.component.html',
  styleUrls: ['./project-table.component.css']
})
export class ProjectTableComponent implements OnInit {
  @Input() table: ProjectTable;
  @Input() project: Project;
  @Input() viewSettings: ViewSettings;
  @Output() reloadEvent = new EventEmitter<ProjectTable>();

  constructor(private projectService: ProjectService, private authService: AuthService, private soundService: SoundService, private modalService: NgbModal) { }

  ngOnInit(): void {
  }

  public addEntry(): void {
    const ref = this.modalService.open(AddTableEntryModalComponent);

    ref.result.then((result) => {
      const entry: ProjectTableEntry = result;
      this.projectService.postOneProjectTableEntry(this.project.id, this.table.id, entry, 1).subscribe((res) => {
        //this.reloadEvent.emit(this.table);
        this.refreshEntries()
      },
      (error) => {
        console.error(error);
      });
    }, () => {});
  }

  public isOwner(): boolean {
    return this.authService.userValue.id == this.project.owner.id;
  }

  public deleteTable(): void {
    this.soundService.play('delete1');
    this.projectService.deleteOneProjectTable(this.project.id, this.table.id, 1).subscribe((res) => {
      this.reloadEvent.emit(this.table);
    },
    (error) => {
      console.error(error);
    });
  }

  public reloadEntries(): void {
    this.refreshEntries();
  }

  private refreshEntries(): void {
    //After successful data modification of table content fetch only this one table entries and swap them with the old ones
    this.projectService.getAllProjectTableEntries(this.project.id, this.table.id, 1).subscribe((res) => {
      this.table.entries = res;
    },
    (error) => {
      console.error(error);
    });
  }
}
