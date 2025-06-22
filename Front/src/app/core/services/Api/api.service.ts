import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, Observable, throwError } from 'rxjs';
import { Cookie } from '../cookie/cookie.service';
import { Injectable } from '@angular/core';
import { ResponseModel } from './model/response.model';
import { error } from 'console';
import { response } from 'express';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  url: string = 'https://localhost:7147/api';

  constructor(private http: HttpClient, private cookie: Cookie) {}
  get<model>(endPoint: string): Observable<model> {
    return this.http
      .get<ResponseModel<model>>(this.url + endPoint, {
        headers: this.montarHeader(),
      })
      .pipe(
        map((response) => {
          if (response.success) {
            return response.data;
          } else {
            throw new Error('Erro ao excluir');
          }
        }),
        catchError((error) => {
          // Você pode abrir a modal aqui, ou lançar erro
          // this.modalService.show('Erro ao excluir');
          return throwError(() => error);
        })
      );
  }
  post<model>(endPoint: string, obj: object): Observable<ResponseModel<model>> {
    return this.http
      .post<ResponseModel<model>>(this.url + endPoint, obj, {
        headers: this.montarHeader(),
      })
      .pipe(
        map((response) => {
          return response;
        }),
        catchError((error) => {
          // Você pode abrir a modal aqui, ou lançar erro
          // this.modalService.show('Erro ao excluir');
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
            return response.data;
          } else {
            throw new Error('Erro ao excluir');
          }
        }),
        catchError((error) => {
          // Você pode abrir a modal aqui, ou lançar erro
          // this.modalService.show('Erro ao excluir');
          return throwError(() => error);
        })
      );
  }

  delete<model>(endPoint: string): Observable<model> {
    return this.http
      .delete<ResponseModel<model>>(this.url + endPoint, {
        headers: this.montarHeader(),
      })
      .pipe(
        map((response) => {
          if (response.success) {
            return response.data;
          } else {
            throw new Error('Erro ao excluir');
          }
        }),
        catchError((error) => {
          // Você pode abrir a modal aqui, ou lançar erro
          // this.modalService.show('Erro ao excluir');
          return throwError(() => error);
        })
      );
  }

  private montarHeader(): HttpHeaders {
    const token = this.cookie.resgatarCookie('token');

    const headers: HttpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
      // Authorization: token,
    });
    return headers;
  }
}
