import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from '../services/account.service';
import { Account } from '../models/account.model';
import { MatDialog } from '@angular/material/dialog';
import { IncomeDialogComponent } from '../income-dialog/income-dialog.component';

@Component({
  selector: 'app-account-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './account-detail.component.html',
  styleUrl: './account-detail.component.css'
})
export class AccountDetailComponent {
  account: any; 

  constructor(
    private route: ActivatedRoute,
    private accountService: AccountService,
    private dialog: MatDialog 
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.accountService.getAccountById(id).subscribe(data => {
      this.account = data;
    });
  }

  openIncomeDialog(): void {
    const dialogRef = this.dialog.open(IncomeDialogComponent, {
      data: { accountId: this.account.id } 
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log('Income added');
      }
    });
  }
  
}
