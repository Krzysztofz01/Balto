import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { User } from 'src/app/core/models/user.model';
import { UserService } from 'src/app/core/services/user.service';

@Component({
  selector: 'app-invite-modal',
  templateUrl: './invite-modal.component.html',
  styleUrls: ['./invite-modal.component.css']
})
export class InviteModalComponent implements OnInit {
  public inviteForm: FormGroup;
  
  public selectedUser: User;
  public users: Array<User>; 
  
  public showNotification: boolean;
  public notificationContent: string;

  constructor(private userService: UserService, private modal: NgbActiveModal) { }

  ngOnInit(): void {
    this.showNotification = false;
    
    this.users = new Array<User>();
    this.userService.getAll(1).subscribe((res) => {
      this.users = res;
    });

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

  public selectUser(): void {
    if(this.selectedUser != null) {
      this.inviteForm.controls['email'].setValue(this.selectedUser.email);
    }
  }

}
