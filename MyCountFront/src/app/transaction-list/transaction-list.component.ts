import { CommonModule, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, ParamMap, RouterLink } from '@angular/router';
import { Transaction } from '../models/transaction.model';
import { AccountService } from '../services/account.service';
import { MatDialog } from '@angular/material/dialog';
import { IncomeDialogComponent } from '../income-dialog/income-dialog.component';
import { ExpenseDialogComponent } from '../expense-dialog/expense-dialog.component';

@Component({
  selector: 'app-transaction-list',
  standalone: true,
  imports: [CommonModule,NgIf,RouterLink,NgIf],
  templateUrl: './transaction-list.component.html',
  styleUrl: './transaction-list.component.css'
})
export class TransactionListComponent {
  account: any; 
  transactions: Transaction[] = [];
  page: number=1;
  hasMore: boolean=true;

  constructor(
    private route: ActivatedRoute,
    private accountService: AccountService,
    private dialog: MatDialog 
  ) { }

  ngOnInit(): void {
   
    this.route.paramMap.subscribe((params: ParamMap) => {
      const id = params.get('id');
      this.page = +(params.get('page') ?? 1);
      this.loadAccount(id);
      this.loadTransactions(id, this.page);
      if(this.transactions.length < 3)
        this.hasMore = false;
      console.log(this.transactions);
    });
  }

  loadAccount(id: string | null): void {
    if (id) {
      this.accountService.getAccountById(id).subscribe(
        data => {
          this.account = data;
        },
        error => {
          console.error('Error al obtener la cuenta:', error);
        }
      );
    }
  }

  loadTransactions(accountId: string | null, page: number): void {
    if (accountId) {
      this.accountService.getAllTrans(accountId, page).subscribe(
        data => {
          this.transactions = data;

        },
        error => {
          console.error('Error al cargar las transacciones:', error);
        }
      );
    }
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
