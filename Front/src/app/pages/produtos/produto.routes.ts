import { Routes } from "@angular/router";
import { ProtudosListComponent } from "./components/produtos-list/protudos-list/protudos-list.component";
import { ProdutoCadastroComponent } from "./components/produtos-cadastro/produto-cadastro/produto-cadastro.component";

export const ProdutoRoutes: Routes = [
  {
    path:"Produto",
    component:ProtudosListComponent,
    data:{title:"Produtos"}
  },
  {
    path:"Produto/Cadastro",
    component:ProdutoCadastroComponent,
    data:{title:"Novo Produto"}
  },
  {
    path:"Produto/Editar",
    component:ProdutoCadastroComponent,
    data:{title:"Editar Produto"}
  },

]
