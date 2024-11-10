import { Component ,Inject } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { AccountService } from '../services/account.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { IncomeCat } from '../models/income-cat.model';

@Component({
  selector: 'app-income-dialog',
  standalone: true,
  imports: [ FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatDialogModule, 
    MatInputModule,
    MatSelectModule,
    MatOptionModule,
    MatButtonModule,
    CommonModule],
  templateUrl: './income-dialog.component.html',
  styleUrl: './income-dialog.component.css'
})
export class IncomeDialogComponent {
  account: any;
  incomeForm: FormGroup;
  incomeCat: IncomeCat[] = [];

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<IncomeDialogComponent>,
    private accountService: AccountService,
    @Inject(MAT_DIALOG_DATA) public data: any 
  ) {
    this.account= data.account;
    this.accountService.getActiveIncomeCats().subscribe((data: IncomeCat[]) => {
      this.incomeCat = data;
      console.log('Lista de categories:', this.incomeCat);
    });
    this.incomeForm = this.fb.group({
      accountId: [data.account.id],
      value: ['', [Validators.required, Validators.min(0)]],
      description: ['', Validators.required],
      typeTransaction: ['expense'],
      transactionCatId: []
    });
    console.log(this.account)
    console.log(data)
  }
  
  onSubmit(): void {
    if (this.incomeForm.valid) {
      this.accountService.addIncome(this.incomeForm.value).subscribe({
        next: () => {
          this.dialogRef.close(true);
        },
        error: (err) => {
          console.error('Error adding income:', err);
        }
      });
    }
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }
}
