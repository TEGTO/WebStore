import { Component, Input } from '@angular/core';
import { ProductDataDto } from '../../../shared';

@Component({
  selector: 'store-cart-product',
  templateUrl: './cart-product.component.html',
  styleUrl: './cart-product.component.scss'
})
export class CartProductComponent {
  @Input({ required: true })
  product!: ProductDataDto;
  productQuantity: number = 1;

  get finalPrice(): number { return this.product.price * this.productQuantity; }

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
