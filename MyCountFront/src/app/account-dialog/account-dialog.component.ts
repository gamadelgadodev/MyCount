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
  templateUrl: './account-dialog.component.html',
  styleUrl: './account-dialog.component.css'
})
export class AccountDialogComponent {
  account: any;
    accountForm: FormGroup;
    expenseCat: ExpenseCat[] = [];
    accountD: any;
    edit : boolean = false;
  
    constructor(
      private fb: FormBuilder,
      private dialogRef: MatDialogRef<AccountDialogComponent>,
      private accountService: AccountService,
      @Inject(MAT_DIALOG_DATA) public data: any 
    ) {
      this.accountForm = this.fb.group({
        UserId: [data.account.id],
        Name: [data.expenseData?.value ||  '', [Validators.required, Validators.min(0)]],
        description: [data.expenseData?.description || '', Validators.required],
        nessesary: [data.expenseData?.nessesary || false, Validators.required],
        typeTransaction: ['expense'],
        transactionCatId: [data.expenseData?.transactionCatId || '']
      });
      console.log(this.account)
      console.log(data)
    }
    
    onSubmit(): void {
      if (this.accountForm.valid && this.data.expenseData?.id==null) {
        this.accountService.addTrans(this.accountForm.value).subscribe({
          next: () => {
            this.dialogRef.close(true);
          },
          error: (err) => {
            console.error('Error adding expense:', err);
          }
        });
      }if(this.accountForm.valid && this.data.expenseData?.id!==null)
      {
          this.accountService.editExpense(this.accountForm.value).subscribe({
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
