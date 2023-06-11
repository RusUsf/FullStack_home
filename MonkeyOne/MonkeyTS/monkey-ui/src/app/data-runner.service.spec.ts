import { TestBed } from '@angular/core/testing';

import { DataRunnerService } from './data-runner.service';

describe('DataRunnerService', () => {
  let service: DataRunnerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DataRunnerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
