import { Component, } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { Router, RouterLink } from '@angular/router';
import { CidadeModel } from '../../../../services-geral/model/cidade.model';
import { ClienteModel } from '../../models/cliente.model';
import { EstadoModel } from '../../../../services-geral/model/estado.model';
import { CommonModule } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { DialogInativarComponent } from '../dialog-inativar/dialog-inativar.component';
import { DialogFiltroComponent } from '../dialog-filtro/dialog-filtro.component';

@Component({
  selector: 'app-clientes-list',
  standalone: true,
  imports: [
    MatPaginatorModule,
    MatTableModule,
    MatButtonModule,
    MatIcon,
    RouterLink,
    MatIconModule,
    CommonModule
  ],
  templateUrl: './clientes-list.component.html',
  styleUrl: './clientes-list.component.css'
})
export class ClientesListComponent {
  displayedColumns: string[] = ["btnEditar","fAtivo",'nome', 'razao', 'cpf_cnpj', 'telefone','email','cep','cidade','rua',"dthCadastro","dthAlteracao",];
    dataSource = new MatTableDataSource<ClienteModel>(cliente);
    constructor(private router:Router,  private dialog: MatDialog){}
    editarCliente(cliente:ClienteModel){
      this.router.navigateByUrl("Cliente/Editar",{
        state:cliente
      })
    }
    inativarCliente(cliente:ClienteModel){
       this.dialog.open(DialogInativarComponent,{
        data:cliente
       })
    }
    abrirModalPesquisa(){
      const dialog = this.dialog.open(DialogFiltroComponent)
    }
}
const estado: EstadoModel ={
  id: 1,
  estado: "Parana",
  sigla:"PR"
}
const cidade: CidadeModel ={
  id:1,
  cidade:"Colombo",
  estado: estado
}
const cliente: ClienteModel[] = [{
  id:0,
  nome: "string",
  razao:"string",
  cpf_cnpj: "string",
  telefone: "string",
  email:"string",
  cidade: cidade,
  rua:"string",
  cep:"string",
  fAtivo:1,
  dthCadastro: new Date(),
  dthAlteracao:new Date(),

}]
