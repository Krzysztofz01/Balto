import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectTableEntryComponent } from './project-table-entry.component';

describe('ProjectTableEntryComponent', () => {
  let component: ProjectTableEntryComponent;
  let fixture: ComponentFixture<ProjectTableEntryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProjectTableEntryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectTableEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
