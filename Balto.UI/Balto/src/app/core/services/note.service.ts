import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Invitation } from '../models/invitation.model';
import { Note } from '../models/note.model';

@Injectable({
  providedIn: 'root'
})
export class NoteService {
  private readonly path: string = "note";
  private readonly server: string = environment.SERVER_URL;

  constructor(private httpClient: HttpClient) { }

  private preparePath(apiVersion: number): string {
    return `${ this.server }/api/v${ apiVersion }/${ this.path }`;
  }

  public getAll(apiVersion: number): Observable<Array<Note>> {
    return this.httpClient.get<Array<Note>>(this.preparePath(apiVersion));
  }

  public postOne(note: Note, apiVersion: number): Observable<void> {
    return this.httpClient.post<void>(this.preparePath(apiVersion), note);
  }

  public getOne(noteId: number, apiVersion: number): Observable<Note> {
    return this.httpClient.get<Note>(`${ this.preparePath(apiVersion) }/${ noteId }`);
  }

  public deleteOne(noteId: number, apiVersion: number): Observable<void> {
    return this.httpClient.delete<void>(`${ this.preparePath(apiVersion) }/${ noteId }`);
  }

  public patchOne(noteId: number, note: Note, apiVersion: number): Observable<void> {
    return this.httpClient.patch<void>(`${ this.preparePath(apiVersion) }/${ noteId }`, note);
  }

  public inviteUser(noteId: number, invitation: Invitation, apiVersion: number): Observable<void> {
    return this.httpClient.post<void>(`${ this.preparePath(apiVersion) }/${ noteId }/invite`, invitation);
  }
}
