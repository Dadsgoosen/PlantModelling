import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PageTitleMenuComponent } from './page-title-menu.component';

describe('PageTitleMenuComponent', () => {
  let component: PageTitleMenuComponent;
  let fixture: ComponentFixture<PageTitleMenuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PageTitleMenuComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PageTitleMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
