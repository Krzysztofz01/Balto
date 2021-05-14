import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Note } from 'src/app/core/models/note.model';

@Injectable({
  providedIn: 'root'
})
export class NoteSyncService {
  private noteSubject = new BehaviorSubject<Note>(null);
  public note = this.noteSubject.asObservable();

  constructor() { }

  public change(note: Note): void {
    this.noteSubject.next(note);
  }
}
