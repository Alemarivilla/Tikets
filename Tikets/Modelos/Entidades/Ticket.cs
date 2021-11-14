using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tikets.Modelos.Entidades
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public int IdTipoSoporte { get; set; }
        public int IdEstado { get; set; }
        public int IdUsuario { get; set; }
        public int IdCliente { get; set; }
    }
}
