import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlantDrawerComponent } from './plant-drawer.component';

describe('PlantDrawerComponent', () => {
  let component: PlantDrawerComponent;
  let fixture: ComponentFixture<PlantDrawerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlantDrawerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlantDrawerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
