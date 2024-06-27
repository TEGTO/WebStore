import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatMenuModule } from '@angular/material/menu';
import { provideEffects } from '@ngrx/effects';
import { provideState, provideStore } from '@ngrx/store';
import { AssortementComponent, CartComponent, CartProductComponent, ProductComponent, ProductControllerService, ProductRatingComponent, ProductService, ProductsEffects, UserCartControllerService, UserCartEffects, UserCartService, productReducer, userCartReducer } from '.';
import { AuthenticationModule } from '../authentication/authentication.module';

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
  ],
  providers: [
    { provide: ProductService, useClass: ProductControllerService },
    { provide: UserCartService, useClass: UserCartControllerService },
    provideStore(),
    provideState({ name: "products", reducer: productReducer }),
    provideState({ name: "usercart", reducer: userCartReducer }),
    provideEffects(ProductsEffects),
    provideEffects(UserCartEffects),
  ]
})
export class StoreModule { }
