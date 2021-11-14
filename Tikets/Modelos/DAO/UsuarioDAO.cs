using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tikets.Modelos.Entidades;

namespace Tikets.Modelos.DAO
{
    public class UsuarioDAO : Conexion
    {
        SqlCommand comando = new SqlCommand();

        public bool ValidarUsuario(Usuario user)
        {
            bool valido = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT 1 FROM Usuario WHERE Email= @Email AND Clave = @Clave; ");
                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = CommandType.Text;
                comando.CommandText = sql.ToString();
                comando.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = user.Email;
                comando.Parameters.Add("@Clave", SqlDbType.NVarChar, 80).Value = user.Clave;
                valido = Convert.ToBoolean(comando.ExecuteScalar());
                MiConexion.Close();
            }
            catch (Exception )
            {
                MiConexion.Close();
            }
            return valido;
        }

        public bool InsertarNuevoUsuario(Usuario user)
        {
            bool inserto = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" INSERT INTO Usuario ");
                sql.Append(" VALUES (@Nombre, @Email, @Clave, @EsAdministrador); ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = CommandType.Text;
                comando.CommandText = sql.ToString();

                comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 50).Value = user.Nombre;
                comando.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = user.Email;
                comando.Parameters.Add("@Clave", SqlDbType.NVarChar, 80).Value = EncriptarClave(user.Clave);
                comando.Parameters.Add("@EsAdministrador", SqlDbType.Bit).Value = user.EsAdministrador;
                comando.ExecuteNonQuery();
                inserto = true;
                MiConexion.Close();

            }
            catch (Exception)
            {
                inserto = false;
                MiConexion.Close();
            }
            return inserto;
        }

        public static string EncriptarClave(string str)
        {
            string cadena = str + "MiClavePersonal";
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(cadena));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public DataTable GetUsuarios()
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM Usuario ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = CommandType.Text;
                comando.CommandText = sql.ToString();

                SqlDataReader dr = comando.ExecuteReader();
                dt.Load(dr);
                MiConexion.Close();
            }
            catch (Exception)
            {
                MiConexion.Close();
            }
            return dt;
        }

        public bool ActualizarUsuario(Usuario user)
        {
            bool actualizo = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" UPDATE Usuario ");
                sql.Append(" SET Nombre = @Nombre, Email = @Email, Clave = @Clave, EsAdministrador = @EsAdministrador ");
                sql.Append(" WHERE ID = @Id; ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = CommandType.Text;
                comando.CommandText = sql.ToString();

                comando.Parameters.Add("@Id", SqlDbType.Int).Value = user.Id;
                comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 50).Value = user.Nombre;
                comando.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = user.Email;
                comando.Parameters.Add("@Clave", SqlDbType.NVarChar, 80).Value = EncriptarClave(user.Clave);
                comando.Parameters.Add("@EsAdministrador", SqlDbType.Bit).Value = user.EsAdministrador;
                comando.ExecuteNonQuery();
                actualizo = true;
                MiConexion.Close();

            }
            catch (Exception)
            {
                MiConexion.Close();
                actualizo = false;
            }
            return actualizo;
        }

        public bool EliminarUsuario(int id)
        {
            bool elimino = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" DELETE FROM Usuario ");
                sql.Append(" WHERE ID = @Id; ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = CommandType.Text;
                comando.CommandText = sql.ToString();

                comando.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                comando.ExecuteNonQuery();
                elimino = true;
                MiConexion.Close();

            }
            catch (Exception)
            {
                MiConexion.Close();
                elimino = false;
            }
            return elimino;
        }

        public string GetNombreUsuarioPorEmail(string email)
        {
            string nombre = string.Empty;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT NOMBRE FROM Usuario WHERE Email = @Email; ");
                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = CommandType.Text;
                comando.CommandText = sql.ToString();
                comando.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = email;

                SqlDataReader dr = comando.ExecuteReader();
                if (dr.Read())
                {
                    nombre = dr["NOMBRE"].ToString();
                }

                MiConexion.Close();
            }
            catch (Exception)
            {
                MiConexion.Close();
            }
            return nombre;
        }

    }
}
