using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Estoque.Domain.Entities;
using Estoque.Util.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Estoque.MvcCore.Controllers
{
    public class BaseController : Controller
    {

        protected string NomeUsuario
        {
            get
            {
                return HttpContext.Session.GetString("Nome") ?? string.Empty;
            }
        }
        protected string EmailUsuario
        {
            get
            {
                return HttpContext.Session.GetString("Email") ?? string.Empty;
            }
        }
        protected string RamalUsuario
        {
            get
            {
                return HttpContext.Session.GetString("Ramal") ?? string.Empty;
            }
        }
        protected string CPFUsuario
        {
            get
            {
                return HttpContext.Session.GetString("CPF") ?? string.Empty;
            }
        }
        protected List<SessoesModel> Roles
        {
            get
            {
                List<SessoesModel> sessoes = new List<SessoesModel>();

                sessoes.Add(new SessoesModel() { Nome = "Email", Value = HttpContext.Session.GetString("Email") });
                sessoes.Add(new SessoesModel() { Nome = "Ramal", Value = HttpContext.Session.GetString("Ramal") });
                sessoes.Add(new SessoesModel() { Nome = "CPF", Value = HttpContext.Session.GetString("CPF") });
                sessoes.Add(new SessoesModel() { Nome = "Nome", Value = HttpContext.Session.GetString("Nome") });

                return sessoes.ToList();
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var nome = NomeUsuario.Split(" ");
            ViewBag.NomeUsuario = nome[0] + " " + nome[nome.Length - 1];

            List<SessoesModel> roles = Roles;

            if (roles == null)
                ViewBag.EstaLogado = false;
            else
                ViewBag.EstaLogado = true;

            base.OnActionExecuted(context);
        }
    }
}