using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            ML.Result result = BL.Usuario.GetByUserName(username, password);
            
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                int idUsuario = usuario.IdUsuario;
                if (usuario.Password == password)
                {
                    return RedirectToAction("GetAll", "Cine");
                }//if
                else
                {
                    ViewBag.Caso = "LoginFail";
                    ViewBag.Message = "Contraseña incorrecta";
                    return PartialView("Modal");
                }//if
            }//if
            else
            {
                ViewBag.Caso = "LoginFail";
                ViewBag.Message = "El usuario no existe";
                return PartialView("Modal");
            }//else           
        }//Login(post)

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }//SignUp(get)

        [HttpPost]
        public ActionResult SignUp(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                ViewBag.Message = "No se permiten ingresar campos vacios.";
                ViewBag.Caso = "SignUpFail";
                return PartialView("Modal");
            }
            else
            {
                ML.Result result = BL.Usuario.Add(userName, password);
                if (result.Correct)
                {
                    ViewBag.Message = "Registro exitoso";
                    ViewBag.Caso = "SignUpSuccess";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "El userName ya existe, ingrese uno diferente.";
                    ViewBag.Caso = "SignUpFail";
                    return PartialView("Modal");
                }
            }
        }//SignUp(post)

        public ActionResult LogOut()
        {
            return View("Login");
        }//LogOut

        [HttpPost]
        public JsonResult CheckUserName(string userName)
        {
            ML.Result result = BL.Usuario.CheckUserName(userName);

            return Json(result);
        }//CheckUserName
    }
}
