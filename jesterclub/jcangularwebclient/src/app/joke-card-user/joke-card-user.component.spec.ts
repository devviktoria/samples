import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JokeCardUserComponent } from './joke-card-user.component';

describe('JokeCardUserComponent', () => {
  let component: JokeCardUserComponent;
  let fixture: ComponentFixture<JokeCardUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JokeCardUserComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(JokeCardUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
