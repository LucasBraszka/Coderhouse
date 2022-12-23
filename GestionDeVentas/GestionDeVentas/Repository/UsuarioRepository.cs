using GestionDeVentas.Models;
using System.Data;
using System.Data.SqlClient;

namespace GestionDeVentas.Repository
{
    public class UsuarioRepository
    {
        private SqlConnection conexion;
        private String cadenaConexion = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=ajomuch92_coderhouse_csharp_40930;" +
            "User Id=ajomuch92_coderhouse_csharp_40930;" +
            "Password=ElQuequit0Sexy2022;";

        public UsuarioRepository()
        {
            try
            {
                conexion = new SqlConnection(cadenaConexion);
            }
            catch (Exception ex)
            {

            }
        }

        public Usuario obtenerUsuarioDesdeReader(SqlDataReader reader)
        {
            Usuario user = new Usuario();
            user.Id = long.Parse(reader["Id"].ToString());
            user.Nombre = reader["Nombre"].ToString();
            user.Apellido = reader["Apellido"].ToString();
            user.NombreUsuario = reader["NombreUsuario"].ToString();
            user.Contraseña = reader["Contraseña"].ToString();
            user.Mail = reader["Mail"].ToString();
            return user;
        }

        public List<Usuario> listarUsuario()
        {
            List<Usuario> listaU = new List<Usuario>();
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario user = obtenerUsuarioDesdeReader(reader);
                            listaU.Add(user);
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
            return listaU;
        }

        public Usuario crearUsuario(Usuario user)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                List<Usuario> listaU = listarUsuario();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Usuario (Nombre, Apellido, NombreUsuario, Contraseña, Mail) " +
                    "VALUES (@nombre, @apellido, @nombreUsuario, @contraseña, @mail); SELECT @@Identity", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("Nombre", SqlDbType.VarChar) { Value = user.Nombre });
                    cmd.Parameters.Add(new SqlParameter("Apellido", SqlDbType.VarChar) { Value = user.Apellido });
                    foreach (Usuario usuario in listaU)
                    {
                        if (user.NombreUsuario == usuario.NombreUsuario)
                        {
                            Random random = new Random();
                            int num = random.Next(1, 999990);
                            user.NombreUsuario = user.Nombre + num.ToString();

                        }
                    }
                    cmd.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = user.NombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("Contraseña", SqlDbType.VarChar) { Value = user.Contraseña });
                    cmd.Parameters.Add(new SqlParameter("Mail", SqlDbType.VarChar) { Value = user.Mail });
                    user.Id = long.Parse(cmd.ExecuteScalar().ToString());
                    return user;

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


        public Usuario obtenerUsuarioDesdeID(long id)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario WHERE Id = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt) { Value = id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Usuario user = obtenerUsuarioDesdeReader(reader);
                            return user;
                        }
                        else
                        {
                            return null;
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
        }
 
        public Usuario? actualizarUsuario(long id, Usuario usuarioAActualizar)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                Usuario user = obtenerUsuarioDesdeID(id);
                if (user == null)
                {
                    return null;
                }
                List<string> camposAActualizar = new List<string>();
                if (user.Nombre != usuarioAActualizar.Nombre && !string.IsNullOrEmpty(usuarioAActualizar.Nombre))
                {
                    camposAActualizar.Add("Nombre = @nombre");
                    user.Nombre = usuarioAActualizar.Nombre;
                }
                if (user.Apellido != usuarioAActualizar.Apellido && !string.IsNullOrEmpty(usuarioAActualizar.Apellido))
                {
                    camposAActualizar.Add("Apellido = @apellido");
                    user.Apellido = usuarioAActualizar.Apellido;
                }
                if (user.NombreUsuario != usuarioAActualizar.NombreUsuario && !string.IsNullOrEmpty(usuarioAActualizar.NombreUsuario))
                {
                    camposAActualizar.Add("NombreUsuario = @nombreUsuario");
                    user.NombreUsuario = usuarioAActualizar.NombreUsuario;
                }
                if (user.Contraseña != usuarioAActualizar.Contraseña && !string.IsNullOrEmpty(usuarioAActualizar.Contraseña))
                {
                    camposAActualizar.Add("Contraseña = @contraseña");
                    user.Contraseña = usuarioAActualizar.Contraseña;
                }
                if (user.Mail != usuarioAActualizar.Mail && !string.IsNullOrEmpty(usuarioAActualizar.Mail))
                {
                    camposAActualizar.Add("Mail = @mail");
                    user.Mail = usuarioAActualizar.Mail;
                }
                if (camposAActualizar.Count == 0)
                {
                    throw new Exception("No new fields to update");
                }
                using (SqlCommand cmd = new SqlCommand($"UPDATE Producto SET {String.Join(", ", camposAActualizar)} WHERE id = @id", conexion))
                {
                    cmd.Parameters.Add(new SqlParameter("Nombre", SqlDbType.VarChar) { Value = usuarioAActualizar.Nombre });
                    cmd.Parameters.Add(new SqlParameter("Apellido", SqlDbType.VarChar) { Value = usuarioAActualizar.Apellido });
                    cmd.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = usuarioAActualizar.NombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("Contraseña", SqlDbType.VarChar) { Value = usuarioAActualizar.Contraseña });
                    cmd.Parameters.Add(new SqlParameter("Mail", SqlDbType.VarChar) { Value = usuarioAActualizar.Mail });
                    conexion.Open();
                    cmd.ExecuteReader();
                    return user;
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

        public Usuario obtenerUsuarioDsdeNU(string nombreU)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario WHERE NombreUsuario = @nombreUsuario", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = nombreU });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Usuario user = obtenerUsuarioDesdeReader(reader);
                            return user;
                        }
                        else
                        {
                            return null;
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
        }

        public bool eliminarUsuario(int id)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Usuario WHERE Id = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt) { Value = id });
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
    }
}
