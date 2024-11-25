import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable } from 'rxjs';
import { Account } from '../models/account.model';
import { IncomeCat } from '../models/income-cat.model';
import { ExpenseCat } from '../models/expense-cat.model';
import { Transaction } from '../models/transaction.model';


@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private apiUrl = 'http://localhost:5048/api/';

  constructor(private http: HttpClient) {}

  getActiveAccounts(): Observable<Account[]> {
    return this.http.get<Account[]>(this.apiUrl.concat("Account/Active"));
  }
  getAccountsByUser(id: any): Observable<Account[]> {
    return this.http.get<Account[]>(`${this.apiUrl}Account/UserAcc/${id}`);
  }
  getAccountById(id: any): Observable<Account> {
    return this.http.get<Account>(`${this.apiUrl}Account/ById/${id}`);
  }
  addIncome(income: { accountId: number; value: number; description: string; typeTransaction: string;transactionCatId: number }): Observable<void> {
    return this.http.post<void>(this.apiUrl.concat("Transaction/addIncome"), income);
  }
  editIncome(income: {id:number, accountId: number; value: number; description: string; typeTransaction: string;transactionCatId: number }): Observable<void> {
    return this.http.put<void>(this.apiUrl.concat("Transaction/editTrans"), income);
  }
  addExpense(expense: { accountId: number; value: number; description: string; typeTransaction: string; transactionCatId: number;  nessesary: boolean}): Observable<void> {
    return this.http.post<void>(this.apiUrl.concat("Transaction/addExpense"), expense);
  }
  getActiveIncomeCats(): Observable<IncomeCat[]>{
    return this.http.get<IncomeCat[]>(this.apiUrl.concat("TransactionCat/AllIncoCat"));
  }
  getActiveExpenseCats(): Observable<ExpenseCat[]>{
    return this.http.get<ExpenseCat[]>(this.apiUrl.concat("TransactionCat/AllExpenseCat"))
  }
  getRecTrans(id: any): Observable<Transaction[]>{
    return this.http.get<Transaction[]>(`${this.apiUrl}Transaction/Recent/${id}`);
  }

}
