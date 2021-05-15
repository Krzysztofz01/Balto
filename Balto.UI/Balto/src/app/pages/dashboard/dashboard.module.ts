import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { ObjectiveWidgetComponent } from './objective-widget/objective-widget.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [DashboardComponent, ObjectiveWidgetComponent],
  imports: [
    CommonModule,
    RouterModule
  ]
})
export class DashboardModule { }
