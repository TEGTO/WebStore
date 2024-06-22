import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-authenticated',
  templateUrl: './authenticated.component.html',
  styleUrl: './authenticated.component.scss'
})
export class AuthenticatedComponent {
  register: FormGroup = new FormGroup(
    {
      email: new FormControl('someemail@gmail.com', [Validators.email, Validators.required]),
      oldPassword: new FormControl('', []),
      newPassword: new FormControl('', [Validators.minLength(8)])
    });
  hideNewPassword: boolean = true;

  get emailInput() { return this.register.get('email')!; }
  get oldPassword() { return this.register.get('oldPassword')!; }
  get newPassword() { return this.register.get('newPassword')!; }
  get isInvalidPassword() { return false; }
}
