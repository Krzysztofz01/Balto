import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTableModalComponent } from './add-table-modal.component';

describe('AddTableModalComponent', () => {
  let component: AddTableModalComponent;
  let fixture: ComponentFixture<AddTableModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddTableModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddTableModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
