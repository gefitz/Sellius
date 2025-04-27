import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/components/home/home.component';
import { ProtudosListComponent } from './pages/produtos/components/produtos-list/protudos-list/protudos-list.component';
import { ProdutoCadastroComponent } from './pages/produtos/components/produtos-cadastro/produto-cadastro/produto-cadastro.component';
import { ClientesListComponent } from './pages/clientes/components/clientes-list/clientes-list.component';
import { PedidosComponent } from './pages/pedido/components/pedidos/pedidos.component';
import { ClientesCadastroComponent } from './pages/clientes/components/clientes-cadastro/clientes-cadastro.component';
import { ClienteRoutes } from './pages/clientes/cliente.routes';
import { ProdutoRoutes } from './pages/produtos/produto.routes';
import { PedidoRoutes } from './pages/pedido/pedido.routes';
import { SidenavComponent } from './shared/sidenav/component/sidenav/sidenav.component';
import { AppComponent } from './app.component';
import { AuthenticationComponent } from './shared/authentication/authentication.component';
import { LoginComponent } from './pages/login/components/login/login.component';
import { AuthGuardService } from './services-geral/auth-guard.service';
import { Usuarioroutes } from './pages/usuario/usario.routes';
import { title } from 'process';

export const routes: Routes = [
  {
    path: '',
    component: SidenavComponent,
    children: [...ClienteRoutes, ...ProdutoRoutes, ...PedidoRoutes],
    canActivate: [AuthGuardService],
  },
  {
    path: '',
    component: AuthenticationComponent,
    children: [
      {
        path: 'Login',
        component: LoginComponent,
        data: { title: 'Entrar / Login no Sistema' },
      },
      ...Usuarioroutes,
    ],
  },
  { path: '**', redirectTo: 'Login' },
];
