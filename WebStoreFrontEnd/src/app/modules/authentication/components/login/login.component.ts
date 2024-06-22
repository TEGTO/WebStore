import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthenticationDialogManager } from '../../index';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  login: FormGroup = new FormGroup(
    {
      email: new FormControl('', [Validators.email, Validators.required]),
      password: new FormControl('', [Validators.required, Validators.minLength(8)]),
      passwordConfirm: new FormControl('', [Validators.required]),
    });

  hidePassword: boolean = true;
  isInvalidData: boolean = false;

  get emailInput() { return this.login.get('email')!; }
  get passwordInput() { return this.login.get('password')!; }

  constructor(private authDialogManager: AuthenticationDialogManager) { }

  openRegisterMenu() {
    const dialogRef = this.authDialogManager.openRegisterMenu();
  }
}
