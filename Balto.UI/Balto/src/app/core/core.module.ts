import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { NoteService } from './services/note.service';
import { LocalStorageService } from './services/local-storage.service';
import { AuthenticationModule } from './authentication/authentication.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    AuthenticationModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
    NoteService,
    LocalStorageService
  ]
})
export class CoreModule {

  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    if(parentModule) {
      throw new Error('Core module is already created! Only one instance should exist!');
    }
  }
}
