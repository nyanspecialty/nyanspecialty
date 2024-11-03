import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddeditpolicytypeComponent } from './addeditpolicytype.component';

describe('AddeditpolicytypeComponent', () => {
  let component: AddeditpolicytypeComponent;
  let fixture: ComponentFixture<AddeditpolicytypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddeditpolicytypeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddeditpolicytypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
