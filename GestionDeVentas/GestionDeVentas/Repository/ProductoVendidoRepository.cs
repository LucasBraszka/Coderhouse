using GestionDeVentas.Models;
using System.Data;
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
                                ProductoVendido productoVendido = obtenerProductoVendidoDesdeReader(reader);
                                lista.Add(productoVendido);
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

        private ProductoVendido obtenerProductoVendidoDesdeReader(SqlDataReader reader)
        {
            ProductoVendido productoVendido = new ProductoVendido();
            productoVendido.Id = long.Parse(reader["Id"].ToString());
            productoVendido.IdProducto = int.Parse(reader["IdProducto"].ToString());
            productoVendido.IdVenta = long.Parse(reader["IdVenta"].ToString());
            productoVendido.Stock =int.Parse(reader["Stock"].ToString());
            return productoVendido;
        }

        public List<ProductoVendido> obtenerProductoVendidoIdVenta(long idVenta)
        {
            List<ProductoVendido> lista = new List<ProductoVendido>();
            if (conexion == null) { throw new Exception("Conexión no establecida"); }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM ProductoVendido WHERE idVenta=@idVenta", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt) { Value = idVenta });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ProductoVendido producto = obtenerProductoVendidoDesdeReader(reader);
                                lista.Add(producto);
                            }

                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
            return lista;
        }

        public ProductoVendido cargarProductosVendidos(ProductoVendido productoV)
        {
            ProductoRepository repo = new ProductoRepository();
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO ProductoVendido (IdProducto, Stock, IdVenta) VALUES (@idProducto, @stock, @idVenta); SELECT @@Identity", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.BigInt) { Value = productoV.IdProducto });
                    Producto prodComparar = repo.obtenerProducto(productoV.IdProducto);

                    if (productoV.Stock >= prodComparar.Stock)
                    {
                        Producto productoADescontar = repo.descontarStock(productoV.Stock, productoV.IdProducto);
                        cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = prodComparar.Stock });
                    }
                    else
                    {
                        Producto productoADescontar = repo.descontarStock(productoV.Stock, productoV.IdProducto);
                        cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = productoV.Stock });
                    }

                    cmd.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt) { Value = productoV.IdVenta });
                    productoV.Id = long.Parse(cmd.ExecuteScalar().ToString());
                    return productoV;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }

        public bool eliminarProductoVendido(long idVenta)
        {
            ProductoRepository repo = new ProductoRepository();
            List<ProductoVendido> productoRef = obtenerProductoVendidoIdVenta(idVenta);
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM ProductoVendido WHERE idVenta=@idVenta", conexion))
                {
                    conexion.Open();

                    foreach (ProductoVendido pv in productoRef)
                    {
                        Producto productoAModificar = repo.sumarStock(pv.Stock, pv.IdProducto);

                    }
                    cmd.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt) { Value = idVenta });
                    filasAfectadas = cmd.ExecuteNonQuery();
                }

                return filasAfectadas > 0;
            }
            catch
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }


        }
            public List<ProductoVendido> obtenerProductoVendidoDesdeIdUser(long idVenta)
            {
                ProductoRepository repo = new ProductoRepository();
                List<ProductoVendido> productoRef = obtenerProductoVendidoIdVenta(idVenta);
                if (conexion == null)
                {
                    throw new Exception("Conexión no establecida");
                }
                try
                {

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM ProductoVendido WHERE idVenta=@idVenta", conexion))
                    {
                        conexion.Open();

                        foreach (ProductoVendido pv in productoRef)
                        {
                            Producto productoAModificar = repo.sumarStock(pv.Stock, pv.IdProducto);

                        }
                        cmd.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt) { Value = idVenta });

                    }

                    return productoRef;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    conexion.Close();
                }

            }

            public List<ProductoVendido> obtenerListaProductoVendidoDesdeIdUser(long idVenta)
            {
                ProductoRepository repo = new ProductoRepository();
                List<ProductoVendido> productoRef = obtenerProductoVendidoIdVenta(idVenta);
                if (conexion == null)
                {
                    throw new Exception("Conexión no establecida");
                }
                try
                {

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM ProductoVendido WHERE idVenta=@idVenta", conexion))
                    {
                        conexion.Open();

                        foreach (ProductoVendido pv in productoRef)
                        {
                            Producto productoAModificar = repo.obtenerProducto(pv.IdProducto);

                        }
                        cmd.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt) { Value = idVenta });

                    }

                    return productoRef;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    conexion.Close();
                }

            }

    }
}
