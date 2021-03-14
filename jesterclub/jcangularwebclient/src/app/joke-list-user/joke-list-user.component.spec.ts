import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JokeListUserComponent } from './joke-list-user.component';

describe('JokeListUserComponent', () => {
  let component: JokeListUserComponent;
  let fixture: ComponentFixture<JokeListUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JokeListUserComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(JokeListUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
