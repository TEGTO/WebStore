import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { getAllProducts, selectProducts } from '../..';
import { ProductDataDto } from '../../../shared';
import { ProductService } from './product-service';

@Injectable({
  providedIn: 'root'
})
export class ProductControllerService implements ProductService {

  constructor(private store: Store) { }

  getAllProducts(): Observable<ProductDataDto[]> {
    this.store.dispatch(getAllProducts());
    return this.store.select(selectProducts);
  }
}
