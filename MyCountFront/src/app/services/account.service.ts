import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable } from 'rxjs';
import { Account } from '../models/account.model';
import { IncomeCat } from '../models/income-cat.model';
import { ExpenseCat } from '../models/expense-cat.model';


@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private apiUrl = 'http://localhost:5048/api/';

  constructor(private http: HttpClient) {}

  getActiveAccounts(): Observable<Account[]> {
    return this.http.get<Account[]>(this.apiUrl.concat("Account/Active"));
  }
  getAccountById(id: any): Observable<Account> {
    return this.http.get<Account>(`${this.apiUrl}Account/ById/${id}`);
  }
  addIncome(income: { accountId: number; value: number; description: string; incomeCatId: number }): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}income`, income);
  }
  addExpense(expense: { accountId: number; value: number; description: string; expenseCatId: number;  nessesary: boolean}): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}Expenses`, expense);
  }
  getActiveIncomeCats(): Observable<IncomeCat[]>{
    return this.http.get<IncomeCat[]>(this.apiUrl.concat("IncomeCat/Active"));
  }
  getActiveExpenseCats(): Observable<ExpenseCat[]>{
    return this.http.get<ExpenseCat[]>(this.apiUrl.concat("ExpenseCat/Active"))
  }
}
