import { Component, OnInit } from '@angular/core';
import { UserCartService } from '../..';
import { AuthenticationService } from '../../../authentication';
import { ProductData, RedirectorService, UserAuthData } from '../../../shared';

@Component({
  selector: 'store-cart',
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.scss'
})
export class CartComponent implements OnInit {
  products: ProductData[] = [];
  productTypes: ProductData[][] = [];
  userAuthData!: UserAuthData;

  get totalPrice(): number {
    let total = 0;
    this.products.forEach(product => {
      total += product.price;
    });
    return total;
  }

  constructor(private redirectService: RedirectorService,
    private authService: AuthenticationService,
    private userCartService: UserCartService) { }

  ngOnInit(): void {
    this.authService.getAuthUserData().subscribe(authData => {
      if (!authData.isAuthenticated) {
        this.redirectService.redirectToHome();
      }
      this.userAuthData = authData;
      this.userCartService.getUserCartProducts(authData.userEmail).subscribe(products => {
        this.products = products;
        let types = new Map<number, ProductData[]>();
        products.map(x => x.id).forEach(productId => {
          if (!types.has(productId))
            types.set(productId, products.filter(x => x.id == productId));
        });
        this.productTypes = Array.from(types.values());
      })
    })
  }
}
