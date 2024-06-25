import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssortementComponent } from './assortement.component';

describe('AssortementComponent', () => {
  let component: AssortementComponent;
  let fixture: ComponentFixture<AssortementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AssortementComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AssortementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
