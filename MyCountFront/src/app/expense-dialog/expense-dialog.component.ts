import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { CommonModule, NgIf } from '@angular/common';
import { AccountService } from '../services/account.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { IncomeCat } from '../models/income-cat.model';
import { ExpenseCat } from '../models/expense-cat.model';
import {MatCheckboxModule} from '@angular/material/checkbox';

@Component({
  selector: 'app-expense-dialog',
  standalone: true,
  imports: [FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatDialogModule, 
    MatInputModule,
    MatSelectModule,
    MatOptionModule,
    MatButtonModule,
    MatCheckboxModule,
    CommonModule,
    NgIf],
  templateUrl: './expense-dialog.component.html',
  styleUrl: './expense-dialog.component.css'
})
export class ExpenseDialogComponent {
  account: any;
  expenseForm: FormGroup;
  expenseCat: ExpenseCat[] = [];
  expenseD: any;
  edit : boolean = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<ExpenseDialogComponent>,
    private accountService: AccountService,
    @Inject(MAT_DIALOG_DATA) public data: any 
  ) {
    this.account= data.account;
    this.expenseD = data.expenseData;
    if(data.expenseData!==undefined)
      this.edit = true;
    this.accountService.getActiveExpenseCats().subscribe((data: ExpenseCat[]) => {
      this.expenseCat = data;
      console.log('Lista de categories:', this.expenseCat);
    });
    this.expenseForm = this.fb.group({
      id: [data.expenseData?.id || ''],
      accountId: [data.account.id],
      value: [data.expenseData?.value ||  '', [Validators.required, Validators.min(0)]],
      description: [data.expenseData?.description || '', Validators.required],
      nessesary: [data.expenseData?.nessesary || false, Validators.required],
      typeTransaction: ['expense'],
      transactionCatId: [data.expenseData?.transactionCatId || '']
    });
    console.log(this.account)
    console.log(data)
  }
  
  onSubmit(): void {
    if (this.expenseForm.valid && this.data.expenseData?.id==null) {
      this.accountService.addTrans(this.expenseForm.value).subscribe({
        next: () => {
          this.dialogRef.close(true);
        },
        error: (err) => {
          console.error('Error adding expense:', err);
        }
      });
    }if(this.expenseForm.valid && this.data.expenseData?.id!==null)
    {
        this.accountService.editExpense(this.expenseForm.value).subscribe({
          next: () => {
            this.dialogRef.close(true);
          },
          error: (err) =>{
            console.error('Error editing expense:',err);
          }
        });
    }
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }
}
