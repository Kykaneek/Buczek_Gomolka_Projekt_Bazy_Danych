import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddtraceComponent } from './addtrace.component';

describe('AddusersComponent', () => {
  let component: AddtraceComponent;
  let fixture: ComponentFixture<AddtraceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddtraceComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(AddtraceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
