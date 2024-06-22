import { Injectable } from '@angular/core';
import { catchError } from 'rxjs';
import { UserRegistrationDto } from '../../../index';
import { BaseApiService } from '../base-api/base-api.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationApiService extends BaseApiService {

  // loginUser(userData: UserRegistrationDto): Observable<AccessToken | undefined> {
  //   return this.getHttpClient().post<AccessToken>(this.combinePathWithAuthApiUrl(`/login`), userData);
  // }
  registerUser(userRegistrationData: UserRegistrationDto) {
    return this.getHttpClient().post(this.combinePathWithAuthApiUrl(`/register`), userRegistrationData).pipe(
      catchError((resp) => this.handleError(resp.error))
    );
  }
}
