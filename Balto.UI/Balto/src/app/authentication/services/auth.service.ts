import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AuthUser } from '../models/auth-user-model';
import { RequestLogin } from '../models/request-login.model';
import jwt_decode  from 'jwt-decode';
import { ResponseLogin } from '../models/response-login.model';
import { RequestRegister } from '../models/request-register.model';
import { RequestReset } from '../models/request-reset.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private userSubject: BehaviorSubject<AuthUser>;
  public user: Observable<AuthUser>;

  private refreshTokenTimeout: any;

  private readonly path: string = "auth";
  private readonly server: string = environment.SERVER_URL;
  private readonly authVersion: number = 1;

  constructor(private router: Router, private httpClient: HttpClient) {
    this.userSubject = new BehaviorSubject<AuthUser>(null);
    this.user = this.userSubject.asObservable();
  }

  private preparePath(): string {
    return `${ this.server }/api/v${ this.authVersion }/${ this.path }`;
  }

  public get userValue(): AuthUser {
    return this.userSubject.value;
  }

  public login(form: RequestLogin): Observable<ResponseLogin> {
    return this.httpClient.post<ResponseLogin>(this.preparePath(), form, { withCredentials: true })
      .pipe(map(res => {
        const authUser = this.mapAuthUser(res.bearerToken);
        
        this.userSubject.next(authUser);
        this.startRefreshTokenTimer();
        return res;
      }));
  }

  public logout(): void {
    this.httpClient.post<void>(`${ this.preparePath() }/revoke`, {}, { withCredentials: true }).subscribe();
    this.stopRefreshTokenTimer();
    this.userSubject.next(null);
    this.router.navigate(['/login']);
  }

  public refreshToken(): Observable<any> {
    return this.httpClient.post<any>(`${ this.preparePath() }/refresh`, {}, { withCredentials: true })
      .pipe(map(res => {
        const authUser = this.mapAuthUser(res.bearerToken);
        
        this.userSubject.next(authUser);
        this.startRefreshTokenTimer();
        return res;
      }));
  }

  public register(form: RequestRegister): Observable<void> {
    return this.httpClient.post<void>(`${ this.preparePath() }/register`, form, { withCredentials: true });
  }

  public resetPassword(form: RequestReset): Observable<void> {
    return this.httpClient.post<void>(`${ this.preparePath() }/reset`, form, { withCredentials: true });
  }

  private startRefreshTokenTimer(): void {
    const payload: any = jwt_decode(this.userValue.jwt);
    const expires: Date = new Date(payload.exp * 1000);
    const timeout = expires.getTime() * Date.now() - (60 * 1000);
    this.refreshTokenTimeout = setTimeout(() => this.refreshToken().subscribe(), timeout);
  }

  private stopRefreshTokenTimer(): void {
    clearTimeout(this.refreshTokenTimeout);
  }

  private mapAuthUser(jwt: string): AuthUser {
    const payload: any = jwt_decode(jwt);
    return { 
      jwt: jwt,
      name: payload.given_name,
      id: payload.nameid,
      role: payload.role,
      email: payload.email 
    };
  }
}
