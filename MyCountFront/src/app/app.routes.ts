import { Routes } from '@angular/router';
import { AccountListComponent } from './account-list/account-list.component';
import { AccountDetailComponent } from './account-detail/account-detail.component';
import { ExpenseListComponent } from './expense-list/expense-list.component';
import { IncomeListComponent } from './income-list/income-list.component';

export const routes: Routes = [
    {path: 'account-list', component: AccountListComponent},
    {path: 'account/:id', component: AccountDetailComponent},
    {path: 'expense-list', component: ExpenseListComponent},
    {path: 'income-list', component: IncomeListComponent},
    {path: '', redirectTo: '/account-list', pathMatch: 'full'},
];
