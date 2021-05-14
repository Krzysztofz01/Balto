import { TestBed } from '@angular/core/testing';

import { NoteSyncService } from './note-sync.service';

describe('NoteSyncService', () => {
  let service: NoteSyncService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NoteSyncService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
