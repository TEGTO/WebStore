import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { AuthenticationService, confirmPasswordValidator } from '../..';
import { UserRegistrationDto } from '../../../shared';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  register: FormGroup = new FormGroup(
    {
      email: new FormControl('', [Validators.email, Validators.required]),
      password: new FormControl('', [Validators.required, Validators.minLength(8)]),
      passwordConfirm: new FormControl('', [Validators.required, confirmPasswordValidator])
    });
  hidePassword: boolean = true;
  registerErrors: any;

  get emailInput() { return this.register.get('email')!; }
  get passwordInput() { return this.register.get('password')!; }
  get passwordConfirmInput() { return this.register.get('passwordConfirm')!; }

  constructor(private authService: AuthenticationService, private dialogRef: MatDialogRef<RegisterComponent>) { }

  registerUser() {
    if (this.register.valid) {
      const formValues = { ...this.register.value };
      const user: UserRegistrationDto = {
        email: formValues.email,
        password: formValues.password,
        confirmPassword: formValues.passwordConfirm
      };
      this.authService.registerUser(user).subscribe(isSuccess => {
        if (isSuccess) {
          this.dialogRef.close();
        }
        else {
          this.authService.registerUserGetErrors().subscribe(errors => {
            this.registerErrors = errors;
          })
        }
        this.authService.registerUserGetErrors().subscribe(errors => {
          this.registerErrors = errors;
        })
      });
    }
  }
}
