import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { registerUser, selectIsRegistrationSuccess, selectRegistrationErrors } from '../..';
import { UserRegistrationDto } from '../../../shared';
import { AuthenticationService } from './authentication-service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationControllerService implements AuthenticationService {

  constructor(private store: Store) { }

  registerUser(userRegistrationData: UserRegistrationDto): Observable<boolean> {
    this.store.dispatch(registerUser({ userRegistrationData: userRegistrationData }));
    return this.store.select(selectIsRegistrationSuccess);
  }
  registerUserGetErrors(): Observable<string[]> {
    return this.store.select(selectRegistrationErrors);
  }
}
