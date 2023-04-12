using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Cine
    {
        public int IdCine { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string? Direccion { get; set; }
        public decimal Venta { get; set; }
        public ML.Zona Zona { get; set; }

    }
}
