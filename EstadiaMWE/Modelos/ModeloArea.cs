using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EstadiaMWE.Modelos
{
    public class ModeloArea
    {
        int id_Area;
        string area;

        public int Id_Area { get => id_Area; set => id_Area = value; }
        public string Area { get => area; set => area = value; }
    }
}