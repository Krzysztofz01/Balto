import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HomeModule } from './home/home.module';
import { LoginModule } from './login/login.module';
import { NotesModule } from './notes/notes.module';
import { ObjectivesModule } from './objectives/objectives.module';
import { ProjectsModule } from './projects/projects.module';
import { SettingsModule } from './settings/settings.module';
import { LeaderModule } from './leader/leader.module';
import { DashboardModule } from './dashboard/dashboard.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HomeModule,
    LoginModule,
    NotesModule,
    ObjectivesModule,
    ProjectsModule,
    SettingsModule,
    LeaderModule,
    RouterModule,
    DashboardModule
  ],
  exports: [
    HomeModule,
    LoginModule,
    NotesModule,
    ObjectivesModule,
    ProjectsModule,
    SettingsModule,
    LeaderModule,
    DashboardModule
  ]
})
export class PagesModule { }
