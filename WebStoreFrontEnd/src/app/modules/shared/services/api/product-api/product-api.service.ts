import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ProductDataDto } from '../../..';
import { BaseApiService } from '../base-api/base-api.service';

@Injectable({
  providedIn: 'root'
})
export class ProductApiService extends BaseApiService {

  getAllProducts(): Observable<ProductDataDto[]> {
    return this.getHttpClient().get<ProductDataDto[]>(this.combinePathWithWebStoreApiUrl("/product"));
  }
}
