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
    public class UsuarioController
    {
        UsuariosView vista;
        string operacion = string.Empty;
        UsuarioDAO usuarioDAO = new UsuarioDAO();
        Usuario user = new Usuario();

        public UsuarioController(UsuariosView view)
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
            if (vista.UsuariosDataGridView.SelectedRows.Count > 0)
            {
                bool elimino = usuarioDAO.EliminarUsuario(Convert.ToInt32(vista.UsuariosDataGridView.CurrentRow.Cells[0].Value.ToString()));
                if (elimino)
                {
                    MessageBox.Show("Usuario eliminado correctamente", "Atención",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarControles();
                    DesabilitarControles();
                    ListarUsuarios();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el usuario", "Atención",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Load(object sender, EventArgs e)
        {
            ListarUsuarios();
        }
        private void Modificar(object sender, EventArgs e)
        {
            operacion = "Modificar";
            if (vista.UsuariosDataGridView.SelectedRows.Count > 0)
            {
                vista.IdTextBox.Text = vista.UsuariosDataGridView.CurrentRow.Cells["ID"].Value.ToString();
                vista.NombretextBox.Text = vista.UsuariosDataGridView.CurrentRow.Cells["NOMBRE"].Value.ToString();
                vista.EmailtextBox.Text = vista.UsuariosDataGridView.CurrentRow.Cells["EMAIL"].Value.ToString();
                vista.EsAdministradorcheckBox.Checked = Convert.ToBoolean(vista.UsuariosDataGridView.CurrentRow.Cells["ESADMINISTRADOR"].Value);
                HabilitarControles();
            }
        }
        private void ListarUsuarios()
        {
            vista.UsuariosDataGridView.DataSource = usuarioDAO.GetUsuarios();
        }

        private void Nuevo(object sender, EventArgs e)
        {
            HabilitarControles();
            operacion = "Nuevo";
        }

        private void Guardar(object sender, EventArgs e)
        {
            if (vista.NombretextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.NombretextBox, "Ingrese un nombre");
                vista.NombretextBox.Focus();
                return;
            }
            if (vista.EmailtextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.EmailtextBox, "Ingrese un email");
                vista.EmailtextBox.Focus();
                return;
            }
            if (vista.ClavetextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.ClavetextBox, "Ingrese una contraseña");
                vista.ClavetextBox.Focus();
                return;
            }

            user.Nombre = vista.NombretextBox.Text;
            user.Email = vista.EmailtextBox.Text;
            user.Clave = vista.ClavetextBox.Text;
            user.EsAdministrador = vista.EsAdministradorcheckBox.Checked;

            if (operacion == "Nuevo")
            {
                bool inserto = usuarioDAO.InsertarNuevoUsuario(user);

                if (inserto)
                {
                    MessageBox.Show("Usuario creado correctamente", "Atención",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarControles();
                    DesabilitarControles();
                    ListarUsuarios();
                }
                else
                {
                    MessageBox.Show("Error al crear el usuario", "Atención",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (operacion == "Modificar")
            {
                user.Id = Convert.ToInt32(vista.IdTextBox.Text);
                bool modifico = usuarioDAO.ActualizarUsuario(user);
                if (modifico)
                {
                    MessageBox.Show("Usuario modificado correctamente", "Atención",
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarControles();
                    DesabilitarControles();
                    ListarUsuarios();
                }
                else
                {
                    MessageBox.Show("Error al modificar el usuario", "Atención",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }


        }

        private void HabilitarControles()
        {
            vista.NombretextBox.Enabled = true;
            vista.EmailtextBox.Enabled = true;
            vista.ClavetextBox.Enabled = true;
            vista.EsAdministradorcheckBox.Enabled = true;
            vista.GuardarButton.Enabled = true;
            vista.CancelarButton.Enabled = true;
            vista.ModificarButton.Enabled = false;
            vista.NuevoButton.Enabled = false;
        }

        private void LimpiarControles()
        {
            vista.NombretextBox.Clear();
            vista.EmailtextBox.Clear();
            vista.ClavetextBox.Clear();
            vista.EsAdministradorcheckBox.Checked = false;
        }

        private void DesabilitarControles()
        {
            vista.NombretextBox.Enabled = false;
            vista.EmailtextBox.Enabled = false;
            vista.ClavetextBox.Enabled = false;
            vista.EsAdministradorcheckBox.Enabled = false;
            vista.GuardarButton.Enabled = false;
            vista.CancelarButton.Enabled = false;
            vista.ModificarButton.Enabled = true;
            vista.NuevoButton.Enabled = true;
        }

    }


}

