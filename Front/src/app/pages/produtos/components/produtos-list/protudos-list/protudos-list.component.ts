import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { Router, RouterLink } from '@angular/router';
import { ProdutoModel } from '../../../models/produto.model';
import { MatDialog } from '@angular/material/dialog';
import { DialogFiltroProdutoComponent } from '../../dialog-filtro-produto/dialog-filtro-produto.component';
import { DialogInativarProdutoComponent } from '../../dialog-inativar-produto/dialog-inativar-produto.component';
import { CommonModule, DatePipe } from '@angular/common';
import { EtiquetaComponent } from '../../etiqueta/etiqueta.component';
import { EtiquetaModel } from '../../../models/etiqueta.model';
import { ProdutoService } from '../../../services/produto.service';
import { ProdutoFiltro } from '../../../models/produtoFiltro.model';
import { error } from 'console';
@Component({
  selector: 'app-protudos-list',
  standalone: true,
  imports: [
    MatPaginatorModule,
    MatTableModule,
    MatButtonModule,
    MatIcon,
    RouterLink,
    MatButtonModule,
    MatIconModule,
    CommonModule,
  ],
  templateUrl: './protudos-list.component.html',
  styleUrl: './protudos-list.component.css',
})
export class ProtudosListComponent implements AfterViewInit {
  displayedColumns: string[] = [
    'btnEditar',
    'fAtivo',
    'nome',
    'tipoProduto',
    'descricao',
    'marca',
    'qtd',
    'dthCadastro',
  ];
  dataSource!: MatTableDataSource<ProdutoModel>;
  produtoFiltro!: ProdutoFiltro;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  _pipe: DatePipe;
  constructor(
    private router: Router,
    private dialog: MatDialog,
    private service: ProdutoService,
    private pipe: DatePipe
  ) {
    this.carregarProduto();
    this._pipe = pipe;
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }
  editarProduto(produto: ProdutoModel) {
    this.router.navigateByUrl('/Produto/Cadastro', {
      state: { produto },
    });
  }
  abrirModalPesquisa() {
    const dialog = this.dialog.open(DialogFiltroProdutoComponent);

    dialog.afterClosed().subscribe((result) => {
      //adicionar o metodo que carrega a tabela com parametros do filtro
      if (result) {
        this.produtoFiltro = result;
      }
    });
  }
  inativarProduto(produto: ProdutoModel) {
    const dialog = this.dialog.open(DialogInativarProdutoComponent, {
      data: produto,
    });

    dialog.afterClosed().subscribe((result) => {
      if (result) {
        console.log('Verdadeiro');
      } else {
        console.log('Falso');
      }
    });
  }
  imprimirEtiqueta(produto: ProdutoModel) {
    const etiqueta: EtiquetaModel = {
      nomeProduto: produto.nome,
      codigo: 'asdasd',
      preco: 1.5,
    };
    this.dialog.open(EtiquetaComponent, {
      data: etiqueta,
    });
  }
  async carregarProduto() {
    this.service.listProduto(this.produtoFiltro).subscribe({
      next: (produtoList) => {
        this.dataSource = new MatTableDataSource<ProdutoModel>(produtoList);
        console.log(produtoList);
      },
      error: (resp) => {},
    });
  }
}
