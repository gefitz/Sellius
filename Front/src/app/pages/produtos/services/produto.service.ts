import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ProdutoModel } from '../models/produto.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ProdutoFiltro } from '../models/produtoFiltro.model';
import { ApiService } from '../../../core/services/Api/api.service';
import { Paginacao } from '../../../core/model/paginacao.mode';
import { AuthGuardService } from '../../../core/services/AuthGuard/auth-guard.service';

@Injectable({
  providedIn: 'root',
})
export class ProdutoService {
  private apiUrl = '/Produto';
  constructor(
    private http: HttpClient,
    private cookie: AuthGuardService,
    private snack: MatSnackBar,
    private api: ApiService
  ) {}

  listProduto(
    produto: Paginacao<ProdutoModel, ProdutoFiltro>
  ): Paginacao<ProdutoModel, ProdutoFiltro> {
    this.api
      .post<Paginacao<ProdutoModel, ProdutoFiltro>>(
        this.apiUrl + '/ObterProduto',
        produto
      )
      .subscribe({
        next: (ret) => {
          produto = ret;
        },
        error: (ret) => {
          this.snack.open(ret.errorMessage, 'Ok', { duration: 5000 });
        },
      });
    return produto;
  }
  async cadastrarProduto(produto: ProdutoModel) {
    await this.api
      .post<ProdutoModel>(this.apiUrl + '/CadastrarProduto', produto)
      .subscribe({
        next: (response) => {
          if (response) {
            this.snack.open('Sucesso ao casdastrar o produto', 'Ok', {
              duration: 1000,
            });
            window.location.reload();
          }
        },
        error: (response) => {
          this.snack.open(response.errorMessage, 'Ok');
        },
      });
  }
}
