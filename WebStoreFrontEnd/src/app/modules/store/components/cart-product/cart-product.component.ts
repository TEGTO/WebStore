import { Component, Input } from '@angular/core';
import { UserCartService } from '../..';
import { ProductData, UserAuthData, UserCartChange } from '../../../shared';

@Component({
  selector: 'store-cart-product',
  templateUrl: './cart-product.component.html',
  styleUrl: './cart-product.component.scss'
})
export class CartProductComponent {
  @Input({ required: true })
  typeProducts!: ProductData[];
  @Input({ required: true })
  userAuthData!: UserAuthData;

  get productType(): ProductData { return this.typeProducts[0]; }
  get finalPrice(): number { return this.productType.price * this.productQuantity; }
  get productQuantity(): number { return this.typeProducts.length; }
  set productQuantity(value: number) {
    let difference = value - this.productQuantity;
    let changeCart: UserCartChange =
      { userEmail: this.userAuthData.userEmail, product: this.productType, amount: Math.abs(difference) };
    if (difference > 0) {
      this.userCartService.addProductToUserCart(changeCart)
    }
    else {
      this.userCartService.removeProductFromUserCart(changeCart);
    }
  }

  constructor(private userCartService: UserCartService) { }

  increaseQuantity() {
    this.productQuantity++;
  }
  decreaseQuantity() {
    if (this.productQuantity > 1)
      this.productQuantity--;
  }
  numberOnly(event: any): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }
  deleteAll() {
    this.productQuantity = 0;
  }
}
