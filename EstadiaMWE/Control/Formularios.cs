using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace EstadiaMWE.Control
{
    public interface Formularios
    {
        void LlenarGridDefectos(bool refresh);
        void RefrescarGridDefectos();
        void LlenarDdlBD(DropDownList ddl, string tabla, string nombre, string id);
        void LlenarGrid(DataSet ds);
        void RefrescarGrid();
        void CambiarColorRenglon();
        void AgregarUnidadesSelec();
        void ActualizarUnidadSelec();
        void LimpiarInfoUnidad();
        void MostrarInfoUnidad();
        void ModificarInfoUnidad(bool modificable);
        bool CamposValidos();
        void AgregarDefecto();
        void EliminarDefecto();
        bool esBusqueda();
        void LimpiarBusqueda();
    }
}