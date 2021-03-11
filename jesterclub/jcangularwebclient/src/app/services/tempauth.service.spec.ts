import { TestBed } from '@angular/core/testing';

import { TempauthService } from './tempauth.service';

describe('TempauthService', () => {
  let service: TempauthService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TempauthService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
