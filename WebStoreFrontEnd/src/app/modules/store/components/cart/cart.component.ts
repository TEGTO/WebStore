import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../../authentication';
import { ProductDataDto, RedirectorService } from '../../../shared';

@Component({
  selector: 'store-cart',
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.scss'
})
export class CartComponent implements OnInit {
  products: ProductDataDto[] = [];
  numbers: Array<number> = [];

  constructor(private redirectService: RedirectorService, private authService: AuthenticationService) { }

  ngOnInit(): void {
    this.redirectToHomePageIfUserNotAuthenticated();
    this.numbers = Array(10).fill(1);
    this.products.push({
      id: "",
      name: "Wireless Controller Carbon Black (XOA-0005, QAT-00001)",
      price: 200,
      avgRating: 4.5,
      imgUrl: "https://content1.rozetka.com.ua/goods/images/big/261296642.jpg"
    })
  }
  private redirectToHomePageIfUserNotAuthenticated() {
    // this.authService.getAuthUserData().subscribe(authData => {
    //   if (!authData.isAuthenticated) {
    //     this.redirectService.redirectToHome();
    //   }
    // })
  }
}
