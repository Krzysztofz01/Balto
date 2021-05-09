import { APP_INITIALIZER, NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { NoteService } from './services/note.service';
import { LocalStorageService } from './services/local-storage.service';
import { ObjectiveService } from './services/objective.service';
import { ProjectService } from './services/project.service';
import { TeamService } from './services/team.service';
import { TrelloIntegrationService } from './services/trello-integration.service';
import { UserService } from './services/user.service';
import { PermissionErrorInterceptor } from './interceptors/permission-error.interceptor';
import { appInitializer } from './initializers/app.initializer';
import { AuthService } from '../authentication/services/auth.service';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [
    { provide: APP_INITIALIZER, useFactory: appInitializer, multi: true, deps: [AuthService]},
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: PermissionErrorInterceptor, multi: true },

    NoteService,
    LocalStorageService,
    ObjectiveService,
    ProjectService,
    TeamService,
    TrelloIntegrationService,
    UserService
  ]
})
export class CoreModule {

  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    if(parentModule) {
      throw new Error('Core module is already created! Only one instance should exist!');
    }
  }
}
