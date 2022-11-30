using GestionDeVentas.Models;
using System.Data.SqlClient;


namespace GestionDeVentas.Repository
{
    public class ProductoRepository
    {
        private SqlConnection conexion;
        private String cadenaConexion = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=ajomuch92_coderhouse_csharp_40930;" +
            "User Id=ajomuch92_coderhouse_csharp_40930;" +
            "Password=ElQuequit0Sexy2022;";

        public ProductoRepository()
        {
            try
            {
                conexion = new SqlConnection(cadenaConexion);
            }
            catch (Exception ex)
            {

            }
        }

        public List<Producto> listarProducto()
        {
            List<Producto> lista = new List<Producto>();
            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM producto", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Producto producto = new Producto();
                                producto.Id = long.Parse(reader["Id"].ToString());
                                producto.IdUsuario = long.Parse(reader["IdUsuario"].ToString());
                                producto.Stock = int.Parse(reader["Stock"].ToString());
                                producto.PrecioVenta = double.Parse(reader["PrecioVenta"].ToString());
                                producto.Costo = double.Parse(reader["Costo"].ToString());
                                producto.Descripciones = reader["Descripciones"].ToString();

                                lista.Add(producto);
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
