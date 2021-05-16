import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-add-modal',
  templateUrl: './add-modal.component.html',
  styleUrls: ['./add-modal.component.css']
})
export class AddModalComponent implements OnInit {
  public noteForm: FormGroup;
  
  public showNotification: boolean;
  public notificationContent: string;

  constructor(private modal: NgbActiveModal) { }

  ngOnInit(): void {
    this.showNotification = false;

    this.noteForm = new FormGroup({
      name: new FormControl('', [ Validators.required ])
    });
  }

  public notePostSubmit() {
    if(this.noteForm.valid) {
      this.modal.close({
        name: this.noteForm.controls['name'].value,
        content: '{}'
      });
    } else {
      this.showNotification = true;
      this.notificationContent = "Note data invalid. Check if all required fields are filled.";
    }

    this.noteForm.reset();
  }

}
