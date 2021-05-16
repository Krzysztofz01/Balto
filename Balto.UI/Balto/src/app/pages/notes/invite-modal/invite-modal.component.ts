import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-invite-modal',
  templateUrl: './invite-modal.component.html',
  styleUrls: ['./invite-modal.component.css']
})
export class InviteModalComponent implements OnInit {
  public inviteForm: FormGroup;
  
  public showNotification: boolean;
  public notificationContent: string;

  constructor(private modal: NgbActiveModal) { }

  ngOnInit(): void {
    this.showNotification = false;

    this.inviteForm = new FormGroup({
      email: new FormControl('', [ Validators.required, Validators.email ])
    });
  }

  public invitePostSubmit() {
    if(this.inviteForm.valid) {
      this.modal.close(this.inviteForm.controls['email'].value);
    } else {
      this.showNotification = true;
      this.notificationContent = "Invitation data invalid. Check if all required fields are filled.";
    }

    this.inviteForm.reset();
  }

}
