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
    public class TicketDAO : Conexion
    {
        SqlCommand comando = new SqlCommand();

        public bool CrearNuevoTicket(Ticket ticket)
        {
            bool inserto = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" INSERT INTO TICKET ");
                sql.Append(" VALUES (@Numero, @IdTipoSoporte, @IdEstado, @Usuario, @IdCliente); ");
                
                MiConexion.Open();
                comando.CommandType = CommandType.Text;
                comando.CommandText = sql.ToString();
                comando.Parameters.Add("@Numero", SqlDbType.NVarChar, 20).Value = ticket.Numero;
                comando.Parameters.Add("@IdTipoSoporte", SqlDbType.Int).Value = ticket.IdTipoSoporte;
                comando.Parameters.Add("@IdEstado", SqlDbType.Int).Value = ticket.IdEstado;
                comando.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = ticket.IdUsuario;
                comando.Parameters.Add("@IdCliente", SqlDbType.Int).Value = ticket.IdCliente;
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

        public DataTable GetTickets()
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM TICKET ");

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
    }
}
