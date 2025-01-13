import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { catchError, Observable } from 'rxjs';
import { Account } from '../models/account.model';
import { IncomeCat } from '../models/income-cat.model';
import { ExpenseCat } from '../models/expense-cat.model';
import { Transaction } from '../models/transaction.model';
import { FilterTr } from '../models/filter-tr.model';
import { jwtDecode }  from 'jwt-decode';
import { JwtPayloadModel } from '../models/jwt-payload.model';
import { Category } from '../models/category.model';


@Injectable({
  providedIn: 'root'
})
export class AccountService {
 
  private apiUrl = 'http://localhost:5048/api/';
  private tokenKey = 'authToken';

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
  // addIncome(income: { accountId: number; value: number; description: string; typeTransaction: string;transactionCatId: number }): Observable<void> {
  //   return this.http.post<void>(this.apiUrl.concat("Transaction/addIncome"), income);
  // }
  editIncome(income: {id:number, accountId: number; value: number; description: string; typeTransaction: string;transactionCatId: number }): Observable<void> {
    return this.http.put<void>(this.apiUrl.concat("Transaction/editTrans"), income);
  }
  editExpense(income: {id:number, accountId: number; value: number; description: string; typeTransaction: string;transactionCatId: number; nessesary: boolean }): Observable<void> {
    return this.http.put<void>(this.apiUrl.concat("Transaction/editTrans"), income);
  }
  addTrans(transaction: { accountId: number; value: number; description: string; typeTransaction: string; transactionCatId: number;  nessesary: boolean}): Observable<void> {
    return this.http.post<void>(this.apiUrl.concat("Transaction/addTransacction"), transaction);
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
  getAllTrans(id: any,page: any): Observable<Transaction[]>{
    return this.http.get<Transaction[]>(`${this.apiUrl}Transaction/AllTrans/${id}/${page}`);
  }
  getTransactions(id: any, page: any, filter: FilterTr): Observable<any> {
    // Construimos los parámetros de la consulta

    return this.http.post(`${this.apiUrl}Transaction/filteredTrans/${id}/${page}`, filter);
  }
  login(credentials: { username: string; password: string }) {
    return this.http.post<{ token: string }>(`${this.apiUrl}Auth`, credentials);
  }

  saveToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  getToken(): string | null {
    console.log(this.tokenKey);
    return localStorage.getItem(this.tokenKey);
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
  }
  getUserId(): string | null {
    const decodedToken = this.getDecodedToken();
    return decodedToken ? decodedToken.id : null;
  }
  getChartData(accountId: number, startDate: string, endDate: string): Observable<any> {
    const body = {
      acc: accountId,
      startDate: startDate,
      endDate: endDate
    };
  
    return this.http.post<any>(`${this.apiUrl}Finance/All`, body);
  }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(`${this.apiUrl}TransactionCat/All`);
  }

  // Crear una nueva categoría
  createCategory(category: Category): Observable<Category> {
    return this.http.post<Category>(`${this.apiUrl}TransactionCat`, category);
  }

  // Editar una categoría existente
  editCategory(category: Category): Observable<Category> {
    return this.http.put<Category>(`${this.apiUrl}TransactionCat`, category);
  }

  getDecodedToken(): JwtPayloadModel | null {
    const token = localStorage.getItem(this.tokenKey); // Asegúrate de guardar el token en `localStorage`
    if (!token) return null;

    try {
      return jwtDecode<JwtPayloadModel>(token);
    } catch (error) {
      console.error('Error decoding token', error);
      return null;
    }
  }
}
