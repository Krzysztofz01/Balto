import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { NotificationComponent } from './notification/notification.component';



@NgModule({
  declarations: [NavbarComponent, FooterComponent, NotificationComponent],
  imports: [
    CommonModule
  ],
  exports: [
    NavbarComponent,
    FooterComponent,
    NotificationComponent
  ]
})
export class SharedModule { }
