import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CopyconfirmComponent } from './copyconfirm.component';

describe('CopyconfirmComponent', () => {
  let component: CopyconfirmComponent;
  let fixture: ComponentFixture<CopyconfirmComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CopyconfirmComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CopyconfirmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
