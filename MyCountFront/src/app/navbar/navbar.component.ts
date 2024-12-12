import { Component } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AccountService } from '../services/account.service';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink,RouterLinkActive,NgIf],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})

export class NavbarComponent {
  show : boolean = true;

  constructor(private router: Router,private accountService: AccountService) {
    this.router.events.subscribe(() => {
      // Oculta la navbar si estás en la ruta de login
      this.show = this.router.url !== '/login';
    });
  }
  
  endSession() : void{
    this.show = false;
    this.accountService.logout();
  }
}
