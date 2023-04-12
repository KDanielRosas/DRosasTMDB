using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EstadisticasController : Controller
    {
        public ActionResult Index()
        {            
            return View();
        }

        public JsonResult GetVentas()
        {
            decimal[] result = BL.Cine.GetPorcentVenta();
            return Json(result);
        }

        public JsonResult GetCines()
        {
            ML.Result result = BL.Cine.GetAll();
            return Json(result);
        }
    }
}
