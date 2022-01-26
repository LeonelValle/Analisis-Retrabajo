using EstadiaMWE.Control;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace EstadiaMWE
{
    [Serializable]
    class Globales
    {
        public bool esMultiseleccion;

        public bool esNuevaUnidad;

        public List<Modelos.Defecto> defectos_seleccionados = new List<Modelos.Defecto>();

        public List<Modelos.Defecto> defectos_temporales = new List<Modelos.Defecto>();

        public List<string> num_serie = new List<string>();

        // public static Modelos.Usuario usuario_actual = new Modelos.Usuario();

        public Modelos.Unidad unidad_seleccionada = new Modelos.Unidad();

        public List<Modelos.Unidad> unidades_seleccionadas = new List<Modelos.Unidad>();

        public string fecha_entrada = "";

        public string fecha_salida = "";
    }


}