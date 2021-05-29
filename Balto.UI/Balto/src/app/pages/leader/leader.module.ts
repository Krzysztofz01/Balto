import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LeaderComponent } from './leader.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AddTeamModalComponent } from './add-team-modal/add-team-modal.component';
import { ChangeTeamModalComponent } from './change-team-modal/change-team-modal.component';

@NgModule({
  declarations: [LeaderComponent, AddTeamModalComponent, ChangeTeamModalComponent],
  imports: [
    CommonModule,
    NgbModule,
    ReactiveFormsModule
  ]
})
export class LeaderModule { }
