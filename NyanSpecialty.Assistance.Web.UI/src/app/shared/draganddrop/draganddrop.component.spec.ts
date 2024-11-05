import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DraganddropComponent } from './draganddrop.component';

describe('DraganddropComponent', () => {
  let component: DraganddropComponent;
  let fixture: ComponentFixture<DraganddropComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DraganddropComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DraganddropComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
