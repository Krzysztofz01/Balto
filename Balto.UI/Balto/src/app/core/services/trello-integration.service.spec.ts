import { TestBed } from '@angular/core/testing';

import { TrelloIntegrationService } from './trello-integration.service';

describe('TrelloIntegrationService', () => {
  let service: TrelloIntegrationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TrelloIntegrationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
