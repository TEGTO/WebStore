import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { catchError, map, mergeMap, of } from "rxjs";
import { addProductToUserCart, addProductToUserCartFailure, addProductToUserCartSuccess, getAllProducts, getAllProductsFailure, getAllProductsSuccess, getUserProductAmount, getUserProductAmountFailure, getUserProductAmountSuccess, getUserProducts, getUserProductsFailure, getUserProductsSuccess, removeProductFromUserCart, removeProductFromUserCartFailure, removeProductFromUserCartSuccess } from "..";
import { ProductApiService, UserCartApiService, UserCartChange, UserCartChangeRequest } from "../../shared";

//Products
@Injectable()
export class ProductsEffects {
    constructor(private actions$: Actions,
        private apiService: ProductApiService) { }

    getProducts$ = createEffect(() =>
        this.actions$.pipe(
            ofType(getAllProducts),
            mergeMap((action) =>
                this.apiService.getAllProducts().pipe(
                    map((products) => getAllProductsSuccess({ products: products })),
                    catchError(error => of(getAllProductsFailure({ error: error.message })))
                )
            )
        )
    );
}
//User Cart
@Injectable()
export class UserCartEffects {
    constructor(private actions$: Actions,
        private apiService: UserCartApiService) { }

    getUserProducts$ = createEffect(() =>
        this.actions$.pipe(
            ofType(getUserProducts),
            mergeMap((action) =>
                this.apiService.getProductsInUserCart(action.userEmail).pipe(
                    map((products) => getUserProductsSuccess({ products: products })),
                    catchError(error => of(getUserProductsFailure({ error: error.message })))
                )
            )
        )
    );
    getUserProductAmount$ = createEffect(() =>
        this.actions$.pipe(
            ofType(getUserProductAmount),
            mergeMap((action) =>
                this.apiService.getProductAmountInUserCart(action.userEmail).pipe(
                    map((amount) => getUserProductAmountSuccess({ productAmount: amount })),
                    catchError(error => of(getUserProductAmountFailure({ error: error.message })))
                )
            )
        )
    );
    addProductToUserCart$ = createEffect(() =>
        this.actions$.pipe(
            ofType(addProductToUserCart),
            mergeMap((action) => {
                let changeCart = action.userCartChange;
                let changeCartDto = this.getUserChangeDto(changeCart);
                return this.apiService.addProductToUserCart(changeCartDto).pipe(
                    map(() => addProductToUserCartSuccess({ userCartChange: changeCart })),
                    catchError(error => of(addProductToUserCartFailure({ error: error.message })))
                );
            })
        )
    );
    removeProductFromUserCart$ = createEffect(() =>
        this.actions$.pipe(
            ofType(removeProductFromUserCart),
            mergeMap((action) => {
                let changeCart = action.userCartChange;
                let changeCartDto = this.getUserChangeDto(changeCart);
                return this.apiService.removeProductFromUserCart(changeCartDto).pipe(
                    map(() => removeProductFromUserCartSuccess({ userCartChange: changeCart })),
                    catchError(error => of(removeProductFromUserCartFailure({ error: error.message })))
                );
            })
        )
    );

    private getUserChangeDto(changeCart: UserCartChange) {
        let dto: UserCartChangeRequest =
        {
            userEmail: changeCart.userEmail,
            productId: changeCart.product.id,
            amount: changeCart.amount
        };
        return dto;
    }
}