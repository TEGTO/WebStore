import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { addProductToUserCart, getUserProductAmount, getUserProducts, removeProductFromUserCart, selectUserProducts, selectUserProductsAmount } from '../..';
import { ProductData, UserCartChange } from '../../../shared';
import { UserCartService } from './user-cart-service';

@Injectable({
  providedIn: 'root'
})
export class UserCartControllerService implements UserCartService {

  constructor(private store: Store) { }

  getUserCartProducts(userEmail: string): Observable<ProductData[]> {
    this.store.dispatch(getUserProducts({ userEmail: userEmail }));
    return this.store.select(selectUserProducts);
  }
  getUserCartProductAmount(userEmail: string): Observable<number> {
    this.store.dispatch(getUserProductAmount({ userEmail: userEmail }));
    return this.store.select(selectUserProductsAmount);
  }
  addProductToUserCart(changeCart: UserCartChange): void {
    this.store.dispatch(addProductToUserCart({ userCartChange: changeCart }));
  }
  removeProductFromUserCart(changeCart: UserCartChange): void {
    this.store.dispatch(removeProductFromUserCart({ userCartChange: changeCart }));
  }
}
