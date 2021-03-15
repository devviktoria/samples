import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JokeDeleteConfirmationComponent } from './joke-delete-confirmation.component';

describe('JokeDeleteConfirmationComponent', () => {
  let component: JokeDeleteConfirmationComponent;
  let fixture: ComponentFixture<JokeDeleteConfirmationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JokeDeleteConfirmationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(JokeDeleteConfirmationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
