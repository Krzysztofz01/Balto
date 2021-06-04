import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectTableTypeEntryComponent } from './project-table-type-entry.component';

describe('ProjectTableTypeEntryComponent', () => {
  let component: ProjectTableTypeEntryComponent;
  let fixture: ComponentFixture<ProjectTableTypeEntryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProjectTableTypeEntryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectTableTypeEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
