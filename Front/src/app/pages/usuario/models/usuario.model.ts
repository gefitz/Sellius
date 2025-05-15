import { CidadeModel } from '../../../core/model/cidade.model';

export interface UsuarioModel {
  id: number;
  nome: string;
  documento: string;
  email: string;
  password: string;
  cidade: CidadeModel;
  cep: string;
  rua: string;
}
