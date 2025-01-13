import { Routes } from '@angular/router';
import { AccountListComponent } from './account-list/account-list.component';
import { AccountDetailComponent } from './account-detail/account-detail.component';
import { ExpenseListComponent } from './expense-list/expense-list.component';
import { IncomeListComponent } from './income-list/income-list.component';
import { TransactionListComponent } from './transaction-list/transaction-list.component';
import { LoginComponent } from './login/login.component';
import { FinancesChartsComponent } from './finances-charts/finances-charts.component';
import { CategoryManagementComponent } from './category-management/category-management.component';

export const routes: Routes = [
    {path: 'account-list', component: AccountListComponent},
    {path: 'account/:id', component: AccountDetailComponent},
    {path: 'expense-list', component: ExpenseListComponent},
    {path: 'income-list', component: IncomeListComponent},
    {path: 'transaction-list/:id/:page', component: TransactionListComponent},
    {path: 'login', component: LoginComponent},
    {path: 'charts', component: FinancesChartsComponent},
    { path: 'category-management', component: CategoryManagementComponent },
    // { path: 'login', loadChildren: () => import('').then(m => m.LoginModule) },
    {path: '', redirectTo: 'login', pathMatch: 'full'},
];
