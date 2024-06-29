import { Component, Input } from '@angular/core';
import { ProductData, UserAuthData, UserCartChange } from '../../../shared';
import { UserCartService } from '../../services/user-cart-service/user-cart-service';

@Component({
  selector: 'store-product',
  templateUrl: './product.component.html',
  styleUrl: './product.component.scss'
})
export class ProductComponent {
  @Input({ required: true })
  product!: ProductData;
  @Input({ required: true })
  userAuthData!: UserAuthData;

  constructor(private userCartService: UserCartService) { }

  addProductToUserCart() {
    if (this.userAuthData.isAuthenticated)
      this.userCartService.addProductToUserCart(this.getCartChange(1));
  }

  private getCartChange(amout: number) {
    let cartChange: UserCartChange =
    {
      userEmail: this.userAuthData.userEmail,
      product: this.product,
      amount: amout
    };
    return cartChange;
  }
}
