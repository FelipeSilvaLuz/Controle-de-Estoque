using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Estoque.MvcCore.Controllers
{
    public class ItensController : BaseController
    {
        [Route("listagem")]
        public IActionResult ListaItens()
        {

            ViewBag.verBotoes = true;

            return View();
        }
    }
}