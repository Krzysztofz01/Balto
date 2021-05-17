import { TestBed } from '@angular/core/testing';

import { ProjectSyncService } from './project-sync.service';

describe('ProjectSyncService', () => {
  let service: ProjectSyncService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProjectSyncService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
