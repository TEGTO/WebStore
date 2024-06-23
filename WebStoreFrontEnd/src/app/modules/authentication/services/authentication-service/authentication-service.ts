import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { UserAuthData } from "../..";
import { UserAuthenticationDto, UserRegistrationDto, UserUpdateDataDto } from "../../../shared";

@Injectable({
    providedIn: 'root'
})
export abstract class AuthenticationService {
    abstract registerUser(userRegistrationData: UserRegistrationDto): Observable<boolean>;
    abstract registerUserGetErrors(): Observable<any>;
    abstract singInUser(userAuthData: UserAuthenticationDto): Observable<UserAuthData>;
    abstract getAuthUserData(): Observable<UserAuthData>;
    abstract logOutUser(): Observable<UserAuthData>;
    abstract updateUser(updateUserData: UserUpdateDataDto): Observable<boolean>;
    abstract getAuthUserErrors(): Observable<any>;
}
