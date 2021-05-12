import { TestBed } from '@angular/core/testing';

import { LeaderGuard } from './leader.guard';

describe('LeaderGuard', () => {
  let guard: LeaderGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(LeaderGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
