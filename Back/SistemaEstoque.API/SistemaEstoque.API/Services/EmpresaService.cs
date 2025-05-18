using SistemaEstoque.API.Context;
using SistemaEstoque.API.DTOs;
using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository.Interfaces;

namespace SistemaEstoque.API.Services
{
    public class EmpresaService
    {
        private IDbMethods<EmpresaModel> _repository;
        private UsuarioService _usarioService;
        private LoginService _loginService;
        private AppDbContext _context;
        private LicencaService _licenca;
        public EmpresaService(IDbMethods<EmpresaModel> repository, UsuarioService suarioService, LoginService loginService, AppDbContext context, LicencaService licenca)
        {
            _repository = repository;
            _usarioService = suarioService;
            _loginService = loginService;
            _context = context;
            _licenca = licenca;
        }

        public async Task<Response<string>> CadastrarNovaEmpresa(CadastroNovoEmpresaDTO empresa)
        {

            Response<string> token = new Response<string>();
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (await _usarioService.VereficaExistenciaUsuario(empresa.Usuario))

                        return Response<string>.Failed("Esse email de usuario ja está sendo utilizado");

                    EmpresaModel emp = empresa.Empresa;
                    #region Criação da empresa
                    #region Gerar Licenca
                    //Gera a licenca da empresa e retorna o id da licenca
                    int idLicenca = await _licenca.GerarLicenca(empresa.Empresa.TipoLicenca);

                    if (idLicenca == 0)
                        return Response<string>.Failed("Falha ao gerar a licenca da empresa");

                    emp.LicencaId = idLicenca;
                    #endregion
                    if (!await VereficaExistenciaEmpresa(empresa.Empresa))
                        return Response<string>.Failed("Empresa com esse cnpj ja está cadastrada");

                    if (!await _repository.Create(emp))

                        return Response<string>.Failed("Falha ao cadastrar a empresa");
                    #endregion

                    #region Criacao do usuario admin

                    empresa.Empresa = emp;
                    empresa.Usuario.EmpresaId = empresa.Empresa.id;
                    empresa.Usuario.TipoUsuario = Enums.TipoUsuario.Adm;
                    Response<UsuarioDTO> responseUsuario = await _usarioService.CriarUsuario(empresa.Usuario);
                    if (responseUsuario == null || !responseUsuario.success)
                        return Response<string>.Failed(responseUsuario.errorMessage);

                    #region Criacao do login do admin
                    token = await _loginService.CriarLogin(empresa.Login, responseUsuario.Data);
                    if (token == null || !token.success)

                        return Response<string>.Failed(token.errorMessage);
                    #endregion

                    #endregion

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return Response<string>.Failed(ex.Message);
                }
            }
            return token;


        }
        private async Task<bool> VereficaExistenciaEmpresa(EmpresaDTO empresa)
        {
            EmpresaModel emp = await _repository.BuscaDireto(empresa);
            if (emp != null)
            {
                return true;
            }
            return false;
        }

    }
}
