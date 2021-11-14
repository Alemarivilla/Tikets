using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tikets.Modelos.Entidades;

namespace Tikets.Modelos.DAO
{
    public class TipoSoporteDAO : Conexion
    {
        SqlCommand comando = new SqlCommand();

        public bool InsertarNuevoTipoSoporte(TipoSoporte tipoSoporte)
        {
            bool inserto = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" INSERT INTO TIPOSOPORTE ");
                sql.Append(" VALUES ( @Nombre); ");
                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = CommandType.Text;
                comando.CommandText = sql.ToString();
                comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 50).Value = tipoSoporte.Nombre;
                comando.ExecuteNonQuery();
                MiConexion.Close();
                inserto = true;
            }
            catch(Exception)
            {
                inserto = false;
                MiConexion.Close();
            }
            return inserto;
        }

        public DataTable GetTipoSoporte()
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM TIPOSOPORTE");
                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = CommandType.Text;
                comando.CommandText = sql.ToString();
                SqlDataReader dr = comando.ExecuteReader();
                dt.Load(dr);
                MiConexion.Close();
            }
            catch(Exception)
            {
                MiConexion.Close();
            }
            return dt;
        }

        public bool ActualizarTipoSoporte(TipoSoporte tipoSoporte)
        {
            bool actualizo = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" UPDATE TIPOSOPORTE ");
                sql.Append(" SET NOMBRE = @Nombre ");
                sql.Append(" WHERE ID = @Id; ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = CommandType.Text;
                comando.CommandText = sql.ToString();

                comando.Parameters.Add("@Id", SqlDbType.Int).Value = tipoSoporte.Id;
                
                comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 50).Value = tipoSoporte.Nombre;
                
               

                comando.ExecuteNonQuery();
                MiConexion.Close();
                actualizo = true;
            }
            catch (Exception)
            {
                actualizo = false;
                MiConexion.Close();
            }
            return actualizo;
        }

        public bool EliminarTipoSoporte(int id)
        {
            bool elimino = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" DELETE FROM TIPOSOPORTE ");
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
                elimino = false;
                MiConexion.Close();
            }
            return elimino;
        }

    }
}
