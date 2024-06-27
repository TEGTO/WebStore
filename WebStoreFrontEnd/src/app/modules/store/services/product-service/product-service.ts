import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ProductDataDto } from "../../../shared";

@Injectable({
    providedIn: 'root'
})
export abstract class ProductService {
    abstract getAllProducts(): Observable<ProductDataDto[]>;
}