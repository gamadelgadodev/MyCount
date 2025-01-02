import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { Account } from '../models/account.model';
import { CommonModule, NgIf } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { Router, RouterLink } from '@angular/router';
import { IncomeDialogComponent } from '../income-dialog/income-dialog.component';
import { AccountDialogComponent } from '../account-dialog/account-dialog.component'; 

@Component({
  standalone: true,
  imports: [CommonModule,NgIf,RouterLink],
  selector: 'app-account-list',
  templateUrl: './account-list.component.html',
  styleUrls: ['./account-list.component.css'],
})

export class AccountListComponent implements OnInit {
  accounts: Account[] = [];

  constructor(private accountService: AccountService,private dialog: MatDialog ) {
    
  }

  ngOnInit(): void {
    const userId = this.accountService.getUserId();
    this.accountService.getAccountsByUser(userId).subscribe((data: Account[]) => { //hadrcodeed user id 
      this.accounts = data;
      console.log('Lista de cuentas:', this.accounts,userId);
    });
  }
   openAccountDialog(incomeData?: any): void {
      const dialogRef = this.dialog.open(AccountDialogComponent, {
        data: { } 
      });
  
      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          window.location.reload()
          console.log('Income added');
        }
      });
      
    }
}
