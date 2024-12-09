import { CommonModule, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, ParamMap, RouterLink } from '@angular/router';
import { Transaction } from '../models/transaction.model';
import { AccountService } from '../services/account.service';
import { MatDialog } from '@angular/material/dialog';
import { IncomeDialogComponent } from '../income-dialog/income-dialog.component';
import { ExpenseDialogComponent } from '../expense-dialog/expense-dialog.component';
import { FilterTr } from '../models/filter-tr.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-transaction-list',
  standalone: true,
  imports: [CommonModule,NgIf,RouterLink,NgIf,FormsModule],
  templateUrl: './transaction-list.component.html',
  styleUrl: './transaction-list.component.css'
})
export class TransactionListComponent {
  account: any; 
  transactions: Transaction[] = [];
  filter: FilterTr = {};
  page: number=1;
  hasMore: boolean=true;
  firstPage: boolean=true;

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
      console.log(this.transactions);
    });
  }
  onReset(): void {
    this.filter = {}; 
    window.location.reload()
  }
  search(): void
  {
    this.page = 1;
    this.route.paramMap.subscribe((params: ParamMap) => {
      const id = params.get('id');
      this.page = +(params.get('page') ?? 1);
      this.loadAccount(id);
      this.loadTransactions(id, this.page);
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
      this.accountService.getTransactions(accountId, page,this.filter).subscribe(
        data => {
          this.transactions = data;
          console.log("Loaded:",data,this.transactions.length,this.page)
          if(this.transactions.length < 5 )
          {
            this.hasMore = false;
          }
          else
            this.hasMore = true;
          if(this.page >1)
          {
            this.firstPage=false;console.log("hello");
          }
          else
            this.firstPage = true;
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
