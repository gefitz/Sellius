using Microsoft.EntityFrameworkCore;
using SistemaEstoque.API.Context;
using SistemaEstoque.API.DTOs.TabelasDTOs;
using SistemaEstoque.API.Models.Cliente;
using SistemaEstoque.API.Repository.Cliente.Interfaces;
using SistemaEstoque.API.Repository.Empresa.Interface;

namespace SistemaEstoque.API.Repository.Cliente
{
    public class GrupoClientesRepository : IGrupoClientesRepository
    {
        private readonly AppDbContext _context;

        public GrupoClientesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GrupoClienteModel> BuscaDireto(GrupoClienteModel idObjeto)
        {
            try
            {
                return await _context.GrupoClientes.Where(g => g.id == idObjeto.id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GrupoClienteModel>> CarregarCombo(int idEmpresa)
        {
            try
            {
                return await _context.GrupoClientes.Where(g => g.idEmpresa == idEmpresa).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Create(GrupoClienteModel obj)
        {
            try
            {
                _context.GrupoClientes.Add(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> Delete(GrupoClienteModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginacaoTabelaResult<GrupoClienteModel, GrupoClienteModel>> Filtrar(PaginacaoTabelaResult<GrupoClienteModel, GrupoClienteModel> obj)
        {
            try
            {

                var query = _context.GrupoClientes.AsQueryable();

                if (!string.IsNullOrEmpty(obj.Filtro.nome))
                    query = query.Where(g => g.nome.Contains(obj.Filtro.nome));
                if (obj.Filtro.fAtivo != -1)
                    query = query.Where(g => g.fAtivo == obj.Filtro.fAtivo);
                query = query.Where(g => g.idEmpresa == obj.Filtro.idEmpresa);
                obj.TotalRegistros = query.Count();
                obj.TotalPaginas = (int)Math.Ceiling((double)obj.TotalRegistros / obj.TamanhoPagina);

                obj.Dados = await query
                    .OrderBy(p => p.id)
                    .Skip((obj.PaginaAtual - 1) * obj.TotalRegistros)
                    .Take(obj.TamanhoPagina)
                    .ToListAsync();

                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Task<IEnumerable<GrupoClienteModel>> Filtrar(GrupoClienteModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(GrupoClienteModel obj)
        {
            try
            {
                _context.Entry(obj).State = EntityState.Modified;
                _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
