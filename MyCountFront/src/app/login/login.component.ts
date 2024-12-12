import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../services/account.service';
import { FormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule,NgIf],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  errorMessage: string = '';
  

  constructor(private accountService: AccountService, private router: Router) {}
  
  onSubmit() {
    const credentials = { username: this.email, password: this.password };
    this.accountService.login(credentials).subscribe(
      response => {
        // Guardar el token y redirigir al usuario
        this.accountService.saveToken(response.token);
        
        this.router.navigate(['/account-list']); // Cambiar a la ruta deseada
      },
      error => {
        // Manejar errores de autenticación
        this.errorMessage = 'Invalid username or password';
      }
    );
  }
}
