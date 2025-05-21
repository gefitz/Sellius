using AutoMapper;
using SistemaEstoque.API.DTOs;
using SistemaEstoque.API.DTOs.CadastrosDTOs;
using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository;
using SistemaEstoque.API.Repository.Interfaces;

namespace SistemaEstoque.API.Services
{
    public class UsuarioService
    {
        private readonly IDbMethods<UsuarioModel> _repository;
        private readonly LoginService _login;

        public UsuarioService(IDbMethods<UsuarioModel> repository, LoginService login)
        {
            _repository = repository;
            _login = login;
        }
        public async Task<Response<UsuarioDTO>> CriarUsuario(UsuarioDTO dTO)
        {
            try
            {

                UsuarioModel usuario = dTO;
                if (await VereficaExistenciaUsuario(dTO)) { return Response<UsuarioDTO>.Failed("Email já esta sendo utilizado"); }

                if (await _repository.Create(usuario))
                {
                    dTO = usuario;
                    return Response<UsuarioDTO>.Ok(dTO);
                }
                return Response<UsuarioDTO>.Failed("Falha ao cadastrar usuario");
            }
            catch (ApplicationException ex)
            {
                return Response<UsuarioDTO>.Failed(ex.Message);
            }

            catch (Exception ex)
            {
                return Response<UsuarioDTO>.Failed(ex.Message);

            }

        }
        public async Task<bool> VereficaExistenciaUsuario(UsuarioDTO dto)
        {
                UsuarioModel usiario = await _repository.BuscaDireto(dto);
                if (usiario != null)
                    return true;
                return false;
        }
    }
}
