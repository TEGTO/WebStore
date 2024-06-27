import { TestBed } from '@angular/core/testing';

import { UserCartApiService } from './user-cart-api.service';

describe('UserCartApiService', () => {
  let service: UserCartApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserCartApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
