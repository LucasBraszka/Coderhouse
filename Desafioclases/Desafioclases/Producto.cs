using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafioclases
{
    internal class Producto
    {
        public int id { get; set; }
        public int stock { get; set; }
        public int id_usuario { get; set; }
        public float costo { get; set; }
        public float precio_venta { get; set; }
        public string descripcion { get; set; }
    }
}
