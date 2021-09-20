import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from 'src/app/authentication/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private authService: AuthService) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if(this.authService.userClaims) {
      if(route.url[0].path == 'login') {
        this.router.navigate(['']);
        return false;
      }
      
      return true;      
    }

    this.router.navigate(['/login']);
    return false;
  }
}
