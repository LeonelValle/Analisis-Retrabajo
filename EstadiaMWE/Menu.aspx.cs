using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EstadiaMWE
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Id_Usuario"] == null)
            {
                Response.Redirect("MenuPrincipal.aspx");
            }
            else
            {
                lblUsuario.Text = Session["Nombre_Empleado"].ToString();

                if (Convert.ToInt32(Session["FK_TipoUsuario"]) == Conexion.obtenerTipoUsuario("Admin"))
                {
                    divAdmin.Visible = true;
                }
                else
                {
                    divAdmin.Visible = false;
                }
            }
        }

        protected void btnMantenimiento_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuMantenimiento.aspx");
        }

        protected void btnRetrabajo_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuRetrabajo.aspx");
        }

        protected void btnReportes_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reportes.aspx");
        }



        protected void btnAnalisis_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuAnalisis.aspx");
        }

        protected void btnBitacora_Click(object sender, EventArgs e)
        {
            Response.Redirect("Bitacora.aspx");
        }

        protected void btnModificaciones_Click(object sender, EventArgs e)
        {
            Response.Redirect("Modificaciones.aspx");
        }
    }
}