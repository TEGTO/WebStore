import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { AuthenticationDialogManager, AuthenticationDialogManagerService, LoginComponent, RegisterComponent } from "./index";
import { AuthenticatedComponent } from './components/authenticated/authenticated.component';

@NgModule({
  declarations: [LoginComponent, RegisterComponent, AuthenticatedComponent],
  imports: [
    CommonModule,
    MatDialogModule,
    MatInputModule,
    FormsModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatButtonModule,
  ],
  providers: [
    { provide: AuthenticationDialogManager, useClass: AuthenticationDialogManagerService },
  ],
  exports: [LoginComponent],
})
export class AuthenticationModule { }
