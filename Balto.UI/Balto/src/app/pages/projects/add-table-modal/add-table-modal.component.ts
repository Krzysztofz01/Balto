import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-add-table-modal',
  templateUrl: './add-table-modal.component.html',
  styleUrls: ['./add-table-modal.component.css']
})
export class AddTableModalComponent implements OnInit {
  public projectTableForm: FormGroup;
  
  public showNotification: boolean;
  public notificationContent: string;

  constructor(private modal: NgbActiveModal) { }

  ngOnInit(): void {
    this.showNotification = false;

    this.projectTableForm = new FormGroup({
      name: new FormControl('', [ Validators.required ])
    });
  }

  public projectTablePostSubmit() {
    if(this.projectTableForm.valid) {
      this.modal.close({
        name: this.projectTableForm.controls['name'].value
      });
    } else {
      this.showNotification = true;
      this.notificationContent = "Project table data invalid. Check if all required fields are filled.";
    }

    this.projectTableForm.reset();
  }

}
