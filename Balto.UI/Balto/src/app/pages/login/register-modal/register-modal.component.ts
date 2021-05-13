import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-register-modal',
  templateUrl: './register-modal.component.html',
  styleUrls: ['./register-modal.component.css']
})
export class RegisterModalComponent implements OnInit {
  public showNotification: boolean;
  public notificationContent: string;

  public registerForm: FormGroup;

  constructor(public modal: NgbActiveModal) { }

  ngOnInit(): void {
    this.showNotification = false;

    //Init register form
    this.registerForm = new FormGroup({
      email: new FormControl('', [ Validators.required, Validators.email ]),
      name: new FormControl('', [ Validators.required ]),
      password: new FormControl('', [ Validators.required, Validators.minLength(8) ]),
      repeatPassword: new FormControl('', [ Validators.required, Validators.minLength(8) ])
    });
  }

  private validatePassword(group: FormGroup): boolean {
    const password = group.controls['password'].value;
    const repeatPassword = group.controls['repeatPassword'].value;

    return (password === repeatPassword) ? true : false;
  }

  public registerSubmit() {
    if(this.registerForm.valid && this.validatePassword(this.registerForm)) {
      this.modal.close({
        email: this.registerForm.controls['email'].value,
        name: this.registerForm.controls['name'].value,
        password: this.registerForm.controls['password'].value,
        repeatPassword: this.registerForm.controls['repeatPassword'].value
      });
    } else {
      this.showNotification = true;
      this.notificationContent = "Registration data invalid. Check that the passwords match and that the email address is correct.";
    }

    this.registerForm.reset();
  }

}
