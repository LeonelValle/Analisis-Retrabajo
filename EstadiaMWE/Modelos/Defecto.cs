using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EstadiaMWE.Modelos
{
    [Serializable]
    public class Defecto
    {
        int id_Defecto;
        string codigo;
        string descripcion;
        //public string FullDefecto => $"{codigo}_{descripcion}";
        string fullDefecto;

        string referencia;

        string part_Number;



        public int Id_Defecto { get => id_Defecto; set => id_Defecto = value; }
        public string Codigo { get => codigo; set => codigo = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string defecto { get => fullDefecto; set => fullDefecto = value; }
        public string Referencia { get => referencia; set => referencia = value; }

        public string Part_Number { get => part_Number; set => part_Number = value; }

        //public string Referencia;
    }
}