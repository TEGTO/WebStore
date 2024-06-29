import { createAction, props } from "@ngrx/store";
import { ProductData, UserCartChange } from "../../shared";

//Products
export const getAllProducts = createAction(
    '[WebStore] Get All Products'
);
export const getAllProductsSuccess = createAction(
    '[WebStore] Get All Products Success',
    props<{ products: ProductData[] }>()
);
export const getAllProductsFailure = createAction(
    '[WebStore] Get All ProductsFailure',
    props<{ error: any }>()
);
//User Cart
export const getUserProducts = createAction(
    '[WebStore] Get User Products In The Cart',
    props<{ userEmail: string }>()
);
export const getUserProductsSuccess = createAction(
    '[WebStore] Get User Products In The Cart Success',
    props<{ products: ProductData[] }>()
);
export const getUserProductsFailure = createAction(
    '[WebStore] Get User Products In The Cart Failure',
    props<{ error: any }>()
);

export const getUserProductAmount = createAction(
    '[WebStore] Get User Product In The Cart Amount',
    props<{ userEmail: string }>()
);
export const getUserProductAmountSuccess = createAction(
    '[WebStore] Get User Product In The Cart Amount Success',
    props<{ productAmount: number }>()
);
export const getUserProductAmountFailure = createAction(
    '[WebStore] Get User Product In The Cart Amount Failure',
    props<{ error: any }>()
);

export const addProductToUserCart = createAction(
    '[WebStore] Add Product To User Cart',
    props<{ userCartChange: UserCartChange }>()
);
export const addProductToUserCartSuccess = createAction(
    '[WebStore] Add Product To User Cart Success',
    props<{ userCartChange: UserCartChange }>()
);
export const addProductToUserCartFailure = createAction(
    '[WebStore] Add Product To User Cart Failure',
    props<{ error: any }>()
);

export const removeProductFromUserCart = createAction(
    '[WebStore] Remove Product From User Cart',
    props<{ userCartChange: UserCartChange }>()
);
export const removeProductFromUserCartSuccess = createAction(
    '[WebStore] Remove Product From User Cart Success',
    props<{ userCartChange: UserCartChange }>()
);
export const removeProductFromUserCartFailure = createAction(
    '[WebStore] Remove Product From User Cart Failure',
    props<{ error: any }>()
);