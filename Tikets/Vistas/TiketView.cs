using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tikets.Controladores;

namespace Tikets.Vistas
{
    public partial class TiketView : Form
    {
        public TiketView()
        {
            InitializeComponent();
            TicketsController controlador = new TicketsController(this);
        }
    }
}
