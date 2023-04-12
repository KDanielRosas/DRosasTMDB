using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class CineController : Controller
    {
        public ActionResult GetAll()
        {
            decimal[] porcent = BL.Cine.GetPorcentVenta();
            ML.Result result = BL.Cine.GetAll();
            return View(result);
        }

        [HttpGet]
        public ActionResult Form(int? idCine)
        {
            ML.Cine cine = new ML.Cine();
            cine.Zona = new ML.Zona();
            ML.Result resultZona = BL.Zona.GetAll();
            cine.Zona.Zonas = resultZona.Objects;

            if (idCine == null)
            {
                //Form Vacio
                return View(cine);
            }
            else
            {
                //GetById
                ML.Result result = BL.Cine.GetById(idCine.Value);

                if (result.Correct)
                {
                    cine = (ML.Cine)result.Object;
                    cine.Zona.Zonas = resultZona.Objects;
                    return View(cine);
                }
                else
                {
                    ViewBag.Message = "Error al mostrar la información";
                    return View("Modal");
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Cine cine)
        {
            ML.Result result = new();
            if (cine.IdCine == 0)
            {
                //Add
                result = BL.Cine.Add(cine);

                if (result.Correct)
                {
                    ViewBag.Message = "Punto de venta registrado correctamente";
                }
                else
                {
                    ViewBag.Message = "Error al registrar el punto de venta";
                }
                return View("Modal");
            }
            else
            {
                //Update
                result = BL.Cine.Update(cine);

                if (result.Correct)
                {
                    ViewBag.Message = "Registro actualizado correctamente";
                }
                else
                {
                    ViewBag.Message = "Error al actualizar el registro.";
                }
                return View("Modal");
            }
        }//Form(post)

        public ActionResult Delete(int idCine)
        {
            ML.Result result = BL.Cine.Delete(idCine);

            if (result.Correct)
            {
                ViewBag.Message = "Registro eliminado correctamente";
            }
            else
            {
                ViewBag.Message = "Error al eliminar";
            }
            return PartialView("Modal");
        }
    }
}
