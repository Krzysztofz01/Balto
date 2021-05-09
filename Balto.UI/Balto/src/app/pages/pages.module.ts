import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeModule } from './home/home.module';
import { LoginModule } from './login/login.module';
import { NotesModule } from './notes/notes.module';
import { ObjectivesModule } from './objectives/objectives.module';
import { ProjectsModule } from './projects/projects.module';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HomeModule,
    LoginModule,
    NotesModule,
    ObjectivesModule,
    ProjectsModule
  ],
  exports: [
    HomeModule,
    LoginModule,
    NotesModule,
    ObjectivesModule,
    ProjectsModule
  ]
})
export class PagesModule { }
