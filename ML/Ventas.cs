using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Ventas
    {
        public int IdCine { get; set; }
        public decimal Venta { get; set; }
        public decimal VentaTotal { get; set; }
        public decimal Porcentaje { get; set; }
        public List<object> ListVenta { get; set; }
        public ML.Zona Zona { get; set; }
    }
}
