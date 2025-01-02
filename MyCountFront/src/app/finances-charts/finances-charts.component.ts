import { Component, OnInit } from '@angular/core';
import Chart from 'chart.js/auto';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-finances-charts',
  standalone: true,
  imports: [],
  templateUrl: './finances-charts.component.html',
  styleUrl: './finances-charts.component.css'
})
export class FinancesChartsComponent  implements OnInit {

  chartData: any;
  constructor(private accountService: AccountService ) {
    
  }

  public chart: any;
  createChart(){

    this.chart = new Chart("MyChart", {
    type: 'doughnut', //this denotes tha type of chart

    data: {
      labels: this.chartData.labels,
      datasets: [{
        label: this.chartData.datasets[0].label,
        data: this.chartData.datasets[0].data,
        backgroundColor: this.chartData.datasets[0].backgroundColor,
        hoverOffset: this.chartData.datasets[0].hoverOffset
      }],
      },
      options: {
        aspectRatio:3.5
      }

    });
  }
  ngOnInit(): void {
    const accountId = 1;
    const startDate = '2024-01-01';
    const endDate = '2024-12-31';

    this.accountService.getChartData(accountId, startDate, endDate).subscribe(data => {
      this.chartData = data;
      this.createChart();
    });
  }
}
