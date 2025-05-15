import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { ProdutoModel } from '../models/produto.model';
import { map, Observable } from 'rxjs';
import { AuthGuardService } from '../../../core/auth-guard.service';
import { ResponseApiModel } from '../../../core/model/ResponseApi.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ProdutoFiltro } from '../models/produtoFiltro.model';

@Injectable({
  providedIn: 'root',
})
export class ProdutoService {
  private apiUrl = environment.apiUrl + '/Produto';
  constructor(
    private http: HttpClient,
    private cookie: AuthGuardService,
    private snack: MatSnackBar
  ) {}

  listProduto(produto: ProdutoFiltro): Observable<ProdutoModel[]> {
    return this.http
      .post<ResponseApiModel<ProdutoModel[]>>(
        this.apiUrl + '/ObterProduto',
        produto,
        {
          headers: {
            'Content-Type': 'application/json',
          },
        }
      )
      .pipe(map((resp) => resp.data));
  }
  async cadastrarProduto(produto: ProdutoModel) {
    await this.http
      .post<ResponseApiModel<ProdutoModel>>(
        this.apiUrl + '/CadastrarProduto',
        produto
      )
      .subscribe({
        next: (response) => {
          if (response.success) {
            this.snack.open('Sucesso ao casdastrar o produto', 'Ok', {
              duration: 1000,
            });
            window.location.reload();
          }
        },
        error: (response) => {
          this.snack.open('Falha ao cadastrar o produto', 'Ok');
        },
      });
  }
}
