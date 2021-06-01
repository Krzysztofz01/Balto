import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectTableTypeComponent } from './project-table-type.component';

describe('ProjectTableTypeComponent', () => {
  let component: ProjectTableTypeComponent;
  let fixture: ComponentFixture<ProjectTableTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProjectTableTypeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectTableTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
