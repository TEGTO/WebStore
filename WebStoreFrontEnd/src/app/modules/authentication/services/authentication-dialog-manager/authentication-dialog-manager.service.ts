import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { AuthenticatedComponent, LoginComponent, RegisterComponent } from '../../index';
import { AuthenticationDialogManager } from './authentication-dialog-manager';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationDialogManagerService implements AuthenticationDialogManager {
  isAuthenticated: boolean = true;

  constructor(private dialog: MatDialog) { }

  openLoginMenu(): MatDialogRef<any, any> {
    var dialogRef: MatDialogRef<any, any>;
    if (this.isAuthenticated) {
      dialogRef = this.dialog.open(AuthenticatedComponent, {
        height: '430px',
        width: '450px',
      });
    }
    else {
      dialogRef = this.dialog.open(LoginComponent, {
        height: '345px',
        width: '450px',
      });
    }
    return dialogRef;
  }
  openRegisterMenu(): MatDialogRef<any, any> {
    var component = this.isAuthenticated ? AuthenticatedComponent : RegisterComponent;
    const dialogRef = this.dialog.open(RegisterComponent, {
      height: '430px',
      width: '450px',
    });
    return dialogRef;
  }
}
