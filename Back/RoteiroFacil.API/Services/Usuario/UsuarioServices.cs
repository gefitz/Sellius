using AutoMapper;
using RoteiroFacil.API.DTOs;
using RoteiroFacil.API.Models;
using RoteiroFacil.API.Repository.Interfaces;

namespace RoteiroFacil.API.Services.Usuario
{
    public class UsuarioServices
    {
        private readonly IRepositoryCRUD<UsuarioModel> _repository;
        private readonly IMapper _mapper;
        private readonly LoginService _login;
        public UsuarioServices(IRepositoryCRUD<UsuarioModel> repository, IMapper mapper, LoginService login)
        {
            _repository = repository;
            _mapper = mapper;
            _login = login;
        }
        public async Task<bool> CreateUsuario(UsuarioDTO usuario)
        {
            //Verefica se existe usuario com email
            var vereficaSeExiste = await _repository.SearchObj(_mapper.Map<UsuarioModel>(usuario));
            if (vereficaSeExiste != null && vereficaSeExiste.Count != 0) return false;

            var ret = await _login.EncryptionPassword(usuario.Password);
            if (ret == null) return false;

            UsuarioModel model = _mapper.Map<UsuarioModel>(usuario);
            model.Hash = ret["hash"];
            model.Salta = ret["salt"];
            return await _repository.Create(model);

        }
        public async Task<bool> UpdateUsuario(UsuarioDTO usuarioDTO)
        {
            //Recuperar os dados do usuario 
            UsuarioModel usuario = await _repository.GetId(usuarioDTO.id);


            if (!string.IsNullOrEmpty(usuarioDTO.Password))
            {
                var ret = await _login.EncryptionPassword(usuarioDTO.Password);
                if (ret == null) return false;
                usuario.Hash = ret["hash"];
                usuario.Salta = ret["salt"];
            }

            usuario = _mapper.Map<UsuarioModel>(usuarioDTO);
            return await _repository.Update(usuario);
        }
    }
}
