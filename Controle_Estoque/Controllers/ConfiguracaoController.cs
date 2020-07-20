using Estoque.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.MvcCore.Controllers
{
    public class ConfiguracaoController : Controller
    {
        private readonly IUsuarioAppService _usuarioAppService;
        public ConfiguracaoController(
            IUsuarioAppService usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }
        public IActionResult ListarUsuarios()
        {
            var listaUsuarios = _usuarioAppService.BuscarTodosUsuarios();
            return View("./ListarUsuarios", listaUsuarios);
        }
    }
}