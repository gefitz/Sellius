import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { RouterLink } from '@angular/router';
import { UsuarioModel } from '../../models/usuario.model';
import { UsuarioserviceService } from '../../services/usuarioservice.service';
import { CommonModule } from '@angular/common';
import { CidadeModel } from '../../../../core/model/cidade.model';
import { EstadoModel } from '../../../../core/model/estado.model';
import { MatSelectModule } from '@angular/material/select';

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
    CommonModule,
    MatSelectModule,
  ],
  templateUrl: './usuario-cadastro.component.html',
  styleUrl: './usuario-cadastro.component.css',
})
export class UsuarioCadastroComponent {
  estado: EstadoModel = {
    id: 1,
    estado: 'Parana',
    sigla: 'Pr',
  };
  cidade: CidadeModel[] = [
    {
      id: 1,
      cidade: 'Colombo',
      estado: this.estado,
    },
  ];
  formUsuario: FormGroup;
  constructor(private service: UsuarioserviceService) {
    this.formUsuario = new FormGroup({
      id: new FormControl(0),
      nome: new FormControl('', Validators.required),
      email: new FormControl('', Validators.required),
      documento: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
      confirmaPassword: new FormControl('', Validators.required),
      cidade: new FormControl(null, Validators.required),
      cep: new FormControl('', Validators.required),
      rua: new FormControl('', Validators.required),
    });
  }
  submitCadastro() {
    if (this.formUsuario.valid) {
      const usuairo: UsuarioModel = this.formUsuario.value;
      this.service.createUsuario(usuairo);
    }
  }
}
