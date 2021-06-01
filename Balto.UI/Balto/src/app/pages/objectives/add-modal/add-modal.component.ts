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
      notify: new FormControl(false, [ Validators.required ]),
      startingDate: new FormControl(this.todayCustom()),
      endingDate: new FormControl('')
    });
  }

  private setDateForDaily(): boolean {
    if(this.objectiveForm.controls['daily'].value) {
      const date = new Date(Date.now());
      const dateNext = new Date(date.getTime() + 24 * 60 * 60 * 1000);

      this.objectiveForm.controls['notify'].setValue(false);
      this.objectiveForm.controls['startingDate'].setValue({ year: date.getFullYear(), month: date.getMonth(), day: date.getDate() });
      this.objectiveForm.controls['endingDate'].setValue({ year: dateNext.getFullYear(), month: dateNext.getMonth(), day: dateNext.getDate() });  
    } else {
      if(this.objectiveForm.controls['startingDate'].value == '' || this.objectiveForm.controls['startingDate'].value == null) return false;
      if(this.objectiveForm.controls['endingDate'].value == '' || this.objectiveForm.controls['endingDate'].value == null) return false;
      
      if(this.parseDate(this.objectiveForm.controls['startingDate'].value).getTime() > this.parseDate(this.objectiveForm.controls['endingDate'].value).getTime()) return false;
    }
    return true;
  }

  public objectivePostSubmit() {
    if(this.setDateForDaily()) {
      if(this.objectiveForm.valid) {
        this.modal.close({
          name: this.objectiveForm.controls['name'].value,
          description: this.objectiveForm.controls['desc'].value,
          daily: this.objectiveForm.controls['daily'].value,
          notify: this.objectiveForm.controls['notify'].value,
          startingDate: this.parseDate(this.objectiveForm.controls['startingDate'].value),
          endingDate: this.parseDate(this.objectiveForm.controls['endingDate'].value)
        });
      } else {
        this.showNotification = true;
        this.notificationContent = "Objective data invalid. Check if all required fields are filled.";
      }
    } else {
      this.showNotification = true;
      this.notificationContent = "Objective data invalid. Check if all required fields are filled.";
    }

    this.objectiveForm.reset();
  }

  private parseDate(date: any): Date {
    return new Date(`${ date.year }-${ date.month }-${ date.day } ${ '00' }:${ '00' }.${ '000' }`);
  }

  private todayCustom(): any {
    const date = new Date(Date.now());
    return { year: date.getFullYear(), month: date.getMonth(), day: date.getDate() };
  }

}
