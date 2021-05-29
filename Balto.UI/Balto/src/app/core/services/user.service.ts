import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly path: string = "user";
  private readonly server: string = environment.SERVER_URL;

  constructor(private httpClient: HttpClient) { }

  private preparePath(apiVersion: number): string {
    return `${ this.server }/api/v${ apiVersion }/${ this.path }`;
  }

  public getAll(apiVersion: number): Observable<Array<User>> {
    return this.httpClient.get<Array<User>>(this.preparePath(apiVersion));
  }

  public getOne(userId: number, apiVersion: number): Observable<User> {
    return this.httpClient.get<User>(`${ this.preparePath(apiVersion) }/${ userId }`);
  }

  public deleteOne(userId: number, apiVersion: number): Observable<void> {
    return this.httpClient.delete<void>(`${ this.preparePath(apiVersion) }/${ userId }`);
  }

  public changeActivation(userId: number, apiVersion: number): Observable<void> {
    return this.httpClient.patch<void>(`${ this.preparePath(apiVersion) }/${ userId }/activation`, {});
  }

  public setTeam(userId: number, teamId: number, apiVersion: number): Observable<void> {
    return this.httpClient.patch<void>(`${ this.preparePath(apiVersion) }/${ userId }/team/${ teamId }`, {});
  }
}
