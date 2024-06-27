import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ProductDataDto } from "../../../shared";

@Injectable({
    providedIn: 'root'
})
export abstract class UserCartService {
    abstract getUserCartProducts(userEmail: string): Observable<ProductDataDto[]>;
    abstract getUserCartProductAmount(userEmail: string): Observable<number>;
    abstract addProductToUserCart(userEmail: string, product: ProductDataDto): void;
    abstract removeProductFromUserCart(userEmail: string, product: ProductDataDto): void;
}