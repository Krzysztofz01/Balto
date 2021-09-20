import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthResponse } from '../models/auth-response.model';
import { UserClaims } from '../models/user-claims.model';
import jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private userClaimsSubject: BehaviorSubject<UserClaims>;
  private userClaims: Observable<UserClaims>;

  private refreshTokenTimeout: any;

  private readonly authVersion = 1;
  private readonly refreshCookieName = 'balto_refresh_cookie';

  constructor(private router: Router, private httpClient: HttpClient, private cookieService: CookieService) {
    this.userClaimsSubject = new BehaviorSubject<UserClaims>(null);
    this.userClaims = this.userClaimsSubject.asObservable();
  }

  private getServerPath(): string {
    return `${ environment.serverUrl }/api/v${ this.authVersion }/auth`;
  }

  public get currentUserClaims(): UserClaims {
    return this.userClaimsSubject.value;
  }

  public login(email: string, password: string) : Observable<AuthResponse> {
    return this.httpClient.post<AuthResponse>(this.getServerPath(), { email, password }, { withCredentials: true })
      .pipe(map(res => {
        const userClaims = this.mapUserClaims(res.token);

        this.userClaimsSubject.next(userClaims);
        this.startRefreshTokenTimer();
        return res;
      }));
  }

  public refresh(): Observable<AuthResponse> {
    return this.httpClient.post<AuthResponse>(`${ this.getServerPath() }/refresh`, {}, { withCredentials: true })
      .pipe(map(res => {
        const userClaims = this.mapUserClaims(res.token);

        this.userClaimsSubject.next(userClaims);
        this.startRefreshTokenTimer();
        return res;
      }));
  }

  public register(name: string, email: string, password: string, passwordRepeat: string): Observable<void> {
    return this.httpClient.post<void>(`${ this.getServerPath() }/register`, { name, email, password, passwordRepeat }, { withCredentials: true });
  }

  public resetPassword(password: string, passwordRepeat: string): Observable<void> {
    return this.httpClient.post<void>(`${ this.getServerPath() }/reset`, { password, passwordRepeat }, { withCredentials: true })
      .pipe(map(res => {
        this.handleClientSideLogout();

        return res;
      }));
  }

  public logout(): void {
    this.httpClient.post<void>(`${ this.getServerPath() }/logout`, {}, { withCredentials: true }).subscribe(() => {
      this.handleClientSideLogout();
    },
    (error) => {
      console.error(error);
      this.handleClientSideLogout();
    });
  }

  private handleClientSideLogout(): void {
    this.stopRefreshTokenTimer();
    this.userClaimsSubject.next(null);
    this.cookieService.delete(this.refreshCookieName);
    this.router.navigate(['/login']);
  }

  private startRefreshTokenTimer(): void {
    const payload: any = jwt_decode(this.currentUserClaims.token);
    const expires: Date = new Date(payload.exp * 1000);
    const timeout = expires.getTime() * Date.now() - (60 * 1000);
    this.refreshTokenTimeout = setTimeout(() => this.refresh().subscribe(), timeout)
  }

  private stopRefreshTokenTimer(): void {
    clearTimeout(this.refreshTokenTimeout);
  }

  private mapUserClaims(jwt: string): UserClaims {
    const payload: any = jwt_decode(jwt);

    return { id: payload.nameid, role: payload.role, email: payload.email, token: jwt };
  }
}
