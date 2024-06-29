import { Injectable } from '@angular/core';
import { Observable, catchError } from 'rxjs';
import { ProductData } from '../../..';
import { BaseApiService } from '../base-api/base-api.service';

@Injectable({
  providedIn: 'root'
})
export class ProductApiService extends BaseApiService {

  getAllProducts(): Observable<ProductData[]> {
    return this.getHttpClient().get<ProductData[]>(this.combinePathWithWebStoreApiUrl("/product")).pipe(
      catchError((resp) => this.handleError(resp))
    );
  }
}
