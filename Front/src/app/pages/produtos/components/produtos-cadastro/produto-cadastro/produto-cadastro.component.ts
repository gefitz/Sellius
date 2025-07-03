import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { Router, RouterLink } from '@angular/router';
import { MatSelectModule } from '@angular/material/select';
import { FlexLayoutModule } from '@angular/flex-layout';
import { ProdutoModel } from '../../../models/produto.model';
import { ProdutoService } from '../../../services/produto.service';
import { CommonModule } from '@angular/common';
import { TpProdutoModel } from '../../../models/tpProduto.model';
import { TpProdutoService } from '../../../services/tp-produto.service';

@Component({
  selector: 'app-produto-cadastro',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatIconModule,
    MatButtonModule,
    RouterLink,
    MatSelectModule,
    FlexLayoutModule,
    CommonModule,
  ],
  templateUrl: './produto-cadastro.component.html',
  styleUrl: './produto-cadastro.component.css',
})
export class ProdutoCadastroComponent {
  produtoForm!: FormGroup;
  tpProduto!: TpProdutoModel[];
  constructor(
    private router: Router,
    private service: ProdutoService,
    private tpService: TpProdutoService
  ) {
    const nav = router.getCurrentNavigation();
    const produtoEditar: ProdutoModel = nav?.extras.state?.['produto'];
    this.carregarCombos();
    this.preencherCamposFormulario(produtoEditar);
  }
  salvarProduto() {
    if (this.produtoForm.valid) {
      this.produtoForm.value.dthCriacao = new Date(
        this.produtoForm.value.dthCriacao
      ).toISOString();
      const produto: ProdutoModel = this.produtoForm.value;
      console.log(produto);
      this.service.cadastrarProduto(produto);
    }
  }
  preencherCamposFormulario(produtoEditar: ProdutoModel) {
    if (produtoEditar) {
      this.produtoForm = new FormGroup({
        id: new FormControl(produtoEditar.id),
        Nome: new FormControl(produtoEditar.nome, Validators.required),
        tipoProduto: new FormControl(
          produtoEditar.tipoProduto,
          Validators.required
        ),
        qtd: new FormControl(produtoEditar.qtd, [
          Validators.required,
          Validators.min(0),
        ]),
        marca: new FormControl(produtoEditar.marca, Validators.required),
        fAtivo: new FormControl(produtoEditar.fAtivo, Validators.required),
        dthCriacao: new FormControl(produtoEditar.dthCriacao),
        descricao: new FormControl(produtoEditar.descricao),
        valor: new FormControl(produtoEditar.valor, Validators.required),
      });
    } else {
      this.produtoForm = new FormGroup({
        id: new FormControl(0),
        Nome: new FormControl('', Validators.required),
        tipoProduto: new FormControl('', Validators.required),
        qtd: new FormControl('', [Validators.required, Validators.min(0)]),
        marca: new FormControl('', Validators.required),
        fAtivo: new FormControl('0', Validators.required),
        dthCriacao: new FormControl(new Date().toISOString().split('T')[0]),
        descricao: new FormControl(''),
        valor: new FormControl(0, Validators.required),
      });
    }
  }
  carregarCombos() {
    this.tpService.carregarTpProdutoCombo().subscribe({
      next: (ret) => {
        this.tpProduto = ret;
      },
    });
  }
}
