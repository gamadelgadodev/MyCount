import { Component ,Inject } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { AccountService } from '../services/account.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-income-dialog',
  standalone: true,
  imports: [ FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatDialogModule, 
    MatInputModule,
    MatButtonModule,],
  templateUrl: './income-dialog.component.html',
  styleUrl: './income-dialog.component.css'
})
export class IncomeDialogComponent {
  incomeForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<IncomeDialogComponent>,
    private accountService: AccountService,
    @Inject(MAT_DIALOG_DATA) public data: any 
  ) {
    this.incomeForm = this.fb.group({
      accountId: [data.accountId],
      value: ['', [Validators.required, Validators.min(0)]],
      description: ['', Validators.required],
      incomeCatId: ['', Validators.required]
    });
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
