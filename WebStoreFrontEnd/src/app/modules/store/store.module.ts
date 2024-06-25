import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AssortementComponent } from '.';
import { ProductComponent } from './components/product/product.component';
import { ProductRatingComponent } from './components/product-rating/product-rating.component';

@NgModule({
  declarations: [
    AssortementComponent,
    ProductComponent,
    ProductRatingComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    AssortementComponent
  ]
})
export class StoreModule { }
