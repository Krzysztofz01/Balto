import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

const Priority = [
  { value: 0, name: 'Default' },
  { value: 1, name: 'Important' },
  { value: 2, name: 'Leading'}
];

@Component({
  selector: 'app-add-table-entry-modal',
  templateUrl: './add-table-entry-modal.component.html',
  styleUrls: ['./add-table-entry-modal.component.css']
})
export class AddTableEntryModalComponent implements OnInit {
  public entryForm: FormGroup;
  
  public showNotification: boolean;
  public notificationContent: string;
  public priority = Priority;

  constructor(private modal: NgbActiveModal) { }

  ngOnInit(): void {
    this.showNotification = false;

    this.entryForm = new FormGroup({
      name: new FormControl('', [ Validators.required ]),
      content: new FormControl(''),
      priority: new FormControl(0),
      startingDate: new FormControl('', [ Validators.required ]),
      endingDate: new FormControl('', [ Validators.required ]),
      notify: new FormControl(false)
    });
  }

  private validateDate(): boolean {
    const start = this.parseDate(this.entryForm.controls['startingDate'].value);
    const ending = this.parseDate(this.entryForm.controls['endingDate'].value);

    console.log(start);
    console.log(ending);

    if(ending.getTime() >= start.getTime()) return true;
    return false;
  }

  private parseDate(date: any): Date {
    return new Date(`${ date.year }-${ date.month }-${ date.day } ${ '00' }:${ '00' }.${ '000' }`);
  }

  public entryPostSubmit() {
    if(this.entryForm.valid && this.validateDate()) {
      this.modal.close({
        name: this.entryForm.controls['name'].value,
        content: this.entryForm.controls['content'].value,
        priority: Number(this.entryForm.controls['priority'].value),
        startingDate: this.parseDate(this.entryForm.controls['startingDate'].value),
        endingDate: this.parseDate(this.entryForm.controls['endingDate'].value),
        notify: this.entryForm.controls['notify'].value
      });
    } else {
      this.showNotification = true;
      this.notificationContent = "Entry data invalid. Check if all required fields are filled.";
    }

    this.entryForm.reset();
  }
}
