import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { LoginModel } from '../models/login.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  public isAuthenticado: boolean = true;
  constructor(
    private cookie: CookieService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}
  authenticarUsuario(login: LoginModel) {
    const token = 'teste';
    this.cookie.set('auth_token', token);
    this.snackBar.open('Sucesso na autenticação', 'ok', {
      duration: 1000,
    });
    this.router.navigateByUrl('');
  }
  sair() {
    this.cookie.delete('auth_token');
    this.router.navigateByUrl('/Login');
  }
}
