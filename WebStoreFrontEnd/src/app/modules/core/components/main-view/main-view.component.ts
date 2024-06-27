import { Component, OnInit } from '@angular/core';
import { AuthenticationDialogManager, AuthenticationService } from '../../../authentication';
import { RedirectorService } from '../../../shared';
import { UserCartService } from '../../../store';

@Component({
  selector: 'app-main-view',
  templateUrl: './main-view.component.html',
  styleUrl: './main-view.component.scss'
})
export class MainViewComponent implements OnInit {
  isAuthenticated: boolean = false;
  itemsInCartAmount: number = 0;

  constructor(private authService: AuthenticationService,
    private redirectService: RedirectorService,
    private authDialogManager: AuthenticationDialogManager,
    private userCartService: UserCartService) { }

  redirectToCartPage() {
    this.redirectService.redirectTo("/cart");
  }
  openLoginMenu() {
    const dialogRef = this.authDialogManager.openLoginMenu();
  }

  ngOnInit(): void {
    this.authService.getAuthUserData().subscribe(data => {
      this.isAuthenticated = data.isAuthenticated;
      if (this.isAuthenticated) {
        this.userCartService.getUserCartProductAmount(data.userEmail).subscribe(amount => {
          this.itemsInCartAmount = amount;
        });
      }
    })
  }
}
