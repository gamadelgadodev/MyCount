import { Component, OnInit } from '@angular/core';
import { AccountService, Account } from '../account.service';

@Component({
  selector: 'app-account-list',
  templateUrl: './account-list.component.html',
  styleUrls: ['./account-list.component.css']
})
export class AccountListComponent implements OnInit {
  accounts: Account[] = [];

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {
    this.accountService.getActiveAccounts().subscribe((data: Account[]) => {
      this.accounts = data;
    });
  }
}
