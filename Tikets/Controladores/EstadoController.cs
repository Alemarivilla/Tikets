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
    public class EstadoController
    {
        EstadoView vista;
        EstadoDAO estadoDAO = new EstadoDAO();
        Estado estado = new Estado();
        string operacion = string.Empty;

        public EstadoController(EstadoView view)
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
            if (vista.EstadodataGridView.SelectedRows.Count > 0)
            {
                bool elimino = estadoDAO.EliminarEstado(Convert.ToInt32(vista.EstadodataGridView.CurrentRow.Cells[0].Value));
                if (elimino)
                {
                    MessageBox.Show("Estado eliminado correctamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarEstados();
                }
                else
                {
                    MessageBox.Show("Estado no se pudo eliminar", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Modificar(object sender, EventArgs e)
        {
            if (vista.EstadodataGridView.SelectedRows.Count > 0)
            {
                operacion = "Modificar";
                HabilitarControles();

                vista.IdtextBox.Text = vista.EstadodataGridView.CurrentRow.Cells["ID"].Value.ToString();
                vista.NombreTextBox.Text = vista.EstadodataGridView.CurrentRow.Cells["DESCRIPCION"].Value.ToString();

            }
        }

        private void Load(object sender, EventArgs e)
        {
            ListarEstados();
        }

        private void ListarEstados()
        {
            vista.EstadodataGridView.DataSource = estadoDAO.GetEstado();
        }

        private void Guardar(object sender, EventArgs e)
        {

            if (vista.NombreTextBox.Text == "")
            {
                vista.NombreTextBox.Focus();
                return;
            }




            estado.Descripcion = vista.NombreTextBox.Text;




            if (operacion == "Nuevo")
            {
                bool inserto = estadoDAO.InsertarNuevoEstado(estado);
                if (inserto)
                {
                    MessageBox.Show("Estado insertado correctamente");
                    DesabilitarControles();
                    LimpiarControles();
                    ListarEstados();
                }
                else
                {
                    MessageBox.Show("Tipo de soporte no se pudo insertar");
                }
            }
            else if (operacion == "Modificar")
            {
                estado.Id = Convert.ToInt32(vista.IdtextBox.Text);
                bool modifico = estadoDAO.ActualizarEstado(estado);
                if (modifico)
                {
                    MessageBox.Show("Estado modificado correctamente", "Atanción", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DesabilitarControles();
                    LimpiarControles();
                    ListarEstados();
                }
                else
                {
                    MessageBox.Show("Estado no se pudo modificar", "Atanción", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Nuevo(object sender, EventArgs e)
        {
            operacion = "Nuevo";
            HabilitarControles();
        }

        private void LimpiarControles()
        {
            vista.NombreTextBox.Clear();
            vista.IdtextBox.Clear();
        }

        private void DesabilitarControles()
        {
            vista.NombreTextBox.Enabled = false;
            vista.GuardarButton.Enabled = false;
            vista.CancelarButton.Enabled = false;
            vista.ModificarButton.Enabled = true;
            vista.NuevoButton.Enabled = true;
        }

        private void HabilitarControles()
        {
            vista.NombreTextBox.Enabled = true;
            vista.GuardarButton.Enabled = true;
            vista.CancelarButton.Enabled = true;
            vista.ModificarButton.Enabled = false;
            vista.NuevoButton.Enabled = false;
        }
    }
}
