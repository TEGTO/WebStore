import { ProductData } from "./productDataDto";

export interface UserCartChange {
    userEmail: string;
    product: ProductData;
    amount: number;
}