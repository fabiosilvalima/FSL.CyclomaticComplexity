using FSL.CyclomaticComplexity.Models.Novo;
using FSL.CyclomaticComplexity.Models.Novo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace FSL.CyclomaticComplexity.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFactory _factory;

        public HomeController(IFactory factory)
        {
            _factory = factory;
        }

        public ActionResult Index()
        {
            var diaSemana = _factory.InstanceOf<ITipoDataService>(TipoData.DiaDaSemana);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}