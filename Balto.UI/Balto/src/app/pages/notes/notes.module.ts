import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotesComponent } from './notes.component';
import { NoteComponent } from './note/note.component';
import { QuillModule } from 'ngx-quill';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InviteModalComponent } from './invite-modal/invite-modal.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AddModalComponent } from './add-modal/add-modal.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [NotesComponent, NoteComponent, InviteModalComponent, AddModalComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    NgbModule,
    QuillModule,
    SharedModule
  ]
})
export class NotesModule { }
