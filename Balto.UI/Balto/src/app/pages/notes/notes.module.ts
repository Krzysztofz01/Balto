import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotesComponent } from './notes.component';
import { NoteComponent } from './note/note.component';
import { QuillModule } from 'ngx-quill';
import { ReactiveFormsModule } from '@angular/forms';
import { AddModalComponent } from './add-modal/add-modal.component';
import { InviteModalComponent } from './invite-modal/invite-modal.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [NotesComponent, NoteComponent, AddModalComponent, InviteModalComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    NgbModule,
    QuillModule
  ]
})
export class NotesModule { }
