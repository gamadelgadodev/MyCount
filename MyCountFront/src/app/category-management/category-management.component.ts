import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms'; // Asegúrate de importar FormsModule
import { AccountService } from '../services/account.service'; // Asegúrate de importar el servicio
import { NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-category-management',
  standalone: true,
  imports: [FormsModule,NgIf,NgFor], // Importar FormsModule aquí para usar ngModel
  templateUrl: './category-management.component.html',
  styleUrls: ['./category-management.component.css']
})
export class CategoryManagementComponent {
  selectedCategory: any = { id: 0, name: '', description: '', color: '', typeCat: 'income', isDeleted: false };
  categories: any[] = [];

  constructor(private accountService: AccountService) {
    this.loadCategories();
  }

  // Función para guardar la categoría (crear o actualizar)
  saveCategory() {
    if (this.selectedCategory.id === 0) {
      // Crear nueva categoría
      this.accountService.createCategory(this.selectedCategory).subscribe(() => {
        this.loadCategories();
      });
    } else {
      // Actualizar categoría existente
      this.accountService.editCategory(this.selectedCategory).subscribe(() => {
        this.loadCategories();
      });
    }
  }

   ngOnInit(): void {
    this.accountService.getCategories().subscribe(data => {
      this.categories = data;
      console.log(this.categories);
    });
    }

  // Cargar categorías desde el backend
  loadCategories() {
    this.accountService.getCategories().subscribe(data => {
      this.categories = data;
    });
  }

  // Función para editar una categoría existente
  editCategory(category: any) {
    this.selectedCategory = { ...category }; // Copiar los valores para editarlos
  }

  // Función para cancelar la edición
  cancelEdit() {
    this.selectedCategory = { id: 0, name: '', description: '', color: '', typeCat: 'income', isDeleted: false };
  }
}
