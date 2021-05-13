import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ObjectivesComponent } from './objectives.component';
import { CardComponent } from './card/card.component';
import { AddModalComponent } from './add-modal/add-modal.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';



@NgModule({
  declarations: [ObjectivesComponent, CardComponent, AddModalComponent],
  imports: [
    CommonModule,
    NgbModule,
    ReactiveFormsModule,
    SharedModule
  ]
})
export class ObjectivesModule { }
