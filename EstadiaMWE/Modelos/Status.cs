using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EstadiaMWE.Modelos
{
    public class Status
    {
        int id_Status;
        string nombre_Status;

        public int Id_Status { get => id_Status; set => id_Status = value; }
        public string Nombre_Status { get => nombre_Status; set => nombre_Status = value; }
    }
}