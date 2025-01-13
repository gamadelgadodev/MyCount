import { Component, OnInit } from '@angular/core';
import Chart from 'chart.js/auto';
import { AccountService } from '../services/account.service';
import { Account } from '../models/account.model';
import { CommonModule, NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-finances-charts',
  standalone: true,
  imports: [NgFor,FormsModule,CommonModule],
  templateUrl: './finances-charts.component.html',
  styleUrl: './finances-charts.component.css'
})
export class FinancesChartsComponent  implements OnInit {
  accounts: Account[] = [];
  selectedAccountId: number | null = null;
  startDate: string = '';
  endDate: string = '';
  chartData: any;
accountForm: any;
  constructor(private accountService: AccountService ) {
    
  }

  public chart: any;
  createChart(){
    if (this.chart) {
      this.chart.destroy();
    }
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
    const userId = this.accountService.getUserId();
    this.accountService.getAccountsByUser(userId).subscribe((data: Account[]) => { //hadrcodeed user id 
      this.accounts = data;
      console.log('Lista de cuentas:', this.accounts,userId);
    });
  }
  onSubmit(): void {
    if (this.selectedAccountId && this.startDate && this.endDate) {
      this.accountService.getChartData(this.selectedAccountId, this.startDate, this.endDate).subscribe(
        (data) => {
          this.chartData = data;
          console.log('Datos del gráfico:', this.chartData);
          this.createChart();
        },
        (error) => {
          console.error('Error al cargar los datos del gráfico:', error);
        }
      );
    } else {
      console.error('Faltan campos por completar.');
    }
  }
  onAccountChange(event: any) {
    this.selectedAccountId = +event.target.value; // Convierte el valor a número
    console.log('Cuenta seleccionada:', this.selectedAccountId);
  }
}
