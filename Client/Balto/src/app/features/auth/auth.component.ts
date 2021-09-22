import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from 'src/app/authentication/services/auth.service';
import { RegistrationModalComponent } from './registration-modal/registration-modal.component';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {
  public loginForm: FormGroup;

  constructor(private authService: AuthService, private router: Router, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl('', [ Validators.required, Validators.email ]),
      password: new FormControl('', [ Validators.required ])
    });
  }

  //Get login credentials and post to auth endpoint via authservice
  public loginSubmit(): void {
    if(this.loginForm.valid) {
      this.authService.login(
        this.loginForm.get('email').value,
        this.loginForm.get('password').value
      ).subscribe(() => {
        this.router.navigate(['']);
      },
      (error) => {
        //TODO: Toast service server error
      });  
    } else {
      //TODO: Toast service validation error
    }

    this.loginForm.reset();
  }

  //Launch registration modal and register if data is valid
  public register(): void {
    this.modalService.open(RegistrationModalComponent, { modalDialogClass: 'modal-lg'}).result.then((credentials) => {
      this.authService.register(credentials.name, credentials.email, credentials.password, credentials.passwordRepeat).subscribe(() => {
        //TODO: Toast service show register info
      },
      (error) => {
        //TODO: Toast service show server error
      });
    }, () => {});
  }
}
