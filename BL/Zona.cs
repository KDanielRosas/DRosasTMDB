using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Zona
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DrosasUniSolutionsContext context = new())
                {
                    var query = context.Zonas.FromSqlRaw("ZonaGetAll").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var item in query)
                        {
                            ML.Zona zona = new ML.Zona();
                            zona.IdZona = item.IdZona;
                            zona.Nombre = item.Nombre;

                            result.Objects.Add(zona);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                throw;
            }
            return result;
        }//GetAll
    }
}
