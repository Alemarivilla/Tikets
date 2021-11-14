using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tikets.Vistas
{
    public partial class MenuView : Form
    {
        public MenuView()
        {
            InitializeComponent();
        }
        UsuariosView users;
        ClientesView clientes;
        private void UsuariosToolStripButton_Click(object sender, EventArgs e)
        {
            if (users == null)
            {
                users = new UsuariosView();
                users.MdiParent = this;
                users.FormClosed += Users_FormClosed;
                users.Show();
            }
            else
            {
                users.Activate();
            }

        }

        private void Users_FormClosed(object sender, FormClosedEventArgs e)
        {
            users = null;
        }

        private void ClientesToolStripButton_Click(object sender, EventArgs e)
        {
            if (clientes == null)
            {
                clientes = new ClientesView();
                clientes.MdiParent = this;
                clientes.FormClosed += Clientes_FormClosed;
                clientes.Show();
            }
            else
            {
                clientes.Activate();
            }
        }

        private void Clientes_FormClosed(object sender, FormClosedEventArgs e)
        {
            clientes = null;
        }

        private void iniciarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginView login = new LoginView();
            login.Show();
        }

        private void crearUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsuariosView usuario = new UsuariosView();
            usuario.Show();
        }

        private void ingresarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TiketView tiket = new TiketView();
            tiket.Show();
        }

        private void registrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClientesView cliente = new ClientesView();
            cliente.Show();
        }

        private void ingresarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TipoSoporteView tipoSoporte = new TipoSoporteView();
            tipoSoporte.Show();
        }

        private void agregarEstadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EstadoView estado = new EstadoView();
            estado.Show();
        }
    }
}
