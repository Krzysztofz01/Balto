import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Objective } from '../models/objective.model';

@Injectable({
  providedIn: 'root'
})
export class ObjectiveService {
  private readonly path: string = "objective";
  private readonly server: string = environment.SERVER_URL;

  constructor(private httpClient: HttpClient) { }

  private preparePath(apiVersion: number): string {
    return `${ this.server }/api/v${ apiVersion }/${ this.path }`;
  }

  public getAll(apiVersion: number): Observable<Array<Objective>> {
    return this.httpClient.get<Array<Objective>>(this.preparePath(apiVersion));
  }

  public postOne(objective: Objective, apiVersion: number): Observable<void> {
    return this.httpClient.post<void>(this.preparePath(apiVersion), objective);
  }

  public getOne(objectiveId: number, apiVersion: number): Observable<Objective> {
    return this.httpClient.get<Objective>(`${ this.preparePath(apiVersion) }/${ objectiveId }`);
  }

  public deleteOne(objectiveId: number, apiVersion: number): Observable<void> {
    return this.httpClient.delete<void>(`${ this.preparePath(apiVersion) }/${ objectiveId }`);
  }

  public changeState(objectiveId: number, apiVersion: number): Observable<void> {
    return this.httpClient.patch<void>(`${ this.preparePath(apiVersion) }/${ objectiveId }/state`, {});
  }
}
