using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PL.Controllers
{
    public class PeliculasController : Controller
    {
        public string apiKey = "3e6f67d5f0c672ebf42531ee86f98639";
        public string urlApi = "https://api.themoviedb.org/3/";
        public string urlImage = "https://www.themoviedb.org/t/p/w600_and_h900_bestv2";
        public string idAccount = "18694752";
        public string sessionId = "1c015feff0875872e6264e70f5c7baabc09b0c6d";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PopularMovies()
        {
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);

                var responseTask = client.GetAsync("movie/popular?api_key=" + apiKey + "&language=es-MX&page=1");
                responseTask.Wait();

                var resultServicio = responseTask.Result;
                Models.Movie movie = new Models.Movie();

                if (resultServicio.IsSuccessStatusCode)
                {
                    var readTask = resultServicio.Content.ReadAsStringAsync();
                    dynamic resultJSON = JObject.Parse(readTask.Result.ToString());
                    readTask.Wait();
                    movie.Movies = new List<object>();

                    foreach (var item in resultJSON.results)
                    {
                        Models.Movie movieItem = new Models.Movie();
                        movieItem.Id = item.id;
                        movieItem.Titulo = item.title;
                        movieItem.Descripcion = item.overview;
                        movieItem.Imagen = urlImage + item.backdrop_path;

                        movie.Movies.Add(movieItem);
                    }
                }
                return View(movie);
            }//using            
        }//PopularMovies

        public ActionResult AddFavorite(int IdMovie)
        {
            Models.FavMovie favMovie = new Models.FavMovie
            {
                favorite = true,
                media_id = IdMovie,
                media_type = "movie"
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                //POST
                var postTask = client.PostAsJsonAsync("account/" + idAccount + "/favorite?api_key=" + apiKey + "&session_id=" + sessionId, favMovie);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Se ha añadido a favoritos";
                }
                else
                {
                    ViewBag.Message = "Error al añadir";
                }
            }
            ViewBag.To = "Popular";
            return PartialView("Modal");
        }//AddFavorite

        public ActionResult DeleteFavorite(int IdMovie)
        {
            Models.FavMovie favMovie = new Models.FavMovie
            {
                favorite = false,
                media_id = IdMovie,
                media_type = "movie"
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                //POST
                var postTask = client.PostAsJsonAsync("account/" + idAccount + "/favorite?api_key=" + apiKey + "&session_id=" + sessionId, favMovie);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Se ha eliminado de favoritos.";
                }
                else
                {
                    ViewBag.Message = "Error al eliminar de favoritos";
                }
            }
            ViewBag.To = "Favorites";
            return PartialView("Modal");
        }//DeleteFavorite

        public ActionResult GetFavorites()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);

                var responseTask = client.GetAsync("account/" + idAccount + "/favorite/movies?api_key=" + apiKey + "&session_id=" + sessionId + "&language=es-MX&sort_by=created_at.asc");
                responseTask.Wait();

                var resultServicio = responseTask.Result;
                Models.Movie movie = new Models.Movie();

                if (resultServicio.IsSuccessStatusCode)
                {
                    var readTask = resultServicio.Content.ReadAsStringAsync();
                    dynamic resultJSON = JObject.Parse(readTask.Result.ToString());
                    readTask.Wait();
                    movie.Movies = new List<object>();

                    foreach (var item in resultJSON.results)
                    {
                        Models.Movie movieItem = new Models.Movie();
                        movieItem.Id = item.id;
                        movieItem.Titulo = item.title;
                        movieItem.Descripcion = item.overview;
                        movieItem.Imagen = urlImage + item.backdrop_path;

                        movie.Movies.Add(movieItem);
                    }
                }
                return View(movie);
            }//using            
        }//GetFavorites
    }//Controller
}//NS
