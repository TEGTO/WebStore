import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { LoginComponent } from '../../../authentication';

@Component({
  selector: 'app-main-view',
  templateUrl: './main-view.component.html',
  styleUrl: './main-view.component.scss'
})
export class MainViewComponent {
  isAuthenticated: boolean = false;
  itemsInCartAmount: number = 0;

  constructor(private dialog: MatDialog) { }

  increaseItemsInCartAmount() {
    this.itemsInCartAmount++;
  }
  openLoginMenu() {
    const dialogRef = this.dialog.open(LoginComponent, {
      height: '345px',
      width: '450px',
    });
  }
}
