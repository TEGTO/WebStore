import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ProductData } from "../../../shared";

@Injectable({
    providedIn: 'root'
})
export abstract class ProductService {
    abstract getAllProducts(): Observable<ProductData[]>;
}