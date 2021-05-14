import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Note } from 'src/app/core/models/note.model';
import { InviteModalComponent } from '../invite-modal/invite-modal.component';
import { NoteSyncService } from '../note-sync/note-sync.service';

@Component({
  selector: 'app-note',
  templateUrl: './note.component.html',
  styleUrls: ['./note.component.css']
})
export class NoteComponent implements OnInit {
  @Output() changesEvent = new EventEmitter<Note>();
  @Output() inviteEvent = new EventEmitter<string>();
  
  public note: Note;
  public noteForm: FormGroup;

  constructor(private modalService: NgbModal, private noteSyncService: NoteSyncService) { }

  ngOnInit(): void {
    this.noteSyncService.note.subscribe(n => {
      this.note = n;
      this.noteForm = new FormGroup({
        name: new FormControl(this.note.name, [ Validators.required ]),
        content: new FormControl(this.note.content, [ Validators.required ])
      });
    });
  }

  public save(): void {
    if(this.noteForm.valid) {
      this.note.name = this.noteForm.controls['name'].value;
      this.note.content = this.noteForm.controls['content'].value;

      this.changesEvent.emit(this.note)
    }
  }

  public invite(): void {
    const ref = this.modalService.open(InviteModalComponent);

    ref.result.then(result => {
      const email: string = result;
      this.inviteEvent.emit(email);
    },
    () => {});
  }

}
