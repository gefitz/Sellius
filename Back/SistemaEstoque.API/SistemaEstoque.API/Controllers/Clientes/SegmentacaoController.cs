using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaEstoque.API.DTOs.CadastrosDTOs.ClientesCadastros;
using SistemaEstoque.API.DTOs.TabelasDTOs;
using SistemaEstoque.API.Repository.Interfaces;
using SistemaEstoque.API.Services.segmentacaos;
using SistemaEstoque.API.Utils;

namespace SistemaEstoque.API.Controllers.Clientes
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class SegmentacaoController: Controller
    {
        private readonly SegmentacaoService _service;

        public SegmentacaoController(SegmentacaoService service)
        {
            _service = service;
        }
        [HttpPost("novo")]
        public async Task<IActionResult> NovoSegmentacao(SegmentacaoDTO segmento)
        {
            segmento.idEmpresa = TokenService.RecuperaIdEmpresa(User);
            var response = await _service.CriarSegmento(segmento);
            if (response.success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPost("listaSegmentacao")]
        public async Task<IActionResult> Paginacao(PaginacaoTabelaResult<SegmentacaoDTO, SegmentacaoDTO> segmento)
        {
            segmento.Filtro.idEmpresa = TokenService.RecuperaIdEmpresa(User);
            var response = await _service.Buscarsegmentacaos(segmento);
            if (response.success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(SegmentacaoDTO segmento)
        {
            segmento.idEmpresa = TokenService.RecuperaIdEmpresa(User);
            var response = await _service.Updatesegmentacao(segmento);
            if (response.success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.Inativarsegmentacao(id);
            if (response.success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
