import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ProjectTableEntry } from 'src/app/core/models/project-table-entry.model';
import { ProjectTable } from 'src/app/core/models/project-table.model';
import { ProjectService } from 'src/app/core/services/project.service';
import { AddTableEntryModalComponent } from '../add-table-entry-modal/add-table-entry-modal.component';

@Component({
  selector: 'app-project-table',
  templateUrl: './project-table.component.html',
  styleUrls: ['./project-table.component.css']
})
export class ProjectTableComponent implements OnInit {
  @Input() table: ProjectTable;
  @Input() projectId: number;
  @Output() reloadEvent = new EventEmitter<ProjectTable>();

  constructor(private projectService: ProjectService, private modalService: NgbModal) { }

  ngOnInit(): void {
  }

  public addEntry(): void {
    const ref = this.modalService.open(AddTableEntryModalComponent);

    ref.result.then((result) => {
      const entry: ProjectTableEntry = result;

      this.projectService.postOneProjectTableEntry(this.projectId, this.table.id, entry, 1).subscribe((res) => {
        this.reloadEvent.emit(this.table);
      },
      (error) => {
        console.error(error);
      });
    }, () => {});
  }

}
