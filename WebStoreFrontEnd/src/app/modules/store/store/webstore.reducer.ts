import { createReducer, on } from "@ngrx/store";
import { addProductToUserCartFailure, addProductToUserCartSuccess, getAllProductsFailure, getAllProductsSuccess, getUserProductAmountFailure, getUserProductAmountSuccess, getUserProductsFailure, getUserProductsSuccess, removeProductFromUserCartFailure, removeProductFromUserCartSuccess } from "..";
import { ProductDataDto } from "../../shared";

//Products
export interface ProductState {
    products: ProductDataDto[],
    error: any
}
const initialProductState: ProductState = {
    products: [],
    error: null
};
export const productReducer = createReducer(
    initialProductState,
    on(getAllProductsSuccess, (state, { products: products }) => ({
        ...state,
        products: products,
        error: null
    })),
    on(getAllProductsFailure, (state, { error: error }) => ({
        ...state,
        products: [],
        error: error
    })),
);
//User Cart
export interface UserCartState {
    productsInCart: ProductDataDto[],
    productsInCartAmount: number,
    error: any
}
const initialUserCartState: UserCartState = {
    productsInCart: [],
    productsInCartAmount: 0,
    error: null
};
export const userCartReducer = createReducer(
    initialUserCartState,
    on(getUserProductsSuccess, (state, { products: products }) => ({
        ...state,
        productsInCart: products,
        productsInCartAmount: products.length,
        error: null
    })),
    on(getUserProductsFailure, (state, { error: error }) => ({
        ...state,
        productsInCart: [],
        productsInCartAmount: 0,
        error: error
    })),
    on(getUserProductAmountSuccess, (state, { productAmount: amount }) => ({
        ...state,
        productsInCartAmount: amount,
        error: null
    })),
    on(getUserProductAmountFailure, (state, { error: error }) => ({
        ...state,
        productsInCart: [],
        productsInCartAmount: 0,
        error: error
    })),
    on(addProductToUserCartSuccess, (state, { product: product }) => ({
        ...state,
        productsInCart: [...state.productsInCart, product],
        productsInCartAmount: state.productsInCartAmount + 1,
        error: null
    })),
    on(addProductToUserCartFailure, (state, { error: error }) => ({
        ...state,
        error: error
    })),
    on(removeProductFromUserCartSuccess, (state, { product: product }) => {
        const indexToRemove = state.productsInCart.findIndex(p => p.id === product.id);
        if (indexToRemove !== -1) {
            return {
                ...state,
                productsInCart: [
                    ...state.productsInCart.slice(0, indexToRemove),
                    ...state.productsInCart.slice(indexToRemove + 1)
                ],
                productsInCartAmount: state.productsInCartAmount > 0 ? state.productsInCartAmount - 1 : 0,
                error: null
            };
        }
        return state;
    }),
    on(removeProductFromUserCartFailure, (state, { error: error }) => ({
        ...state,
        error: error
    })),
);
