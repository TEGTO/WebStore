import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { addProductToUserCart, getUserProductAmount, getUserProducts, removeProductFromUserCart, selectUserProducts, selectUserProductsAmount } from '../..';
import { ProductDataDto } from '../../../shared';
import { UserCartService } from './user-cart-service';

@Injectable({
  providedIn: 'root'
})
export class UserCartControllerService implements UserCartService {

  constructor(private store: Store) { }

  getUserCartProducts(userEmail: string): Observable<ProductDataDto[]> {
    this.store.dispatch(getUserProducts({ userEmail: userEmail }));
    return this.store.select(selectUserProducts);
  }
  getUserCartProductAmount(userEmail: string): Observable<number> {
    this.store.dispatch(getUserProductAmount({ userEmail: userEmail }));
    return this.store.select(selectUserProductsAmount);
  }
  addProductToUserCart(userEmail: string, product: ProductDataDto): void {
    this.store.dispatch(addProductToUserCart({ userEmail: userEmail, product: product }));
  }
  removeProductFromUserCart(userEmail: string, product: ProductDataDto): void {
    this.store.dispatch(removeProductFromUserCart({ userEmail: userEmail, product: product }));
  }
}
