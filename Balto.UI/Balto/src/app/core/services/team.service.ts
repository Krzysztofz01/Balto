import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Team } from '../models/team.model';

@Injectable({
  providedIn: 'root'
})
export class TeamService {
  private readonly path: string = "team";
  private readonly server: string = environment.SERVER_URL;

  constructor(private httpClient: HttpClient) { }

  private preparePath(apiVersion: number): string {
    return `${ this.server }/api/v${ apiVersion }/${ this.path }`;
  }

  public getAll(apiVersion: number): Observable<Array<Team>> {
    return this.httpClient.get<Array<Team>>(this.preparePath(apiVersion));
  }

  public postOne(team: Team, apiVersion: number): Observable<void> {
    return this.httpClient.post<void>(this.preparePath(apiVersion), team);
  }

  public getOne(teamId: number, apiVersion: number): Observable<Team> {
    return this.httpClient.get<Team>(`${ this.preparePath(apiVersion) }/${ teamId }`);
  }

  public deleteOne(teamId: number, apiVersion: number): Observable<void> {
    return this.httpClient.delete<void>(`${ this.preparePath(apiVersion) }/${ teamId }`);
  }

  public patchOne(teamId: number, team: Team, apiVersion: number): Observable<void> {
    return this.httpClient.patch<void>(`${ this.preparePath(apiVersion) }/${ teamId }`, team);
  }
}
