import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { RequestRegister } from 'src/app/authentication/models/request-register.model';
import { AuthService } from 'src/app/authentication/services/auth.service';
import { environment } from 'src/environments/environment';
import { RegisterModalComponent } from './register-modal/register-modal.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public loginForm: FormGroup;
  public showNotification: boolean;
  public notificationType: string;
  public notificationContent: string;

  constructor(private authService: AuthService, private router: Router, private modalSerivce: NgbModal) { }

  ngOnInit(): void {
    this.showNotification = false;

    //Init the login form
    this.loginForm = new FormGroup({
      email: new FormControl('', [ Validators.required, Validators.email ]),
      password: new FormControl('', [ Validators.required, Validators.minLength(8) ])
    });
  }

  public loginSubmit(): void {
    if(this.loginForm.valid) {
      this.authService.login({
        email: this.loginForm.controls['email'].value,
        password: this.loginForm.controls['password'].value
      }).subscribe((res) => {
        this.router.navigate(['']);
      },
      (error) => {
        this.generateNotification(error);
      });
    } else {
      this.generateNotification("Input data invalid!");
    }

    this.loginForm.reset();
  }

  public register(): void {
    const ref = this.modalSerivce.open(RegisterModalComponent);

    ref.result.then((result) => {
      if(result != null) {
        const registerData: RequestRegister = result;

        this.authService.register(registerData).subscribe((res) => {
          this.showNotification = true;
          this.notificationContent = "Registration was successful.";
          this.notificationType = 'success';
        },
        (error) => {
          this.generateNotification(error);
        });
      }
    }, () => { 
      this.showNotification = false;
    });
  }

  public displayVersion(): string {
    return `Client: ${ environment.CLIENT_VERSION } Server: ${ environment.SERVER_VERSION }`;
  }

  private generateNotification(content: string): void {
    this.showNotification = true;
    this.notificationContent = content;
    this.notificationType = 'danger';
  }

}
