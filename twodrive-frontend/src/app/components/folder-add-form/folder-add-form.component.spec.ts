import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FolderAddFormComponent } from './folder-add-form.component';

describe('FolderAddFormComponent', () => {
  let component: FolderAddFormComponent;
  let fixture: ComponentFixture<FolderAddFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FolderAddFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FolderAddFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
