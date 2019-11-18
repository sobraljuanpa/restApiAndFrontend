import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserListTop10Component } from './user-list-top10.component';

describe('UserListTop10Component', () => {
  let component: UserListTop10Component;
  let fixture: ComponentFixture<UserListTop10Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserListTop10Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserListTop10Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
