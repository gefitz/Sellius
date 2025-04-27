import { Routes } from "@angular/router";
import { ClientesListComponent } from "./components/clientes-list/clientes-list.component";
import { ClientesCadastroComponent } from "./components/clientes-cadastro/clientes-cadastro.component";

export const ClienteRoutes: Routes = [
{
  path:"Cliente",
  component:ClientesListComponent,
  data:{title:"Clientes"}
},
{
  path:"Cliente/Cadastro",
  component:ClientesCadastroComponent,
  data:{title:"Novo Cliente"}
},
{
  path:"Cliente/Editar",
  component:ClientesCadastroComponent,
  data:{title:"Editar Cliente"}
},
]
