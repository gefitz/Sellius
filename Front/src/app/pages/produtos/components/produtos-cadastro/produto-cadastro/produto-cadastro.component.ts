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
  ],
  templateUrl: './produto-cadastro.component.html',
  styleUrl: './produto-cadastro.component.css',
})
export class ProdutoCadastroComponent {
  produtoForm!: FormGroup;
  constructor(private router: Router) {
    const nav = router.getCurrentNavigation();
    const produtoEditar: ProdutoModel = nav?.extras.state?.['produto'];
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
        dthCadastro: new FormControl(produtoEditar.dthCadastro),
        descricao: new FormControl(produtoEditar.descricao),
      });
      console.log(this.produtoForm.value);
    } else {
      this.produtoForm = new FormGroup({
        id: new FormControl(''),
        Nome: new FormControl('', Validators.required),
        tipoProduto: new FormControl('', Validators.required),
        qtd: new FormControl('', [Validators.required, Validators.min(0)]),
        marca: new FormControl('', Validators.required),
        fAtivo: new FormControl('0', Validators.required),
        dthCadastro: new FormControl(new Date().toISOString().split('T')[0]),
        descricao: new FormControl(''),
      });
    }
  }
  salvarProduto() {
    if (this.produtoForm.valid) {
      const produto: ProdutoModel = this.produtoForm.value;
      console.log(produto);
    }
  }
}
