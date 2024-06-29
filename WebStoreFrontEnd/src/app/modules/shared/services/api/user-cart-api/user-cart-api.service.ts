import { Injectable } from '@angular/core';
import { Observable, catchError } from 'rxjs';
import { ProductData, UserCartChangeRequest } from '../../..';
import { BaseApiService } from '../base-api/base-api.service';

@Injectable({
  providedIn: 'root'
})
export class UserCartApiService extends BaseApiService {

  getProductsInUserCart(userEmail: string): Observable<ProductData[]> {
    return this.getHttpClient().get<ProductData[]>(this.combinePathWithWebStoreApiUrl(`/usercart/product/${userEmail}`)).pipe(
      catchError((resp) => this.handleError(resp))
    );
  }
  getProductAmountInUserCart(userEmail: string): Observable<number> {
    return this.getHttpClient().get<number>(this.combinePathWithWebStoreApiUrl(`/usercart/amount/${userEmail}`)).pipe(
      catchError((resp) => this.handleError(resp))
    );
  }
  addProductToUserCart(userCartChangeDto: UserCartChangeRequest) {
    return this.getHttpClient().post(this.combinePathWithWebStoreApiUrl(`/usercart`), userCartChangeDto).pipe(
      catchError((resp) => this.handleError(resp))
    );
  }
  removeProductFromUserCart(userCartChangeDto: UserCartChangeRequest): Observable<any> {
    return this.getHttpClient().put(this.combinePathWithWebStoreApiUrl(`/usercart`), userCartChangeDto).pipe(
      catchError((resp) => this.handleError(resp))
    );
  }
}