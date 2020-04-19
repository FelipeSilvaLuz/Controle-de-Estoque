using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estoque.Util;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.MvcCore.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Autenticar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ValidarAcesso([FromBody] DadosUsuarioViewModel dados)
        {
            return null;
        }
    }
}