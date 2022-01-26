using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EstadiaMWE.Modelos
{
    public class ModeloCliente
    {
        int id_Cliente;
        string cliente;

        public int Id_Cliente { get => id_Cliente; set => id_Cliente = value; }
        public string Cliente { get => cliente; set => cliente = value; }
    }
}