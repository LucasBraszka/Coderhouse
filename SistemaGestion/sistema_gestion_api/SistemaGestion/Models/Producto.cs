using System;
using Microsoft.Extensions.Hosting;

namespace SistemaGestion.Models
{
    public class Producto
    {
        public long Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Costo { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int IdUsuario { get; set; }


        public Producto()
        {
            Id = 0;
            Descripcion = "";
            Costo = 0;
            PrecioVenta = 0;
            Stock = 0;
            IdUsuario = 0;

        }

        public Producto(long codigo, string descripcion, decimal costo, decimal precioVenta, int stock, int idUsuario)
        {
            Id = codigo;
            Descripcion = descripcion;
            Costo = costo;
            PrecioVenta = precioVenta;
            Stock = stock;
            IdUsuario = idUsuario;

        }
    }
}
