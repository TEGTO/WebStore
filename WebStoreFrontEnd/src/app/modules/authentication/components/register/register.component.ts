import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { confirmPasswordValidator } from '../../index';

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
  hidePasswordConfirm: boolean = true;

  get emailInput() { return this.register.get('email')!; }
  get passwordInput() { return this.register.get('password')!; }
  get passwordConfirmInput() { return this.register.get('passwordConfirm')!; }
}
