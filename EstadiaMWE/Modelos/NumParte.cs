using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EstadiaMWE.Modelos
{
    public class NumParte
    {
        int id_NumParte;
        string num_Parte;
        float precio_Unitario;
        int fK_Cliente;

        public int Id_NumParte { get => id_NumParte; set => id_NumParte = value; }
        public string Num_Parte { get => num_Parte; set => num_Parte = value; }
        public float Precio_Unitario { get => precio_Unitario; set => precio_Unitario = value; }
        public int FK_Cliente { get => fK_Cliente; set => fK_Cliente = value; }
    }
}