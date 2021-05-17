import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTableEntryModalComponent } from './add-table-entry-modal.component';

describe('AddTableEntryModalComponent', () => {
  let component: AddTableEntryModalComponent;
  let fixture: ComponentFixture<AddTableEntryModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddTableEntryModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddTableEntryModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
