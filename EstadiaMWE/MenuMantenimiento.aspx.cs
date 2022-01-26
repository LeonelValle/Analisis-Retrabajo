using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EstadiaMWE
{
    public partial class MenuMantenimiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Nombre_Empleado"] == null || Convert.ToInt32(Session["FK_TipoUsuario"]) != Conexion.obtenerTipoUsuario("Admin"))
            {
                Response.Redirect("MenuPrincipal.aspx");
            }
            else
            {
                lblUsuario.Text = Session["Nombre_Empleado"].ToString();
            }
        }
    }
}