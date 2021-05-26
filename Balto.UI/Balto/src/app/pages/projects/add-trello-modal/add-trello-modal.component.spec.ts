import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTrelloModalComponent } from './add-trello-modal.component';

describe('AddTrelloModalComponent', () => {
  let component: AddTrelloModalComponent;
  let fixture: ComponentFixture<AddTrelloModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddTrelloModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddTrelloModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
