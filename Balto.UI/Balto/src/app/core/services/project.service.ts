import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Invitation } from '../models/invitation.model';
import { ProjectTableEntry } from '../models/project-table-entry.model';
import { ProjectTable } from '../models/project-table.model';
import { Project } from '../models/project.model';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  private readonly path: string = "project";
  private readonly server: string = environment.SERVER_URL; 

  constructor(private httpClient: HttpClient) { }

  //Project related

  private preparePath(apiVersion: number): string {
    return `${ this.server }/api/v${ apiVersion }/${ this.path }`;
  }

  public getAllProjects(apiVersion: number): Observable<Array<Project>> {
    return this.httpClient.get<Array<Project>>(this.preparePath(apiVersion));
  }

  public postOneProject(project: Project, apiVersion: number): Observable<void> {
    return this.httpClient.post<void>(this.preparePath(apiVersion), project);
  }

  public getOneProject(projectId: number, apiVersion: number): Observable<Project> {
    return this.httpClient.get<Project>(`${ this.preparePath(apiVersion) }/${ projectId }`);
  }

  public deleteOneProject(projectId: number, apiVersion: number): Observable<void> {
    return this.httpClient.delete<void>(`${ this.preparePath(apiVersion) }/${ projectId }`);
  }

  public patchOneProject(projectId: number, project: Project, apiVersion: number): Observable<void> {
    return this.httpClient.patch<void>(`${ this.preparePath(apiVersion) }/${ projectId }`, project);
  }

  public inviteUser(projectId: number, invitation: Invitation, apiVersion: number): Observable<void> {
    return this.httpClient.post<void>(`${ this.preparePath(apiVersion) }/${ projectId }/invite`, invitation);
  }

  //Project table related

  private preparePathTable(projectId: number, apiVersion: number): string {
    return `${ this.server }/api/v${ apiVersion }/${ this.path }/${ projectId }/table`;
  }

  public getAllProjectTables(projectId: number, apiVersion: number): Observable<Array<ProjectTable>> {
    return this.httpClient.get<Array<ProjectTable>>(this.preparePathTable(projectId, apiVersion));
  }

  public postOneProjectTable(projectId: number, projectTable: ProjectTable, apiVersion: number): Observable<void> {
    return this.httpClient.post<void>(this.preparePathTable(projectId, apiVersion), projectTable);
  }

  public getOneProjectTable(projectId: number, projectTableId: number, apiVersion: number): Observable<ProjectTable> {
    return this.httpClient.get<ProjectTable>(`${ this.preparePathTable(projectId, apiVersion) }/${ projectTableId }`);
  }

  public deleteOneProjectTable(projectId: number, projectTableId: number, apiVersion: number): Observable<void> {
    return this.httpClient.delete<void>(`${ this.preparePathTable(projectId, apiVersion) }/${ projectTableId }`);
  }

  public patchOneProjectTable(projectId: number,  projectTableId: number, projectTable: ProjectTable, apiVersion: number): Observable<void> {
    return this.httpClient.patch<void>(`${ this.preparePathTable(projectId, apiVersion) }/${ projectTableId }`, projectTable);
  }

  public patchOneProjectTableOrder(projectId: number,  projectTableId: number, order: Array<number>, apiVersion: number): Observable<void> {
    return this.httpClient.patch<void>(`${ this.preparePathTable(projectId, apiVersion) }/${ projectTableId }/order`, order);
  }

  //Project table entry related

  private preparePathTableEntry(projectId: number, projectTableId: number, apiVersion: number): string {
    return `${ this.server }/api/v${ apiVersion }/${ this.path }/${ projectId }/table/${ projectTableId }/entry`;
  }

  public getAllProjectTableEntries(projectId: number, projectTableId: number, apiVersion: number): Observable<Array<ProjectTableEntry>> {
    return this.httpClient.get<Array<ProjectTableEntry>>(this.preparePathTableEntry(projectId, projectTableId, apiVersion));
  }

  public postOneProjectTableEntry(projectId: number, projectTableId: number, projectTableEntry: ProjectTableEntry, apiVersion: number): Observable<void> {
    return this.httpClient.post<void>(this.preparePathTableEntry(projectId, projectTableId, apiVersion), projectTableEntry);
  }

  public getOneProjectTableEntry(projectId: number, projectTableId: number, projectTableEntryId: number, apiVersion: number): Observable<ProjectTableEntry> {
    return this.httpClient.get<ProjectTableEntry>(`${ this.preparePathTableEntry(projectId, projectTableId, apiVersion) }/${ projectTableEntryId }`);
  }

  public deleteOneProjectTableEntry(projectId: number, projectTableId: number, projectTableEntryId: number, apiVersion: number): Observable<void> {
    return this.httpClient.delete<void>(`${ this.preparePathTableEntry(projectId, projectTableId, apiVersion) }/${ projectTableEntryId }`);
  }

  public patchOneProjectTableEntry(projectId: number,  projectTableId: number, projectTableEntryId: number,  projectTableEntry: ProjectTableEntry, apiVersion: number): Observable<void> {
    return this.httpClient.patch<void>(`${ this.preparePathTableEntry(projectId, projectTableId, apiVersion) }/${ projectTableEntryId }`, projectTableEntry);
  }

  public changeState(projectId: number, projectTableId: number, projectTableEntryId: number, apiVersion: number): Observable<void> {
    return this.httpClient.patch<void>(`${ this.preparePathTableEntry(projectId, projectTableId, apiVersion) }/${ projectTableEntryId }/state`, {})
  }
}
