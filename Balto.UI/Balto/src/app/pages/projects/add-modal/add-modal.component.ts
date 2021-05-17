import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-add-modal',
  templateUrl: './add-modal.component.html',
  styleUrls: ['./add-modal.component.css']
})
export class AddModalComponent implements OnInit {
  public projectForm: FormGroup;

  public showNotification: boolean;
  public notificationContent: string;

  constructor(private modal: NgbActiveModal) { }

  ngOnInit(): void {
    this.showNotification = false;

    this.projectForm = new FormGroup({
      name: new FormControl('', [ Validators.required ])
    });
  }

  public projectPostSubmit() {
    if(this.projectForm.valid) {
      this.modal.close({
        name: this.projectForm.controls['name'].value
      });
    } else {
      this.showNotification = true;
      this.notificationContent = "Project data invalid. Check if all required fields are filled.";
    }

    this.projectForm.reset();
  }
}
