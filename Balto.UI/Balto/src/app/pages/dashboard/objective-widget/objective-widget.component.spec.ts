import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ObjectiveWidgetComponent } from './objective-widget.component';

describe('ObjectiveWidgetComponent', () => {
  let component: ObjectiveWidgetComponent;
  let fixture: ComponentFixture<ObjectiveWidgetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ObjectiveWidgetComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ObjectiveWidgetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
