import { TestBed } from '@angular/core/testing';

import { BearerTokenInterceptor } from './bearer-token.interceptor';

describe('BearerTokenInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      BearerTokenInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: BearerTokenInterceptor = TestBed.inject(BearerTokenInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
