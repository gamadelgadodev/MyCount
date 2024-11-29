import { Component ,Inject } from '@angular/core';
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
    CommonModule,
    NgIf],
  templateUrl: './income-dialog.component.html',
  styleUrl: './income-dialog.component.css'
})
export class IncomeDialogComponent {
  account: any;
  incomeForm: FormGroup;
  incomeCat: IncomeCat[] = [];
  incomeD : any;
  edit : boolean = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<IncomeDialogComponent>,
    private accountService: AccountService,
    @Inject(MAT_DIALOG_DATA) public data: any 
  ) {
    this.account= data.account;
    this.incomeD = data.incomeData;
    if(this.incomeD!==null) 
      this.edit = true;

    this.accountService.getActiveIncomeCats().subscribe((data: IncomeCat[]) => {
      this.incomeCat = data;
      console.log('Lista de categories:', this.incomeCat);
    });
    this.incomeForm = this.fb.group({
      id: [data.incomeData?.id || ''],
      accountId: [data.account.id],
      value: [data.incomeData?.value || '', [Validators.required, Validators.min(0)]],
      description: [data.incomeData?.description || '', Validators.required],
      typeTransaction: ['income'],
      transactionCatId: [data.incomeData?.transactionCatId || '']
    });
    console.log(this.account)
    console.log("hello",this.incomeD)
    console.log("data",data)
  }
  
  onSubmit(): void {
    if (this.incomeForm.valid && this.data.incomeData?.id==null) {
      this.accountService.addTrans(this.incomeForm.value).subscribe({
        next: () => {
          this.dialogRef.close(true);
        },
        error: (err) => {
          console.error('Error adding income:', err);
        }
      });
    }else
    {
      this.accountService.editIncome(this.incomeForm.value).subscribe({
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
