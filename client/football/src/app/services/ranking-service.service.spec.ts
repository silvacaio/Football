import { TestBed } from '@angular/core/testing';

import { RankingServiceService } from './ranking-service.service';

describe('RankingServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RankingServiceService = TestBed.get(RankingServiceService);
    expect(service).toBeTruthy();
  });
});
