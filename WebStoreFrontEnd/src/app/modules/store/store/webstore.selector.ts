import { createFeatureSelector, createSelector } from "@ngrx/store";
import { ProductState, UserCartState } from "..";

//Products
export const selectProductState = createFeatureSelector<ProductState>('products');
export const selectProducts = createSelector(
    selectProductState,
    (state: ProductState) => state.products
);
export const selectProductsErrors = createSelector(
    selectProductState,
    (state: ProductState) => state.error
);
//User Cart
export const selectUserCart = createFeatureSelector<UserCartState>('usercart');
export const selectUserProducts = createSelector(
    selectUserCart,
    (state: UserCartState) => state.productsInCart
);
export const selectUserProductsAmount = createSelector(
    selectUserCart,
    (state: UserCartState) => state.productsInCartAmount
);
export const selectUserCartErrors = createSelector(
    selectUserCart,
    (state: UserCartState) => state.error
);
