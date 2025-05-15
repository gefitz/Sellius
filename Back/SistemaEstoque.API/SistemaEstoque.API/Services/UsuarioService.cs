using AutoMapper;
using SistemaEstoque.API.DTOs;
using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository;
using SistemaEstoque.API.Repository.Interfaces;

namespace SistemaEstoque.API.Services
{
    public class UsuarioService
    {
        private readonly IDbMethods<UsuarioModel> _repository;
        private readonly IMapper _mapper;
        private readonly LoginService _login;

        public UsuarioService(IDbMethods<UsuarioModel> repository, IMapper mapper, LoginService login)
        {
            _repository = repository;
            _mapper = mapper;
            _login = login;
        }
        public async Task<Response<UsuarioDTO>> CriarUsuario(UsuarioDTO dTO)
        {
            try
            {

                UsuarioModel usuario = _mapper.Map<UsuarioModel>(dTO);
                var vereficaUsuario = await _repository.BuscaDireto(usuario);
                if(vereficaUsuario != null) { return Response<UsuarioDTO>.Failed("Email já esta sendo utilizado"); }
                Dictionary<string, byte[]> hashSalt = _login.CriptografiaSenha(dTO.Password);
                if (hashSalt.Count == 0) { return Response<UsuarioDTO>.Failed("Falha ao criptografar a senha"); }
                usuario.Salt = hashSalt["salt"];
                usuario.Hash = hashSalt["hash"];
                if(await _repository.Create(usuario))
                {

                return Response<UsuarioDTO>.Ok();
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
        public async Task<Response<string>> LoginAutenticacao(LoginDTO usuarioDTO)
        {
            var usuario = _mapper.Map<UsuarioModel>(usuarioDTO);
            var retornoUsuario = await _repository.BuscaDireto(usuario);
            if (retornoUsuario == null) { return Response<string>.Failed("Email invalidado"); }
            if (!await _login.ValidaSenha(retornoUsuario, usuarioDTO.Password)) { return Response<string>.Failed("Senha incorreta"); }
            return await _login.GerarCookie(retornoUsuario);

        }
    }
}
