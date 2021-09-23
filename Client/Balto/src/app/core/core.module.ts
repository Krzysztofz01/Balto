import { APP_INITIALIZER, NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { BearerTokenInterceptor } from './interceptors/bearer-token.interceptor';
import { authInitializer } from './initializers/auth.initializer';
import { AuthService } from '../authentication/services/auth.service';



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: BearerTokenInterceptor, multi: true },
    { provide: APP_INITIALIZER, useFactory: authInitializer, multi: true, deps: [ AuthService ] }
  ]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    if (parentModule) {
      throw new Error('Core module is already created! Only one instance can exist!');
    }
  }
}
