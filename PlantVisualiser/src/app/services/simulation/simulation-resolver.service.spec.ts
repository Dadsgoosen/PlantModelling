import { TestBed } from '@angular/core/testing';

import { SimulationResolverService } from './simulation-resolver.service';

describe('SimulationResolverService', () => {
  let service: SimulationResolverService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SimulationResolverService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
