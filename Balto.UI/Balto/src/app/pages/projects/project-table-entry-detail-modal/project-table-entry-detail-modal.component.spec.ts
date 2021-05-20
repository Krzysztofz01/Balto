import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectTableEntryDetailModalComponent } from './project-table-entry-detail-modal.component';

describe('ProjectTableEntryDetailModalComponent', () => {
  let component: ProjectTableEntryDetailModalComponent;
  let fixture: ComponentFixture<ProjectTableEntryDetailModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProjectTableEntryDetailModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectTableEntryDetailModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
