import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { UserAuthenticationRequest } from '../../../shared';
import { AuthenticationDialogManager, AuthenticationService, RegisterComponent } from '../../index';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  formGroup: FormGroup = new FormGroup(
    {
      email: new FormControl('', [Validators.email, Validators.required]),
      password: new FormControl('', [Validators.required, Validators.minLength(8)]),
    });
  hidePassword: boolean = true;
  isLoginFailed: boolean = false;

  get emailInput() { return this.formGroup.get('email')!; }
  get passwordInput() { return this.formGroup.get('password')!; }

  constructor(private authDialogManager: AuthenticationDialogManager,
    private authService: AuthenticationService, private dialogRef: MatDialogRef<RegisterComponent>) { }

  openRegisterMenu() {
    const dialogRef = this.authDialogManager.openRegisterMenu();
  }
  signInUser() {
    if (this.formGroup.valid) {
      const formValues = { ...this.formGroup.value };
      const userData: UserAuthenticationRequest = {
        email: formValues.email,
        password: formValues.password,
      };
      this.authService.singInUser(userData).subscribe(authData => {
        if (authData.isAuthenticated) {
          this.dialogRef.close();
        }
        this.isLoginFailed = !authData.isAuthenticated;
      });
    }
  }
}