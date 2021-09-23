import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthPageGuard } from './core/guards/auth-page.guard';
import { AuthGuard } from './core/guards/auth.guard';
import { AuthComponent } from './features/auth/auth.component';
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { HomeComponent } from './features/home/home.component';
import { NoteComponent } from './features/note/note.component';
import { ObjectiveComponent } from './features/objective/objective.component';
import { ProjectComponent } from './features/project/project.component';

const routes: Routes = [
  { 
    path: '',
    component: HomeComponent,
    canActivate: [ AuthGuard ],
    children: [
      { path: '', component: DashboardComponent, canActivate: [ AuthGuard ] },
      { path: 'projects', component: ProjectComponent, canActivate: [ AuthGuard ] },
      { path: 'objectives', component: ObjectiveComponent, canActivate: [ AuthGuard ] },
      { path: 'notes', component: NoteComponent, canActivate: [ AuthGuard ] }
    ]
  },
  { path: 'login', component: AuthComponent, canActivate: [ AuthPageGuard ] },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
