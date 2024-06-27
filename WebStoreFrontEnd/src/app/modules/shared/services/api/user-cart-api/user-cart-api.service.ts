import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ProductDataDto } from '../../..';
import { BaseApiService } from '../base-api/base-api.service';

@Injectable({
  providedIn: 'root'
})
export class UserCartApiService extends BaseApiService {

  getProductsInUserCart(userEmail: string): Observable<ProductDataDto[]> {
    return this.getHttpClient().get<ProductDataDto[]>(this.combinePathWithWebStoreApiUrl(`/usercart/product/${userEmail}`));
  }
  getProductAmountInUserCart(userEmail: string): Observable<number> {
    return this.getHttpClient().get<number>(this.combinePathWithWebStoreApiUrl(`/usercart/amount/${userEmail}`));
  }
  addProductToUserCart(userEmail: string, product: ProductDataDto) {
    return this.getHttpClient().post(this.combinePathWithWebStoreApiUrl(`/usercart/product/${userEmail}`), product);
  }
  removeProductFromUserCart(userEmail: string, product: ProductDataDto) {
    return this.getHttpClient().delete(this.combinePathWithWebStoreApiUrl(`/usercart/product/${userEmail}?productId=${product.id}`));
  }
}