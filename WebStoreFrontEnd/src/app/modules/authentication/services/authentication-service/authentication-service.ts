import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { UserRegistrationDto } from "../../../shared";

@Injectable({
    providedIn: 'root'
})
export abstract class AuthenticationService {
    abstract registerUser(UserRegistrationData: UserRegistrationDto): Observable<boolean>;
    abstract registerUserGetErrors(): Observable<string[]>;

}
