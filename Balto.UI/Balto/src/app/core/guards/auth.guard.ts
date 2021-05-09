import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { AuthService } from 'src/app/authentication/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  
  constructor(private router: Router, private authService: AuthService) { }
  
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const user = this.authService.userValue;
    if (user) {
      return true;
    } else {
      this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
      return false;
    }
  }
  
}
