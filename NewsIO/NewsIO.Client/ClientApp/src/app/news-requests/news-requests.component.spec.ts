import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewsRequestsComponent } from './news-requests.component';

describe('NewsRequestsComponent', () => {
  let component: NewsRequestsComponent;
  let fixture: ComponentFixture<NewsRequestsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewsRequestsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewsRequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
