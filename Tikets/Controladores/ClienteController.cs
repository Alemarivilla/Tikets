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
    public class ClienteController
    {
        ClientesView vista;
        ClienteDAO clienteDAO = new ClienteDAO();
        Cliente cliente = new Cliente();
        string operacion = string.Empty;

        public ClienteController(ClientesView view)
        {
            vista = view;
            vista.NuevoButton.Click += new EventHandler(Nuevo);
            vista.GuardarButton.Click += new EventHandler(Guardar);
            vista.Load += new EventHandler(Load);
            vista.ModificarButton.Click += new EventHandler(Modificar);
            vista.EliminarButton.Click += new EventHandler(Eliminar);
        }

        private void Eliminar(object sender, EventArgs e)
        {
            if (vista.ClientesdataGridView.SelectedRows.Count > 0)
            {
                bool elimino = clienteDAO.EliminarCliente(Convert.ToInt32(vista.ClientesdataGridView.CurrentRow.Cells[0].Value));
                if (elimino)
                {
                    MessageBox.Show("Cliente eliminado correctamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarClientes();
                }
                else
                {
                    MessageBox.Show("Cliente no se pudo eliminar", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Modificar(object sender, EventArgs e)
        {
            if (vista.ClientesdataGridView.SelectedRows.Count > 0)
            {
                operacion = "Modificar";
                HabilitarControles();

                vista.IdtextBox.Text = vista.ClientesdataGridView.CurrentRow.Cells["ID"].Value.ToString();
                vista.NombreTextBox.Text = vista.ClientesdataGridView.CurrentRow.Cells["NOMBRE"].Value.ToString();
                vista.EmailTextBox.Text = vista.ClientesdataGridView.CurrentRow.Cells["EMAIL"].Value.ToString();
                vista.IdentidadTextBox.Text = vista.ClientesdataGridView.CurrentRow.Cells["IDENTIDAD"].Value.ToString();
                vista.DirecciontextBox.Text = vista.ClientesdataGridView.CurrentRow.Cells["DIRECCION"].Value.ToString();

            }
        }

        private void Load(object sender, EventArgs e)
        {
            ListarClientes();
        }

        private void ListarClientes()
        {
            vista.ClientesdataGridView.DataSource = clienteDAO.GetClientes();
        }

        private void Guardar(object sender, EventArgs e)
        {
            if (vista.IdentidadTextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.IdentidadTextBox, "Ingrese una identidad");
                vista.IdentidadTextBox.Focus();
                return;
            }
            if (vista.NombreTextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.NombreTextBox, "Ingrese un nombre");
                vista.NombreTextBox.Focus();
                return;
            }
            if (vista.EmailTextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.EmailTextBox, "Ingrese un email");
                vista.EmailTextBox.Focus();
                return;
            }
            if (vista.DirecciontextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.DirecciontextBox, "Ingrese un email");
                vista.DirecciontextBox.Focus();
                return;
            }

            cliente.Identidad = vista.IdentidadTextBox.Text;
            cliente.Nombre = vista.NombreTextBox.Text;
            cliente.Email = vista.EmailTextBox.Text;
            cliente.Direccion = vista.DirecciontextBox.Text;

            

            if (operacion == "Nuevo")
            {
                bool inserto = clienteDAO.InsertarNuevoCliente(cliente);
                if (inserto)
                {
                    MessageBox.Show("Cliente insertado correctamente");
                    DesabilitarControles();
                    LimpiarControles();
                    ListarClientes();
                }
                else
                {
                    MessageBox.Show("Cliente no se pudo insertar");
                }
            }
            else if (operacion == "Modificar")
            {
                cliente.Id = Convert.ToInt32(vista.IdtextBox.Text);
                bool modifico = clienteDAO.ActualizarCliente(cliente);
                if (modifico)
                {
                    MessageBox.Show("Cliente modificado correctamente", "Atanción", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DesabilitarControles();
                    LimpiarControles();
                    ListarClientes();
                }
                else
                {
                    MessageBox.Show("Cliente no se pudo modificar", "Atanción", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            vista.IdentidadTextBox.Enabled = true;
            vista.NombreTextBox.Enabled = true;
            vista.EmailTextBox.Enabled = true;
            vista.DirecciontextBox.Enabled = true;
           
            vista.GuardarButton.Enabled = true;
            vista.CancelarButton.Enabled = true;

            vista.ModificarButton.Enabled = false;
            vista.NuevoButton.Enabled = false;
        }

        private void LimpiarControles()
        {
            vista.IdentidadTextBox.Clear();
            vista.NombreTextBox.Clear();
            vista.EmailTextBox.Clear();
            vista.DirecciontextBox.Clear();
            
            vista.IdtextBox.Clear();
        }
        private void DesabilitarControles()
        {
            vista.IdentidadTextBox.Enabled = false;
            vista.NombreTextBox.Enabled = false;
            vista.EmailTextBox.Enabled = false;
            vista.DirecciontextBox.Enabled = false;
           
            vista.GuardarButton.Enabled = false;
            vista.CancelarButton.Enabled = false;

            vista.ModificarButton.Enabled = true;
            vista.NuevoButton.Enabled = true;
        }
    }
}
