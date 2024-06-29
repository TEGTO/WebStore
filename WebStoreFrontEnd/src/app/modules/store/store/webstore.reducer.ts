import { createReducer, on } from "@ngrx/store";
import { addProductToUserCartFailure, addProductToUserCartSuccess, getAllProductsFailure, getAllProductsSuccess, getUserProductAmountFailure, getUserProductAmountSuccess, getUserProductsFailure, getUserProductsSuccess, removeProductFromUserCartFailure, removeProductFromUserCartSuccess } from "..";
import { ProductData } from "../../shared";

//Products
export interface ProductState {
    products: ProductData[],
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
    productsInCart: ProductData[],
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
    on(addProductToUserCartSuccess, (state, { userCartChange: cartChange }) => {
        const newProducts = Array(cartChange.amount).fill(cartChange.product);
        return {
            ...state,
            productsInCart: [...state.productsInCart, ...newProducts],
            productsInCartAmount: state.productsInCartAmount + cartChange.amount,
            error: null
        };
    }),
    on(addProductToUserCartFailure, (state, { error: error }) => ({
        ...state,
        error: error
    })),
    on(removeProductFromUserCartSuccess, (state, { userCartChange: cartChange }) => {
        let remainingProductsInCart = [...state.productsInCart];
        let remainingAmount = cartChange.amount;
        for (let i = 0; i < remainingProductsInCart.length && remainingAmount > 0; i++) {
            if (remainingProductsInCart[i].id === cartChange.product.id) {
                remainingProductsInCart.splice(i, 1);
                i--;
                remainingAmount--;
            }
        }
        return {
            ...state,
            productsInCart: remainingProductsInCart,
            productsInCartAmount: Math.max(state.productsInCartAmount - cartChange.amount, 0),
            error: null
        };
    }),
    on(removeProductFromUserCartFailure, (state, { error: error }) => ({
        ...state,
        error: error
    })),
);
