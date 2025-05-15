using SistemaEstoque.API.Models;
using SistemaEstoque.API.Repository;
using Microsoft.AspNetCore.Mvc;
using SistemaEstoque.API.Services;
using SistemaEstoque.API.DTOs;

namespace SistemaEstoque.API.Controllers
{
    public class PedidoController : Controller
    {
        private readonly PedidoServices _service;

        public PedidoController(PedidoServices service)
        {
            _service = service;
        }
        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    IEnumerable<PedidoDTO> cliente = await _service.BuscarPedidos(new PedidoDTO());
        //    ViewBag.Clientes = cliente;
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Index(PedidoDTO pedidoDTO)
        //{

        //}
        //public async Task<IActionResult> Cadastrar()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Cadastrar(PedidoDTO pedido)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.Error = "Pedido foi passado sem nenhuma informação";
        //        return View();
        //    }
        //    if (await _service.CadastrarPedido(pedido)) { return RedirectToAction("Index", "Cliente"); }
            
        //    ViewBag.Error = _log.Messagem;
        //    return View();

        //}

    }
}
