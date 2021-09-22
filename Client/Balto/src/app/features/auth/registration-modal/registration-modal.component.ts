import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-registration-modal',
  templateUrl: './registration-modal.component.html',
  styleUrls: ['./registration-modal.component.css']
})
export class RegistrationModalComponent implements OnInit {
  public registerForm: FormGroup;

  constructor(private modal: NgbActiveModal) { }

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      name: new FormControl('', [ Validators.required ]),
      email: new FormControl('', [ Validators.required, Validators.email ]),
      password: new FormControl('', [ Validators.required ]),
      passwordRepeat: new FormControl('', [ Validators.required ])
    });
  }

  public register(): void {
    if(this.registerForm.valid && this.arePasswordsEqual()) {
      this.modal.close({
        name: this.registerForm.get('name').value,
        email: this.registerForm.get('email').value,
        password: this.registerForm.get('password').value,
        passwordRepeat: this.registerForm.get('passwordRepeat').value
      });
    } else {
      //TODO: Classic notification invalid
    }

    this.registerForm.reset();
  }

  private arePasswordsEqual(): boolean {
    return this.registerForm.get('password').value == this.registerForm.get('passwordRepeat').value;
  }
}
