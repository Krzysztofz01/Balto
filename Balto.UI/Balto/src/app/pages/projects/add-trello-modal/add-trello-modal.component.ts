import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-add-trello-modal',
  templateUrl: './add-trello-modal.component.html',
  styleUrls: ['./add-trello-modal.component.css']
})
export class AddTrelloModalComponent implements OnInit {
  public trelloForm: FormGroup;

  public showNotification: boolean;
  public notificationContent: string;

  constructor(private modal: NgbActiveModal) { }

  ngOnInit(): void {
    this.showNotification = false;

    this.trelloForm = new FormGroup({
      jsonFile: new FormControl('', [ Validators.required ])
    });
  }

  public projectMigrateSubmit() {
    if(this.trelloForm.valid) {
      this.modal.close(this.trelloForm.controls['jsonFile'].value);
    } else {
      this.showNotification = true;
      this.notificationContent = "Project migration data invalid. Check if all required fields are filled.";
    }

    this.trelloForm.reset();
  }

  public changeFile(e: Event): void {
    const file = (e.target as HTMLInputElement).files[0];
    this.trelloForm.controls['jsonFile'].setValue(file);
    this.trelloForm.controls['jsonFile'].updateValueAndValidity();
  }
}
