using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EstadiaMWE.Modelos
{
    public class Bitacora
    {
        int fK_Unidad;
        string Status;
        string Turno;
        string NumEmpleado;
        DateTime fecha;
        string falla;
        string defectos;
        string referencias;


        string part_Number;

        //public DateTime Fecha { get => fecha; set => fecha = DateTime.ParseExact(value.ToString(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture); }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public int FK_Unidad { get => fK_Unidad; set => fK_Unidad = value; }
        public string _Status { get => Status; set => Status = value; }
        public string _Turno { get => Turno; set => Turno = value; }
        public string _NumEmpleado { get => NumEmpleado; set => NumEmpleado = value; }
        public string Falla { get => falla; set => falla = value; }
        public string Defectos { get => defectos; set => defectos = value; }
        public string Referencias { get => referencias; set => referencias = value; }

        public string Part_Numbers { get => part_Number; set => part_Number = value; }
    }
}