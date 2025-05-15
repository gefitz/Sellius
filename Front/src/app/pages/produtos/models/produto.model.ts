import { TpProdutoModel } from '../../tpProduto/models/tpProduto.model';

export interface ProdutoModel {
  id: number;
  nome: string;
  tipoProduto: TpProdutoModel;
  qtd: number;
  marca: number;
  fAtivo: number;
  dthCriacao: Date;
  descricao: string;
  valor: number;
}
