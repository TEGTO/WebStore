import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ProductData, UserCartChange } from "../../../shared";

@Injectable({
    providedIn: 'root'
})
export abstract class UserCartService {
    abstract getUserCartProducts(userEmail: string): Observable<ProductData[]>;
    abstract getUserCartProductAmount(userEmail: string): Observable<number>;
    abstract addProductToUserCart(changeCart: UserCartChange): void;
    abstract removeProductFromUserCart(changeCart: UserCartChange): void;
}