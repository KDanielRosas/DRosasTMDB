using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cine
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new();
            try
            {
                using (DrosasUniSolutionsContext context = new())
                {
                    var query = context.Cines.FromSqlRaw("CineGetAll").ToList();

                    if (query.Count == 0)
                    {
                        result.Objects = null;
                    }
                    else
                    {
                        if (query != null)
                        {
                            result.Objects = new List<object>();
                            foreach (var item in query)
                            {
                                ML.Cine cine = new ML.Cine();

                                cine.IdCine = item.IdCine;
                                cine.Latitud = item.Latitud.Value;
                                cine.Longitud = item.Longitud.Value;
                                cine.Direccion = item.Direccion;
                                cine.Venta = item.Venta.Value;
                                cine.Zona = new ML.Zona();
                                cine.Zona.IdZona = item.IdZona.Value;
                                cine.Zona.Nombre = item.Nombre;

                                result.Objects.Add(cine);
                            }
                            result.Correct = true;
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Correct = false;
                throw;
            }
            return result;
        }//GetAll

        public static ML.Result GetById(int idCine)
        {
            ML.Result result = new();

            try
            {
                using (DrosasUniSolutionsContext context = new())
                {
                    var query = context.Cines.FromSqlRaw($"CineGetById {idCine}").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Cine cine = new ML.Cine();

                        cine.IdCine = query.IdCine;
                        cine.Latitud = query.Latitud.Value;
                        cine.Longitud = query.Longitud.Value;
                        cine.Direccion = query.Direccion;
                        cine.Venta = query.Venta.Value;
                        cine.Zona = new ML.Zona();
                        cine.Zona.IdZona = query.IdZona.Value;
                        cine.Zona.Nombre = query.Nombre;

                        result.Object = cine;
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
        }//GetById

        public static ML.Result Add(ML.Cine cine)
        {
            ML.Result result = new();

            try
            {
                using (DrosasUniSolutionsContext context = new())
                {
                    int query = context.Database.ExecuteSqlRaw($"CineAdd {cine.Latitud}," +
                        $"{cine.Longitud}, '{cine.Direccion}', {cine.Venta}, {cine.Zona.IdZona}");

                    if (query > 0)
                    {
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
        }//Add

        public static ML.Result Delete(int idCine)
        {
            ML.Result result = new();

            try
            {
                using (DrosasUniSolutionsContext context = new())
                {
                    int query = context.Database.ExecuteSqlRaw($"CineDelete {idCine}");

                    if (query > 0)
                    {
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
        }//Delete

        public static ML.Result Update(ML.Cine cine)
        {
            ML.Result result = new();

            try
            {
                using (DrosasUniSolutionsContext context = new())
                {
                    int query = context.Database.ExecuteSqlRaw($"CineUpdate {cine.IdCine}, '{cine.Latitud}'," +
                        $"'{cine.Longitud}', '{cine.Direccion}', {cine.Venta}, {cine.Zona.IdZona}");

                    if (query > 0)
                    {
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
        }//Update

        public static decimal[] GetPorcentVenta()
        {
            ML.Result result = GetAll();

            decimal totalVenta = 0;

            foreach (ML.Cine item in result.Objects)
            {
                totalVenta += item.Venta;
            }

            ML.Ventas resultVentas = new();
            resultVentas.ListVenta = new List<object>();

            foreach (ML.Cine item in result.Objects)
            {               
                ML.Ventas ventasPorcent = new();
                ventasPorcent.IdCine = item.IdCine;
                ventasPorcent.Venta = item.Venta;
                ventasPorcent.Zona = new();
                ventasPorcent.Zona.IdZona = item.Zona.IdZona;
                ventasPorcent.Porcentaje = (item.Venta * 100) / totalVenta;

                resultVentas.ListVenta.Add( ventasPorcent );
            }
            resultVentas.VentaTotal = totalVenta;

            decimal[] porcents = new decimal[5];

            foreach (ML.Ventas obj in resultVentas.ListVenta)
            {
                for (int i = 1; i < 5; i++)
                {
                    if (obj.Zona.IdZona == i)
                    {
                        porcents[i] += obj.Porcentaje;
                        break;
                    }
                }                
            }
            return porcents;
        }
    }
}
