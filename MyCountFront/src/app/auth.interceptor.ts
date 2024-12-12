import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AccountService } from './services/account.service'; // Asegúrate de ajustar la ruta

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AccountService); // Inyecta el servicio manualmente
  const token = authService.getToken();
  console.log(token)

  if (token) {
    const clonedRequest = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
        
      },
    });
    return next(clonedRequest);
  }
  return next(req);
};