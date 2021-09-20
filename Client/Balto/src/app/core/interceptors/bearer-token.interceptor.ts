import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/authentication/services/auth.service';
import { environment } from 'src/environments/environment';

@Injectable()
export class BearerTokenInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const isAuthenticated = this.authService.currentUserClaims != null;
    const isBackendRequest = request.url.startsWith(environment.serverUrl);

    if(isAuthenticated && isBackendRequest) {
      request = request.clone({
        setHeaders: { Authorization: `Bearer ${ this.authService.currentUserClaims.token }` }
      });
    }

    return next.handle(request);
  }
}
