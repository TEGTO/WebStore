import { ProductDataDto } from "./dto/productDataDto";

export interface UserCartChange {
    userEmail: string;
    product: ProductDataDto;
    amount: number;
}