import { Component, Input } from '@angular/core';
import { UserAuthData } from '../../../authentication';
import { ProductDataDto } from '../../../shared';
import { UserCartService } from '../../services/user-cart-service/user-cart-service';

@Component({
  selector: 'store-product',
  templateUrl: './product.component.html',
  styleUrl: './product.component.scss'
})
export class ProductComponent {
  @Input({ required: true })
  product!: ProductDataDto;
  @Input({ required: true })
  userAuthData!: UserAuthData;

  constructor(private userCartService: UserCartService) { }

  addProductToUserCart() {
    if (this.userAuthData.isAuthenticated)
      this.userCartService.addProductToUserCart(this.userAuthData.userEmail, this.product);
  }
}
