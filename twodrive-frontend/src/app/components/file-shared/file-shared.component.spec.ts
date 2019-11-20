import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FileSharedComponent } from './file-shared.component';

describe('FileSharedComponent', () => {
  let component: FileSharedComponent;
  let fixture: ComponentFixture<FileSharedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FileSharedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FileSharedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
