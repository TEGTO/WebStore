import { TestBed } from '@angular/core/testing';

import { UserCartControllerService } from './user-cart-controller.service';

describe('UserCartControllerService', () => {
  let service: UserCartControllerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserCartControllerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
