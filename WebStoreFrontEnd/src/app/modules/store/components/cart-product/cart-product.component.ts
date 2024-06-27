import { Component, Input } from '@angular/core';
import { UserCartService } from '../..';
import { UserAuthData } from '../../../authentication';
import { ProductDataDto } from '../../../shared';

@Component({
  selector: 'store-cart-product',
  templateUrl: './cart-product.component.html',
  styleUrl: './cart-product.component.scss'
})
export class CartProductComponent {
  @Input({ required: true })
  typeProducts!: ProductDataDto[];
  @Input({ required: true })
  userAuthData!: UserAuthData;

  get productType(): ProductDataDto { return this.typeProducts[0]; }
  get finalPrice(): number { return this.productType.price * this.productQuantity; }
  get productQuantity(): number { return this.typeProducts.length; }
  set productQuantity(value: number) {
    while (this.productQuantity != value) {
      if (value > this.productQuantity) {
        this.userCartService.addProductToUserCart(this.userAuthData.userEmail, this.productType);
      }
      else {
        this.userCartService.removeProductFromUserCart(this.userAuthData.userEmail, this.productType);
      }
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
}
