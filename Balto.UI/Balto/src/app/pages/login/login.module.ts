import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from './../../shared/shared.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RegisterModalComponent } from './register-modal/register-modal.component';

@NgModule({
  declarations: [LoginComponent, RegisterModalComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    SharedModule,
    NgbModule
  ]
})
export class LoginModule { }
