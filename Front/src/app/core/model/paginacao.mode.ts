export class Paginacao<model, filtroModel> {
  paginaAtual: number = 0;
  totalPaginas: number = 0;
  totalRegistros: number = 10;
  tamanhoPagina: number = 0;
  dados!: model[];
  filtro!: filtroModel;
}
