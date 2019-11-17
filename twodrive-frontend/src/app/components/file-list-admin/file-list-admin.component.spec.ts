import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FileListAdminComponent } from './file-list-admin.component';

describe('FileListAdminComponent', () => {
  let component: FileListAdminComponent;
  let fixture: ComponentFixture<FileListAdminComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FileListAdminComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FileListAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
