import { Component } from '@angular/core';
import { AuthenticationDialogManager } from '../../../authentication';

@Component({
  selector: 'app-main-view',
  templateUrl: './main-view.component.html',
  styleUrl: './main-view.component.scss'
})
export class MainViewComponent {
  isAuthenticated: boolean = false;
  itemsInCartAmount: number = 0;

  constructor(private authDialogManager: AuthenticationDialogManager) { }

  increaseItemsInCartAmount() {
    this.itemsInCartAmount++;
  }
  openLoginMenu() {
    const dialogRef = this.authDialogManager.openLoginMenu();
  }
}
