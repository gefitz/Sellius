import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { RouterLink } from '@angular/router';
import { ClienteModel } from '../../models/cliente.model';
import { CidadeModel } from '../../../../services-geral/model/cidade.model';
import { EstadoModel } from '../../../../services-geral/model/estado.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-clientes-cadastro',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatIconModule,
    MatButtonModule,
    RouterLink,
    MatSelectModule,
    CommonModule

  ],
  templateUrl: './clientes-cadastro.component.html',
  styleUrl: './clientes-cadastro.component.css'
})
export class ClientesCadastroComponent {
  cidade:CidadeModel[] = [{
    id:1,
    cidade:"Colombo",
    estado: {
      estado:"Parana",
      sigla:"PR",
      id: 1
    }
  }];
  clienteForm: FormGroup;

  constructor(){
    this.clienteForm = new FormGroup({
        id:new FormControl(''),
        nome: new FormControl(''),
        razao:new FormControl(''),
        cpf_cnpj: new FormControl(''),
        telefone: new FormControl(''),
        email:new FormControl(''),
        cidade:new FormControl(null),
        rua:new FormControl(''),
        cep:new FormControl(''),
        fAtivo:new FormControl(''),
        dthCadastro: new FormControl(''),
        dthAlteracao:new FormControl(''),
    })
  }
  salvarCliente(){

    var cliente: ClienteModel = this.clienteForm.value
    console.log(cliente);
  }
}
