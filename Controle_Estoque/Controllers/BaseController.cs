using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Estoque.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Estoque.MvcCore.Controllers
{
    public class BaseController : Controller
    {

        protected string NomeUsuario
        {
            get
            {
                var identity = User.Identity as ClaimsIdentity;
                var userData = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData);
                if (string.IsNullOrEmpty(userData?.Value ?? string.Empty))
                    return string.Empty;
                AutenticacaoUsuarios autenticacao = new AutenticacaoUsuarios();

                var usuario = autenticacao;
                if (usuario == null)
                    return string.Empty;

                return usuario.Nome;
            }
        }
        protected string EmailUsuario
        {
            get
            {
                var identity = User.Identity as ClaimsIdentity;
                var userData = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData);
                if (string.IsNullOrEmpty(userData?.Value ?? string.Empty))
                    return string.Empty;
                AutenticacaoUsuarios autenticacao = new AutenticacaoUsuarios();

                var usuario = autenticacao;
                if (usuario == null)
                    return string.Empty;

                return usuario.Email;
            }
        }
        protected string RamalUsuario
        {
            get
            {
                var identity = User.Identity as ClaimsIdentity;
                var userData = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData);
                if (string.IsNullOrEmpty(userData?.Value ?? string.Empty))
                    return string.Empty;
                AutenticacaoUsuarios autenticacao = new AutenticacaoUsuarios();

                var usuario = autenticacao;
                if (usuario == null)
                    return string.Empty;

                return usuario.Ramal;
            }
        }
        protected string CPFUsuario
        {
            get
            {
                var identity = User.Identity as ClaimsIdentity;
                var userData = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData);
                if (string.IsNullOrEmpty(userData?.Value ?? string.Empty))
                    return string.Empty;
                AutenticacaoUsuarios autenticacao = new AutenticacaoUsuarios();

                var usuario = autenticacao;
                if (usuario == null)
                    return string.Empty;

                return usuario.CPF;
            }
        }
        protected List<string> Roles
        {
            get
            {
                var identity = User.Identity as ClaimsIdentity;
                var userData = identity.Claims.Where(c => c.Type == ClaimTypes.Role);
                var userDataa = identity.Claims;

                if (userData != null)
                    return userData.Select(x => x.Value).ToList();

                return new List<string>();
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ViewBag.NomeUsuario = NomeUsuario;

            List<string> roles = Roles;

            if (roles == null)
                ViewBag.EstaLogado = false;
            else
                ViewBag.EstaLogado = true;
        }
    }
}