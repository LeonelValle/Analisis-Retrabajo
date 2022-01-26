using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EstadiaMWE.Modelos
{
    [Serializable]
    public class Unidad
    {
        int id_Unidad;
        string work_Order;
        //string referencia;
        int fK_Area;
        int fK_PartNumber;
        int fK_Status;
        string serial_Number;
        string falla;

        public int Id_Unidad { get => id_Unidad; set => id_Unidad = value; }
        public string Work_Order { get => work_Order; set => work_Order = value; }
        //public string Referencia { get => referencia; set => referencia = value; }
        public int FK_Area { get => fK_Area; set => fK_Area = value; }
        public int FK_PartNumber { get => fK_PartNumber; set => fK_PartNumber = value; }
        public string Serial_Number { get => serial_Number; set => serial_Number = value; }
        public int FK_Status { get => fK_Status; set => fK_Status = value; }
        public string Falla { get => falla; set => falla = value; }
    }
}