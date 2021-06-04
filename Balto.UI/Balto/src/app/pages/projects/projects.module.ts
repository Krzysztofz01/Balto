import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProjectsComponent } from './projects.component';
import { ProjectComponent } from './project/project.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AddModalComponent } from './add-modal/add-modal.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { InviteModalComponent } from './invite-modal/invite-modal.component';
import { AddTableModalComponent } from './add-table-modal/add-table-modal.component';
import { ProjectTableEntryComponent } from './project-table-entry/project-table-entry.component';
import { ProjectTableComponent } from './project-table/project-table.component';
import { AddTableEntryModalComponent } from './add-table-entry-modal/add-table-entry-modal.component';
import { ProjectTableEntryDetailModalComponent } from './project-table-entry-detail-modal/project-table-entry-detail-modal.component';
import { AddTrelloModalComponent } from './add-trello-modal/add-trello-modal.component';
import { ProjectTableTypeComponent } from './project-table-type/project-table-type.component';
import { ProjectTableTypeEntryComponent } from './project-table-type-entry/project-table-type-entry.component';



@NgModule({
  declarations: [ProjectsComponent, ProjectComponent, AddModalComponent, InviteModalComponent, AddTableModalComponent, ProjectTableEntryComponent, ProjectTableComponent, AddTableEntryModalComponent, ProjectTableEntryDetailModalComponent, AddTrelloModalComponent, ProjectTableTypeComponent, ProjectTableTypeEntryComponent ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    NgbModule,
    SharedModule
  ]
})
export class ProjectsModule { }
