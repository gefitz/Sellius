import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-usuario-cadastro',
  standalone: true,
  imports: [
    MatCardModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatInputModule,
    RouterLink,
    MatIconModule,
  ],
  templateUrl: './usuario-cadastro.component.html',
  styleUrl: './usuario-cadastro.component.css',
})
export class UsuarioCadastroComponent {
  formUsuario: FormGroup;
  constructor() {
    this.formUsuario = new FormGroup({
      id: new FormControl(''),
      nome: new FormControl(''),
      email: new FormControl(''),
      telefone: new FormControl(''),
      cpf_cnpj: new FormControl(''),
      password: new FormControl(''),
      confirmaPassword: new FormControl(''),
      dthCadastro: new FormControl(''),
    });
  }
  submitCadastro() {}
}
