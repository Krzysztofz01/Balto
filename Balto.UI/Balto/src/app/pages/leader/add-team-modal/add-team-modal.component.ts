import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-add-team-modal',
  templateUrl: './add-team-modal.component.html',
  styleUrls: ['./add-team-modal.component.css']
})
export class AddTeamModalComponent implements OnInit {
  public teamForm: FormGroup;

  public showNotification: boolean;
  public notificationContent: string;

  constructor(private modal: NgbActiveModal) { }

  ngOnInit(): void {
    this.showNotification = false;

    this.teamForm = new FormGroup({
      name: new FormControl('', [ Validators.required ]),
      color: new FormControl()
    });
  }

  public teamPostSubmit() {
    if(this.teamForm.valid) {
      this.modal.close({
        name: this.teamForm.controls['name'].value,
        color: this.teamForm.controls['color'].value
      });
    } else {
      this.showNotification = true;
      this.notificationContent = "Team data invalid. Check if all required fields are filled.";
    }

    this.teamForm.reset();
  }
}
