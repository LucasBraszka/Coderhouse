using GestionDeVentas.Models;
using System.Data.SqlClient;

namespace GestionDeVentas.Repository
{
    public class ProductoVendidoRepository
    {
        private SqlConnection conexion;
        private String cadenaConexion = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=ajomuch92_coderhouse_csharp_40930;" +
            "User Id=ajomuch92_coderhouse_csharp_40930;" +
            "Password=ElQuequit0Sexy2022;";

        public ProductoVendidoRepository()
        {
            try
            {
                conexion = new SqlConnection(cadenaConexion);
            }
            catch (Exception ex)
            {

            }
        }

        public List<ProductoVendido> listarProductoVendido()
        {
            List<ProductoVendido> lista = new List<ProductoVendido>();
            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM productovendido", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ProductoVendido productovendido = new ProductoVendido();
                                productovendido.Id = long.Parse(reader["Id"].ToString());
                                productovendido.IdProducto = long.Parse(reader["IdProducto"].ToString());
                                productovendido.IdVenta = long.Parse(reader["IdVenta"].ToString());
                                productovendido.Stock = int.Parse(reader["Stock"].ToString());
                             
                                lista.Add(productovendido);
                            }
                        }
                    }
                }
                conexion.Close();
            }
            catch (Exception ex)
            {

            }
            return lista;
        }
    }
}
