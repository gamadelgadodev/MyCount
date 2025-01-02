import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FinancesChartsComponent } from './finances-charts.component';

describe('FinancesChartsComponent', () => {
  let component: FinancesChartsComponent;
  let fixture: ComponentFixture<FinancesChartsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FinancesChartsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FinancesChartsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
