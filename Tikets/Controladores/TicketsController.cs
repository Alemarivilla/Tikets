using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tikets.Modelos.DAO;
using Tikets.Modelos.Entidades;
using Tikets.Vistas;

namespace Tikets.Controladores
{
    public class TicketsController 
    {
        TiketView vista;
        TicketDAO ticketDAO = new TicketDAO();
        Ticket ticket = new Ticket();
        string operacion = string.Empty;

        public TicketsController(TiketView view)
        {
            vista = view;
            vista.NuevoButton.Click += new EventHandler(Nuevo);
            vista.GuardarButton.Click += new EventHandler(Guardar);
            vista.Load += new EventHandler(Load);
            
        }

        private void Load(object sender, EventArgs e)
        {
            ListarTickets();
        }

        private void Guardar(object sender, EventArgs e)
        {
            if (vista.CodigoTextBox.Text == "")
            {
                
                vista.CodigoTextBox.Focus();
                return;
            }
            if (vista.textBox1.Text == "")
            {

                vista.textBox1.Focus();
                return;
            }
            if (vista.textBox2.Text == "")
            {

                vista.textBox2.Focus();
                return;
            }
            if (vista.textBox3.Text == "")
            {

                vista.textBox3.Focus();
                return;
            }
            if (vista.textBox4.Text == "")
            {

                vista.textBox4.Focus();
                return;
            }

            ticket.Numero = vista.CodigoTextBox.Text;
            ticket.IdCliente = Convert.ToInt32(vista.textBox1.Text);
            ticket.IdEstado = Convert.ToInt32(vista.textBox2.Text);
            ticket.IdTipoSoporte = Convert.ToInt32(vista.textBox3.Text);
            ticket.IdUsuario = Convert.ToInt32(vista.textBox4.Text);

            if (operacion == "Nuevo")
            {
                bool inserto = ticketDAO.CrearNuevoTicket(ticket);
                if (inserto)
                {
                    MessageBox.Show("Ticket creado correctamente");
                    DesabilitarControles();
                    LimpiarControles();
                    ListarTickets();
                }
                else
                {
                    MessageBox.Show("No se pudo crear ticket");
                }
            }

        }

        private void Nuevo(object sender, EventArgs e)
        {
            operacion = "Nuevo";            
            HabilitarControles();
        }

        private void HabilitarControles()
        {
            vista.CodigoTextBox.Enabled = true;
            vista.textBox1.Enabled = true;
            vista.textBox2.Enabled = true;
            vista.textBox3.Enabled = true;
            vista.textBox4.Enabled = true;
            vista.GuardarButton.Enabled = true;
            vista.CancelarButton.Enabled = true;
        }

        private void ListarTickets()
        {
            vista.TicketsdataGridView.DataSource = ticketDAO.GetTickets();
        }

        private void LimpiarControles()
        {
            vista.CodigoTextBox.Clear();
            vista.textBox1.Clear();
            vista.textBox2.Clear();
            vista.textBox3.Clear();
            vista.textBox4.Clear();            
        }

        private void DesabilitarControles()
        {
            vista.CodigoTextBox.Enabled = false;
            vista.textBox1.Enabled = false;
            vista.textBox2.Enabled = false;
            vista.textBox3.Enabled = false;
            vista.textBox4.Enabled = false;
            vista.GuardarButton.Enabled = false;
            vista.CancelarButton.Enabled = false;

            
            vista.NuevoButton.Enabled = true;
        }
    }
}
