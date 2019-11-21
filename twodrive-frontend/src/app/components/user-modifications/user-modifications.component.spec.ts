import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserModificationsComponent } from './user-modifications.component';

describe('UserModificationsComponent', () => {
  let component: UserModificationsComponent;
  let fixture: ComponentFixture<UserModificationsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserModificationsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserModificationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
