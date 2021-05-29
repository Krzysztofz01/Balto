import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeTeamModalComponent } from './change-team-modal.component';

describe('ChangeTeamModalComponent', () => {
  let component: ChangeTeamModalComponent;
  let fixture: ComponentFixture<ChangeTeamModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChangeTeamModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ChangeTeamModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
