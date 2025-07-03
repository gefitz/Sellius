import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, Observable, throwError } from 'rxjs';
import { Cookie } from '../cookie/cookie.service';
import { Injectable } from '@angular/core';
import { ResponseModel } from './model/response.model';
import { error, log } from 'console';
import { response } from 'express';
import { MatSnackBar } from '@angular/material/snack-bar';
import { LoginService } from '../../../pages/login/services/login.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  url: string = 'https://localhost:7147/api';

  constructor(
    private http: HttpClient,
    private cookie: Cookie,
    private snack: MatSnackBar,
    private login: LoginService,
    private route: Router
  ) {}
  get<model>(endPoint: string): Observable<model> {
    return this.http
      .get<ResponseModel<model>>(this.url + endPoint, {
        headers: this.montarHeader(),
      })
      .pipe(
        map((response) => {
          if (response.success) {
            this.snack.open(response.message, 'Ok', { duration: 5000 });
            return response.data;
          } else {
            this.snack.open(response.errorMessage, `Ok`, { duration: 5000 });
            throw new Error('Erro: ' + response.errorMessage);
          }
        }),
        catchError((error) => {
          if (error.status == 401) {
            this.httpResponse401(error);
          }
          // Você pode abrir a modal aqui, ou lançar erro
          // this.modalService.show('Erro ao excluir');
          return throwError(() => error);
        })
      );
  }
  post<model>(endPoint: string, obj: object): Observable<model> {
    return this.http
      .post<ResponseModel<model>>(this.url + endPoint, obj, {
        headers: this.montarHeader(),
      })
      .pipe(
        map((response) => {
          if (response.success) {
            this.snack.open(response.message, `Ok`, { duration: 5000 });
            return response.data;
          } else {
            throw new Error(response.errorMessage);
          }
        }),
        catchError((error) => {
          if (error.status == 401) {
            this.httpResponse401(error);
          }
          this.snack.open(error.error, 'Ok', { duration: 5000 });
          return throwError(() => error.error);
        })
      );
  }

  put<model>(endPoint: string, obj: object): Observable<model> {
    return this.http
      .put<ResponseModel<model>>(this.url + endPoint, obj, {
        headers: this.montarHeader(),
      })
      .pipe(
        map((response) => {
          if (response.success) {
            this.snack.open(response.message, 'Ok', { duration: 5000 });
            return response.data;
          } else {
            throw new Error('Erro ao excluir');
          }
        }),
        catchError((error) => {
          // Você pode abrir a modal aqui, ou lançar erro
          // this.modalService.show('Erro ao excluir');
          if (error.status == 401) {
            this.httpResponse401(error);
          }
          return throwError(() => error);
        })
      );
  }

  delete<model>(endPoint: string): Observable<model> {
    console.log(endPoint);
    return this.http
      .delete<ResponseModel<model>>(this.url + endPoint, {
        headers: this.montarHeader(),
      })
      .pipe(
        map((response) => {
          if (response.success) {
            this.snack.open(response.message, 'Ok', { duration: 5000 });
            return response.data;
          } else {
            throw new Error('Erro ao excluir');
          }
        }),
        catchError((error) => {
          // Você pode abrir a modal aqui, ou lançar erro
          // this.modalService.show('Erro ao excluir');
          if (error.status == 401) {
            this.httpResponse401(error);
          }
          return throwError(() => error);
        })
      );
  }

  private montarHeader(): HttpHeaders {
    const token = this.cookie.resgatarCookie('token');
    console.log(token);
    if (token && token == '') {
      this.login.sair();
    }
    const headers: HttpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
      // Authorization: token,
    });
    return headers;
  }
  private httpResponse401(erro: any) {
    if (erro.error === 'Token is experied.') {
      this.login.sair();
    }
  }
}
