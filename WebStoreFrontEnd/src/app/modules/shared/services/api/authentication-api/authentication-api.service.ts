import { Injectable } from '@angular/core';
import { Observable, catchError } from 'rxjs';
import { AuthResponseDto, UserAuthenticationDto, UserRegistrationDto, UserUpdateDataDto } from '../../..';
import { BaseApiService } from '../base-api/base-api.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationApiService extends BaseApiService {

  loginUser(userAuthData: UserAuthenticationDto): Observable<AuthResponseDto> {
    return this.getHttpClient().post<AuthResponseDto>(this.combinePathWithAuthApiUrl(`/login`), userAuthData);
  }
  registerUser(userRegistrationData: UserRegistrationDto) {
    return this.getHttpClient().post(this.combinePathWithAuthApiUrl(`/register`), userRegistrationData).pipe(
      catchError((resp) => this.handleError(resp.error))
    );
  }
  updateUser(updateUserData: UserUpdateDataDto) {
    return this.getHttpClient().put(this.combinePathWithAuthApiUrl(`/update`), updateUserData).pipe(
      catchError((resp) => this.handleError(resp.error))
    );
  }
}