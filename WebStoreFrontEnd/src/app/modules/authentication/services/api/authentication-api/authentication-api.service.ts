import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseApiService } from '../../../../shared';
import { AccessToken, UserData } from '../../../index';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationApiService extends BaseApiService {

  constructor(httpClient: HttpClient, errorHandler: CustomErrorHandler, urlDefiner: URLDefiner,
    private localStorageService: LocalStorageService) {
    super(httpClient, errorHandler, urlDefiner);
  }

  loginUser(userData: UserData): Observable<AccessToken | undefined> {
    return this.getHttpClient().post<AccessToken>(this.combinePathWithAuthApiUrl(`/login`), userData);
  }
  registerUser(userData: UserData) {
    return this.getHttpClient().post<AccessToken>(this.combinePathWithAuthApiUrl(`/register`), userData);
  }
  private getSavedAccessToken(): AccessToken | null {
    const accessToken = this.localStorageService.getItem(this.userConfig.userIdKey);
    if (!userId)
      return null;
    return { id: userId } as User;
  }
}
