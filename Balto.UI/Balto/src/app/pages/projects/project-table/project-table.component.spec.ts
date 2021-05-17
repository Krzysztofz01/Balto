import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectTableComponent } from './project-table.component';

describe('ProjectTableComponent', () => {
  let component: ProjectTableComponent;
  let fixture: ComponentFixture<ProjectTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProjectTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
