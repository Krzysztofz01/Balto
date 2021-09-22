import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthComponent } from './auth.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RegistrationModalComponent } from './registration-modal/registration-modal.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';



@NgModule({
  declarations: [
    AuthComponent,
    RegistrationModalComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    NgbModule
  ]
})
export class AuthModule { }
