using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estoque.Application.Interfaces;
using Estoque.Util;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.MvcCore.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAutenticacaoUsuarioAppService _autenticacaoUsuarioAppService;
        public LoginController(IAutenticacaoUsuarioAppService autenticacaoUsuarioAppService)
        {
            _autenticacaoUsuarioAppService = autenticacaoUsuarioAppService;
        }
        public IActionResult Autenticar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ValidarAcesso([FromBody] AutenticarUsuarioViewModel dados)
        {

           var teste =  _autenticacaoUsuarioAppService.ValidarUsuario(dados);


            return null;
        }
    }
}