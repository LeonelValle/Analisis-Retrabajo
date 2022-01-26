using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EstadiaMWE.Modelos
{
    public class Unidad_Defecto
    {
        int fK_Unidad;
        int fK_Defecto;
        string referencia;
        string part_Number;

        public int FK_Unidad { get => fK_Unidad; set => fK_Unidad = value; }
        public int FK_Defecto { get => fK_Defecto; set => fK_Defecto = value; }
        public string Referencia { get => referencia; set => referencia = value; }
        public string Part_Number { get => part_Number; set => part_Number = value; }
    }
}