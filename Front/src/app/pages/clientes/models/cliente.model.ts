import { CidadeModel } from "../../../services-geral/model/cidade.model";

export interface ClienteModel{
  id:number,
  nome: string,
  razao:string,
  cpf_cnpj: string,
  telefone: string,
  email:string,
  cidade:CidadeModel,
  rua:string,
  cep:string,
  fAtivo:number,
  dthCadastro: Date,
  dthAlteracao:Date,
}
