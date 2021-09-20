import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthModule } from './auth/auth.module';
import { HomeModule } from './home/home.module';
import { NoteModule } from './note/note.module';
import { ObjectiveModule } from './objective/objective.module';
import { ProjectModule } from './project/project.module';



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  exports: [
    AuthModule,
    HomeModule,
    NoteModule,
    ObjectiveModule,
    ProjectModule
  ]
})
export class FeaturesModule { }
