/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { DragNDropComponent } from './drag-n-drop.component';

describe('DragNDropComponent', () => {
  let component: DragNDropComponent;
  let fixture: ComponentFixture<DragNDropComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DragNDropComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DragNDropComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
