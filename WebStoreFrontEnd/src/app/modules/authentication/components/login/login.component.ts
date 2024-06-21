import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { RegisterComponent } from '../register/register.component';

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

  constructor(private dialog: MatDialog) { }

  openRegisterMenu() {
    const dialogRef = this.dialog.open(RegisterComponent, {
      height: '430px',
      width: '450px',
    });
  }
}
