import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JokeUpsertComponent } from './joke-upsert.component';

describe('JokeUpsertComponent', () => {
  let component: JokeUpsertComponent;
  let fixture: ComponentFixture<JokeUpsertComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JokeUpsertComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(JokeUpsertComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
