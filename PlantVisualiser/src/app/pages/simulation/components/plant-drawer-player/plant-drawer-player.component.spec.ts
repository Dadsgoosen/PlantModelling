import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlantDrawerPlayerComponent } from './plant-drawer-player.component';

describe('PlantDrawerPlayerComponent', () => {
  let component: PlantDrawerPlayerComponent;
  let fixture: ComponentFixture<PlantDrawerPlayerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlantDrawerPlayerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlantDrawerPlayerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
