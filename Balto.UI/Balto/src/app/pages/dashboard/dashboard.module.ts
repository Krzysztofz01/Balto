import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { RouterModule } from '@angular/router';
import { SummaryWidgetComponent } from './summary-widget/summary-widget.component';

@NgModule({
  declarations: [DashboardComponent, SummaryWidgetComponent],
  imports: [
    CommonModule,
    RouterModule
  ]
})
export class DashboardModule { }
