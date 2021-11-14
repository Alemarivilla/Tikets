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
    public class TipoSoporteController
    {
        TipoSoporteView vista;
        TipoSoporteDAO tipoSoporteDAO = new TipoSoporteDAO();
        TipoSoporte tipoSoporte = new TipoSoporte();
        string operacion = string.Empty;

        public TipoSoporteController(TipoSoporteView view)
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
            if (vista.TiposSoportedataGridView.SelectedRows.Count > 0)
            {
                bool elimino = tipoSoporteDAO.EliminarTipoSoporte(Convert.ToInt32(vista.TiposSoportedataGridView.CurrentRow.Cells[0].Value));
                if (elimino)
                {
                    MessageBox.Show("Tipo de soporte eliminado correctamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarTiposSoporte();
                } 
                else
                {
                    MessageBox.Show("Tipo de soporte no se pudo eliminar", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Modificar(object sender, EventArgs e)
        {
            if (vista.TiposSoportedataGridView.SelectedRows.Count > 0)
            {
                operacion = "Modificar";
                HabilitarControles();

                vista.IdtextBox.Text = vista.TiposSoportedataGridView.CurrentRow.Cells["ID"].Value.ToString();
                vista.NombreTextBox.Text = vista.TiposSoportedataGridView.CurrentRow.Cells["NOMBRE"].Value.ToString();
                            
            }
        }

        private void Load(object sender, EventArgs e)
        {
            ListarTiposSoporte();
        }

        private void ListarTiposSoporte()
        {
            vista.TiposSoportedataGridView.DataSource = tipoSoporteDAO.GetTipoSoporte();
        }

        private void Guardar(object sender, EventArgs e)
        {
            
            if (vista.NombreTextBox.Text == "")
            {                
                vista.NombreTextBox.Focus();
                return;
            }
           
           

          
            tipoSoporte.Nombre = vista.NombreTextBox.Text;
          

           

            if (operacion == "Nuevo")
            {
                bool inserto = tipoSoporteDAO.InsertarNuevoTipoSoporte(tipoSoporte);
                if (inserto)
                {
                    MessageBox.Show("Tipo de soporte insertado correctamente");
                    DesabilitarControles();
                    LimpiarControles();
                    ListarTiposSoporte();
                }
                else
                {
                    MessageBox.Show("Tipo de soporte no se pudo insertar");
                }
            }
            else if (operacion == "Modificar")
            {
                tipoSoporte.Id = Convert.ToInt32(vista.IdtextBox.Text);
                bool modifico = tipoSoporteDAO.ActualizarTipoSoporte(tipoSoporte);
                if (modifico)
                {
                    MessageBox.Show("Tipo modificado correctamente", "Atanción", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DesabilitarControles();
                    LimpiarControles();
                    ListarTiposSoporte();
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
