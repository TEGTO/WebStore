import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ProductDataDto, UserCartChange } from "../../../shared";

@Injectable({
    providedIn: 'root'
})
export abstract class UserCartService {
    abstract getUserCartProducts(userEmail: string): Observable<ProductDataDto[]>;
    abstract getUserCartProductAmount(userEmail: string): Observable<number>;
    abstract addProductToUserCart(changeCart: UserCartChange): void;
    abstract removeProductFromUserCart(changeCart: UserCartChange): void;
}