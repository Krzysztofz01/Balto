import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Note } from 'src/app/core/models/note.model';
import { NoteService } from 'src/app/core/services/note.service';
import { AddModalComponent } from '../objectives/add-modal/add-modal.component';
import { NoteSyncService } from './note-sync/note-sync.service';

@Component({
  selector: 'app-notes',
  templateUrl: './notes.component.html',
  styleUrls: ['./notes.component.css']
})
export class NotesComponent implements OnInit {
  public notes: Array<Note>;
  public selectedNote: Note;

  constructor(private noteService: NoteService, private modalService: NgbModal, private noteSyncService: NoteSyncService) { }

  ngOnInit(): void {
    this.initializeNotes();

    this.noteSyncService.note.subscribe(n => {
      this.selectedNote = n;
    });
  }

  private initializeNotes(): void {
    this.noteService.getAll(1).subscribe((res) => {
      this.notes = res;
    },
    (error) => {
      console.error(error);
    });
  }

  public selectNote($event): void {
    const noteId: number = $event;
    const note = this.selectedNote = this.notes.find(n => n.id == noteId);
    this.noteSyncService.change(note);

    //Save before change
  }

  public addNew(): void {
    const ref = this.modalService.open(AddModalComponent);

    ref.result.then((result) => {
      const note: Note = result;
      this.noteService.postOne(note, 1).subscribe((res) => {
        this.initializeNotes();
      },
      (error) => {
        console.error(error);
      })
    }, () => {});
  }

  public changes($event): void {
    const note: Note = $event;
    this.noteService.patchOne(note.id, note, 1).subscribe((res) => {
      this.initializeNotes();
    },
    (error) => {
      console.error(error);
    });
  }

  public invite($event): void {
    const email: string = $event;
    this.noteService.inviteUser(this.selectedNote.id, { email }, 1).subscribe((res) => {
      this.initializeNotes();
    }, 
    (error) => {
      console.error(error);
    });
  }

}
