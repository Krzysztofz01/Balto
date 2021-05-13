import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-add-modal',
  templateUrl: './add-modal.component.html',
  styleUrls: ['./add-modal.component.css']
})
export class AddModalComponent implements OnInit {
  public objectiveForm: FormGroup;
  
  public showNotification: boolean;
  public notificationContent: string;

  constructor(private modal: NgbActiveModal) { }

  ngOnInit(): void {
    this.showNotification = false;

    this.objectiveForm = new FormGroup({
      name: new FormControl('', [ Validators.required ]),
      desc: new FormControl(''),
      daily: new FormControl(false, [ Validators.required ]),
      startingDate: new FormControl(),
      endingDate: new FormControl(),
    }, );
  }

  private setDateForDaily(form: FormGroup): boolean {
    if(form.controls['daily'].value) {
      const date = new Date(Date.now());

      this.objectiveForm.controls['startingDate'].setValue(date.toISOString());
      this.objectiveForm.controls['endingDate'].setValue(date.toISOString());  
    } else {
      if(this.objectiveForm.controls['startingDate'].value == '' || this.objectiveForm.controls['startingDate'].value == null) return false;
      if(this.objectiveForm.controls['endingDate'].value == '' || this.objectiveForm.controls['endingDate'].value == null) return false;
      
      if(this.parseDate(this.objectiveForm.controls['startingDate'].value).getTime() > this.parseDate(this.objectiveForm.controls['endingDate'].value).getTime()) return false;
    }
    return true;
  }

  public objectivePostSubmit() {
    if(this.objectiveForm.valid && this.setDateForDaily(this.objectiveForm)) {
      this.modal.close({
        name: this.objectiveForm.controls['name'].value,
        description: this.objectiveForm.controls['desc'].value,
        daily: this.objectiveForm.controls['daily'].value,
        startingDate: this.parseDate(this.objectiveForm.controls['startingDate'].value),
        endingDate: this.parseDate(this.objectiveForm.controls['endingDate'].value)
      });
    } else {
      this.showNotification = true;
      this.notificationContent = "Objective data invalid. Check if all required fields are filled.";
    }

    this.objectiveForm.reset();
  }

  private parseDate(date: any): Date {
    return new Date(`${ date.year }-${ date.month }-${ date.day } ${ '00' }:${ '00' }.${ '000' }`);
  }

}
