import { CommonModule, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { AccountService } from '../services/account.service';
import { Account } from '../models/account.model';
import { MatDialog } from '@angular/material/dialog';
import { IncomeDialogComponent } from '../income-dialog/income-dialog.component';
import { ExpenseDialogComponent } from '../expense-dialog/expense-dialog.component';
import { Transaction } from '../models/transaction.model';

@Component({
  selector: 'app-account-detail',
  standalone: true,
  imports: [CommonModule,NgIf,RouterLink],
  templateUrl: './account-detail.component.html',
  styleUrl: './account-detail.component.css'
})
export class AccountDetailComponent {
  account: any; 
  transactions: Transaction[] = [];

  constructor(
    private route: ActivatedRoute,
    private accountService: AccountService,
    private dialog: MatDialog 
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.accountService.getAccountById(id).subscribe(
      data => {
        this.account = data;
      },
      error => {
        console.error('Error al obtener la cuenta:', error);
      }
    );
    this.accountService.getRecTrans(id).subscribe(
      (data: Transaction[]) => {
        this.transactions = data;
        console.log('Lista de cuentas:', this.transactions);
      },
      error => {
        console.error('Error al obtener las cuentas activas:', error);
      }
    );
  }

  openIncomeDialog(incomeData?: any): void {
    const dialogRef = this.dialog.open(IncomeDialogComponent, {
      data: { account: this.account, incomeData: incomeData,  } 
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        window.location.reload()
        console.log('Income added');
      }
    });
    
  }

  openExpenseDialog(expenseData?: any): void {
    const dialogRef = this.dialog.open(ExpenseDialogComponent, {
      data: { account: this.account , expenseData: expenseData} 
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        window.location.reload()
        console.log('Expense added');
      }
    });
    
  }
  
  
}
