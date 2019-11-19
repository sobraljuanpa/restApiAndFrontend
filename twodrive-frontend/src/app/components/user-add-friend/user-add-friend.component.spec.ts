import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserAddFriendComponent } from './user-add-friend.component';

describe('UserAddFriendComponent', () => {
  let component: UserAddFriendComponent;
  let fixture: ComponentFixture<UserAddFriendComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserAddFriendComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserAddFriendComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
