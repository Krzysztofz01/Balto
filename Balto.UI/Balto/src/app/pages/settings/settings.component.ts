import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/authentication/services/auth.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {
  public passwordResetForm: FormGroup;
  public showPassNotification: boolean;
  public notificationPassContent: string;
  public notificationPassStatus: string;

  public serverVersion: string;
  public appVersion: string;
  public agent: string;
  public platform: string;
  public cookiesEnabled: boolean;

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.serverVersion = environment.SERVER_VERSION;
    this.appVersion = environment.CLIENT_VERSION;
    this.agent = navigator.userAgent;
    this.platform = navigator.platform;
    this.cookiesEnabled = navigator.cookieEnabled;

    this.showPassNotification = false;

    this.passwordResetForm = new FormGroup({
      password: new FormControl('', [ Validators.required ]),
      repeatPassword: new FormControl('', [ Validators.required ])
    });
  }

  private checkPasswords(): boolean {
    return this.passwordResetForm.controls['password'].value == this.passwordResetForm.controls['repeatPassword'].value;
  }

  public changePassword(): void {
    if(this.passwordResetForm.valid && this.checkPasswords()) {
      this.authService.resetPassword({
        password: this.passwordResetForm.controls['password'].value,
        repeatPassword: this.passwordResetForm.controls['repeatPassword'].value
      }).subscribe((res) => {
        this.showPassNotification = true;
        this.notificationPassContent = 'Password has been changed.';
        this.notificationPassStatus = 'success';
      },
      (error) => {
        this.showPassNotification = true;
        this.notificationPassContent = error;
        this.notificationPassStatus = 'danger';
      });
    } else {
      this.showPassNotification = true;
      this.notificationPassContent = 'Check the correctness of the entered data';
      this.notificationPassStatus = 'danger';
    }

    this.passwordResetForm.reset();
  }

}
