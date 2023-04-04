namespace PL.Models
{
    public class Movie
    {
        public List<Object> Movies { get; set; }
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string FechaLanzamiento { get; set; }
        public string Imagen { get; set; }
    }
}
