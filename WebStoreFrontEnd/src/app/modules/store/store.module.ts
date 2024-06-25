import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatMenuModule } from '@angular/material/menu';
import { AssortementComponent, CartComponent, ProductComponent, ProductRatingComponent } from '.';
import { AuthenticationModule } from '../authentication/authentication.module';
import { CartProductComponent } from './components/cart-product/cart-product.component';

@NgModule({
  declarations: [
    AssortementComponent,
    ProductComponent,
    ProductRatingComponent,
    CartComponent,
    CartProductComponent
  ],
  imports: [
    CommonModule,
    AuthenticationModule,
    MatMenuModule,
    FormsModule
  ],
  exports: [
    AssortementComponent,
    CartComponent
  ]
})
export class StoreModule { }
