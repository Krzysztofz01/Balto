import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { Note } from 'src/app/core/models/note.model';
import { NoteService } from 'src/app/core/services/note.service';
import { AddModalComponent } from './add-modal/add-modal.component';
import { NoteSyncService } from './note-sync/note-sync.service';

@Component({
  selector: 'app-notes',
  templateUrl: './notes.component.html',
  styleUrls: ['./notes.component.css']
})
export class NotesComponent implements OnInit, OnDestroy {
  public selectId: number;
  public notes: Array<Note>;
  public selectedNote: Note;

  private syncSubscription: Subscription;

  constructor(private noteService: NoteService, private modalService: NgbModal, private noteSyncService: NoteSyncService) { }

  ngOnInit(): void {
    this.initializeNotes();

    this.syncSubscription = this.noteSyncService.note.subscribe(n => {
      this.selectedNote = n;
    });
  }

  ngOnDestroy(): void {
    this.noteSyncService.change(null);
    this.syncSubscription.unsubscribe();
  }

  private initializeNotes(afterAdd: boolean = false): void {
    this.notes = new Array<Note>();
    this.noteService.getAll(1).subscribe((res) => {
      this.notes = res;

      //If the initialization is called after adding a new note the last note is ,,selected''
      if(afterAdd) {
        this.noteSyncService.change(this.notes[this.notes.length - 1]);
      }
    },
    (error) => {
      console.error(error);
    });
  }

  public selectNote(): void {
    if(this.selectId != null) {
      const note = this.notes.find(n => n.id == this.selectId);
      this.noteSyncService.change(note);
    }
  }

  public addNew(): void {
    const ref = this.modalService.open(AddModalComponent);

    ref.result.then((result) => {
      const note: Note = result;
      this.noteService.postOne(note, 1).subscribe((res) => {
        this.initializeNotes(true);
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

  public delete($event): void {
    const note: Note = $event;
    this.noteService.deleteOne(note.id, 1).subscribe((res) => {
      this.initializeNotes();
      this.noteSyncService.change(null);
    },
    (error) => {
      console.error(error);
    });
  }

}