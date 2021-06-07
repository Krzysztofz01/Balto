import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from 'src/app/authentication/services/auth.service';
import { Note } from 'src/app/core/models/note.model';
import { User } from 'src/app/core/models/user.model';
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
  @Output() deleteEvent = new EventEmitter<Note>();
  
  public note: Note;
  public noteForm: FormGroup;

  //Autosave timeout
  private timeout: any;

  constructor(private authService: AuthService, private modalService: NgbModal, private noteSyncService: NoteSyncService) { }

  ngOnInit(): void {
    this.noteSyncService.note.subscribe(n => {
      //Check if this line doesnt break a null param reference
      if(n != null) {
        this.note = n;
        this.noteForm = new FormGroup({
          name: new FormControl(this.note.name, [ Validators.required ]),
          content: new FormControl(this.note.content, [ Validators.required ])
        });
      }
    });
  }

  public invite(): void {
    const ref = this.modalService.open(InviteModalComponent);

    ref.result.then(result => {
      const email: string = result;
      this.inviteEvent.emit(email);
    },
    () => {});
  }

  public isOwner(): boolean {
    return this.authService.userValue.id == this.note.owner.id;
  }

  public delete(): void {
    this.deleteEvent.emit(this.note);
  }

  public saveRequestTimeout(): void {
    clearTimeout(this.timeout);
    this.timeout = setTimeout(() => {
      if(this.noteForm.valid) {
        this.note.name = this.noteForm.controls['name'].value;
        this.note.content = this.noteForm.controls['content'].value;
  
        this.changesEvent.emit(this.note)
      }
    }, 800);
  }

  public parseUserForTable(user: User): string {
    let name = user.name;
    if(this.note.owner.id == user.id) name = `${ name } (Owner)`;
    if(user.isLeader) name = `${ name } (Leader)`;
    if(user.team != null) {
      return `${ name } - ${ user.team.name }`;
    }
    return `${ name }`;
  }

  public parseUserColor(user: User): string {
    if(user.team != null) {
      if(user.team.color.length) {
        return user.team.color;
      }
    }
    return '#000';
  }
}
