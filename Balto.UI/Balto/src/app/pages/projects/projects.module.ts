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



@NgModule({
  declarations: [ProjectsComponent, ProjectComponent, AddModalComponent, InviteModalComponent, AddTableModalComponent, ProjectTableEntryComponent, ProjectTableComponent, AddTableEntryModalComponent ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    NgbModule,
    SharedModule
  ]
})
export class ProjectsModule { }
